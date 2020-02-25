namespace BugTracker.Web.ViewModels.Projects
{
    using System;

    public class ReportBugProjectViewModel
    {
        public string Name { get; set; }

        public string ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Priority { get; set; }
        
        public string Severity { get; set; }

        public string Status { get; set; }

        public DateTime DueDate { get; set; }
    }
}
