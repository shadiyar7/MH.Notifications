using System.Collections.Generic;
using System.Threading.Tasks;

namespace MH.Notifications.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationToReferralsAsync(long sponsorId, string message);
    }
}
