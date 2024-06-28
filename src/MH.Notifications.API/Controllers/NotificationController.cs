using Microsoft.AspNetCore.Mvc;
using MH.Notifications.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using MH.Notifications.Application.Services;

namespace MH.Notifications.API.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationsController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("send/{sponsorId}")]
        public async Task<IActionResult> SendNotification(long sponsorId, [FromBody] string message)
        {
            await _notificationService.SendNotificationToReferralsAsync(sponsorId, message);
            return Ok("Notifications sent successfully.");
        }
    }
}
