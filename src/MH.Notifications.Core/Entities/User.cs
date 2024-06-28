namespace MH.Notifications.Core.Entities
{
    public class User
    {
        public long Id { get; set; }
        public long? SponsorId { get; set; }
        public long StatusId { get; set; }

        public virtual User Sponsor { get; set; }
        public virtual ICollection<User> Referrals { get; set; }
    }
}
