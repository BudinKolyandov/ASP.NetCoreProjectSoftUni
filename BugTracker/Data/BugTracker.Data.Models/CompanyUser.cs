namespace BugTracker.Data.Models
{
    public class CompanyUser
    {
        public string CompanyId { get; set; }

        public Company Company { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
