using Microsoft.EntityFrameworkCore;
using MH.Notifications.Core.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MH.Notifications.Infrastructure.Data
{
    public class NotificationContext : DbContext
    {
        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Referrals)
                .WithOne(u => u.Sponsor)
                .HasForeignKey(u => u.SponsorId);

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Message)
                    .IsRequired();

                entity.Property(e => e.IsSent)
                    .HasDefaultValue(false);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
