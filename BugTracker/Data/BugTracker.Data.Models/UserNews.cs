namespace BugTracker.Data.Models
{
    public class UserNews
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int NewsId { get; set; }

        public News News { get; set; }

        public bool Seen { get; set; }
    }
}
