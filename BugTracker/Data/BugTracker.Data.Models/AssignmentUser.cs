namespace BugTracker.Data.Models
{
    public class AssignmentUser
    {
        public int AssignmentId { get; set; }

        public Assignment Assignment { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public bool Completed { get; set; }
    }
}
