﻿namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BugTracker.Data.Models.Enums;

    public class Assignment
    {
        public Assignment()
        {
            this.Assignees = new HashSet<AssignmentUser>();
        }

        public int Id { get; set; }

        public string AssignedById { get; set; }

        public virtual User AssignedBy { get; set; }

        public string BugId { get; set; }

        public Bug Bug { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public Priority Priority { get; set; }

        public DateTime DueDate { get; set; }

        public bool Completed { get; set; }

        public virtual ICollection<AssignmentUser> Assignees { get; set; }
    }
}
