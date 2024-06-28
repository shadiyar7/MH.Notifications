using MH.Notifications.Core.Entities;
using MH.Notifications.Core.Interfaces;
using MH.Notifications.Infrastructure.Data;
using System.Threading.Tasks;

namespace MH.Notifications.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationContext _context;

        public NotificationRepository(NotificationContext context)
        {
            _context = context;
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
