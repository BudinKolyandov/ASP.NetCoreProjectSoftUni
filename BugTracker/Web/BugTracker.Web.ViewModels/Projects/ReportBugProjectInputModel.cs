namespace BugTracker.Web.ViewModels.Projects
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Data.Models.Enums;
    using BugTracker.Services.Mapping;

    public class ReportBugProjectInputModel : IMapFrom<Bug>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ProjectId { get; set; }

        public string ProjectName { get; set; }

        public Priority Priority { get; set; }

        public Severity Severity { get; set; }

        public Status Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
    }
}
