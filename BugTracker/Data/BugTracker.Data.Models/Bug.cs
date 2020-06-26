namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models.Enums;

    public class Bug
    {
        public Bug()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Assignments = new HashSet<Assignment>();
            this.BugHistories = new HashSet<BugHistory>();
        }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(160)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public string ReporterId { get; set; }

        public virtual User Reporter { get; set; }

        public Priority Priority { get; set; }

        public Severity Severity { get; set; }

        public Status Status { get; set; }

        public DateTime DueDate { get; set; }

        public virtual ICollection<BugHistory> BugHistories { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
