namespace BugTracker.Web.ViewModels.Projects
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<IndexProjectViewModel> Projects { get; set; }
    }
}
