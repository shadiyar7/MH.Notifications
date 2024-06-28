namespace MH.Notifications.Core.Entities
{
    public class Notification
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Message { get; set; }
        public DateTime? SentAt { get; set; }
        public bool IsSent { get; set; }
    }
}
