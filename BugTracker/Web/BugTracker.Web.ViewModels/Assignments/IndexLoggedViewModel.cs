namespace BugTracker.Web.ViewModels.Assignments
{
    using System.Collections.Generic;

    public class IndexLoggedViewModel
    {
        public IEnumerable<IndexLoggedAssignmentViewModel> Assignments { get; set; }

        public IEnumerable<IndexLoggedNewsViewModel> News { get; set; }
    }
}
