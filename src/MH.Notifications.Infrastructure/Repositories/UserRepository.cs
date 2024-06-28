using Microsoft.EntityFrameworkCore;
using MH.Notifications.Core.Entities;
using MH.Notifications.Infrastructure.Data;
using System.Threading.Tasks;
using MH.Notifications.Core.Interfaces;

namespace MH.Notifications.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NotificationContext _context;

        public UserRepository(NotificationContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetReferralsBySponsorIdAsync(long sponsorId)
        {
            return await _context.Users.Where(u => u.SponsorId == sponsorId).ToListAsync();
        }
    }
}
