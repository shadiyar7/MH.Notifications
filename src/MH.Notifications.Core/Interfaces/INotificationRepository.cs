using MH.Notifications.Core.Entities;
using System.Threading.Tasks;

namespace MH.Notifications.Core.Interfaces
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification);
        Task SaveChangesAsync();
    }
}
