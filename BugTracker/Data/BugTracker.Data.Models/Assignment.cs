namespace BugTracker.Data.Models
{
    using System;

    using BugTracker.Data.Models.Enums;

    public class Assignment
    {
        public int Id { get; set; }

        public string AssigneeId { get; set; }

        public virtual User Assignee { get; set; }

        public string BugId { get; set; }

        public Bug Bug { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public Priority Priority { get; set; }

        public DateTime DueDate { get; set; }
    }
}
