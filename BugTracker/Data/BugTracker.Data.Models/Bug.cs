namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BugTracker.Data.Models.Enums;

    public class Bug
    {
        public Bug()
        {
            this.Handlers = new HashSet<User>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public string ReporterId { get; set; }

        public User Reporter { get; set; }

        public Priority Priority { get; set; }

        public Severity Severity { get; set; }

        public Status Status { get; set; }

        public DateTime DueDate { get; set; }

        public ICollection<User> Handlers { get; set; }
    }
}
