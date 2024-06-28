using MH.Notifications.Core.Entities;
using System.Threading.Tasks;

namespace MH.Notifications.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(long id);
        Task<List<User>> GetReferralsBySponsorIdAsync(long sponsorId);
    }
}
