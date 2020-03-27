namespace BugTracker.Web.ViewModels.Bugs
{
    using System;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class DetailsBugsViewModel : IMapFrom<Bug>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ProjectId { get; set; }

        public string ReporterId { get; set; }

        public string Priority { get; set; }

        public string Severity { get; set; }

        public string Status { get; set; }

        public DateTime DueDate { get; set; }
    }
}
