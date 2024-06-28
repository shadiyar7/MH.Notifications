using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MH.Notifications.Core.Entities;
using System;
using System.Linq;

namespace MH.Notifications.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task Initialize(NotificationContext context)
        {
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new[]
            {
                new User { Id = 1, StatusId = 1 },
                new User { Id = 2, SponsorId = 1, StatusId = 2 },
                new User { Id = 3, SponsorId = 2, StatusId = 3 },
                new User { Id = 4, SponsorId = 3, StatusId = 1 },
                new User { Id = 5, SponsorId = 2, StatusId = 2 },
                new User { Id = 6, SponsorId = 5, StatusId = 3 },
                new User { Id = 7, SponsorId = 6, StatusId = 1 },
                new User { Id = 8, StatusId = 2 },
                new User { Id = 9, SponsorId = 8, StatusId = 3 },
                new User { Id = 10, SponsorId = 9, StatusId = 1 },
                new User { Id = 11, SponsorId = 10, StatusId = 2 },
                new User { Id = 12, SponsorId = 9, StatusId = 3 },
                new User { Id = 13, SponsorId = 12, StatusId = 1 },
                new User { Id = 14, SponsorId = 13, StatusId = 2 }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
