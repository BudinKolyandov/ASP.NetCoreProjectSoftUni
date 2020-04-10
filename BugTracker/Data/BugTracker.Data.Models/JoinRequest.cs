namespace BugTracker.Data.Models
{
    public class JoinRequest
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string AdminId { get; set; }

        public User Admin { get; set; }

        public string CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
