namespace BugTracker.Web.ViewModels.Projects
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexProjectViewModel> Projects { get; set; }
    }
}
