namespace BugTracker.Web.ViewModels.Bugs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class DetailsBugsViewModel : IMapFrom<Bug>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ProjectId { get; set; }

        [Display(Name = "Reported By")]
        public string ReporterFullName { get; set; }

        public string Priority { get; set; }

        public string Severity { get; set; }

        public string Status { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        public virtual ICollection<AssigmnentsDetailsViewModel> Assignments { get; set; }
    }
}
