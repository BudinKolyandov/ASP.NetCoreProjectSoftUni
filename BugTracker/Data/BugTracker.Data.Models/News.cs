namespace BugTracker.Data.Models
{
    using System;

    public class News
    {
        public int Id { get; set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public string PosterId { get; set; }

        public User User { get; set; }

        public string Headline { get; set; }

        public string Body { get; set; }

        public DateTime DatePosted { get; set; }
    }
}
