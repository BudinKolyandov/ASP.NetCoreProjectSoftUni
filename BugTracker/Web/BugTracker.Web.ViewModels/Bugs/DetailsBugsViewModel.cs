using System;

namespace BugTracker.Web.ViewModels.Bugs
{
    public class DetailsBugsViewModel
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
