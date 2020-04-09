namespace BugTracker.Web.ViewModels.Projects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Data.Models.Enums;
    using BugTracker.Services.Mapping;

    public class DetailsProjectBugViewModel : IMapFrom<Bug>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public Severity Severity { get; set; }

        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public string Assigned => this.Assignments.Count > 0 ? "Yes" : "No";
    }
}
