namespace BugTracker.Data.Models
{
    public class ProjectUser
    {
        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

    }
}
