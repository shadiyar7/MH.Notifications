using Hangfire;
using MH.Notifications.Application.Interfaces;
using MH.Notifications.Core.Entities;
using MH.Notifications.Core.Interfaces;
using MH.Notifications.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MH.Notifications.Application.Services
{
    public class NotificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly NotificationContext _context;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IUserRepository userRepository, NotificationContext context, ILogger<NotificationService> logger)
        {
            _userRepository = userRepository;
            _context = context;
            _logger = logger;
        }

        public async Task SendNotificationToReferralsAsync(long sponsorId, string message)
        {
            var referrals = await _userRepository.GetReferralsBySponsorIdAsync(sponsorId);
            await SendNotificationRecursively(referrals, message);
        }

        private async Task SendNotificationRecursively(List<User> referrals, string message)
        {
            foreach (var referral in referrals)
            {
                // Create notification record
                var notification = new Notification
                {
                    UserId = referral.Id,
                    Message = message,
                    IsSent = false
                };
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                // Enqueue background job to send notification
                BackgroundJob.Enqueue(() => SendNotificationAsync(notification.Id));

                // Get sub-referrals and send notifications recursively
                var subReferrals = await _userRepository.GetReferralsBySponsorIdAsync(referral.Id);
                await SendNotificationRecursively(subReferrals, message);
            }
        }

        public async Task SendNotificationAsync(long notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null || notification.IsSent)
            {
                return;
            }

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.PostAsync("https://webhook.site/your-webhook-url", new StringContent(notification.Message));

                if (response.IsSuccessStatusCode)
                {
                    notification.IsSent = true;
                    notification.SentAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Failed to send notification");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending notification. Retrying...");

                // Retry after delay
                BackgroundJob.Schedule(() => SendNotificationAsync(notificationId), TimeSpan.FromMinutes(1));
            }
        }
    }
}
