namespace BugTracker.Web.ViewModels.Companies
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public string UserId { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<IndexCompanyViewModel> Companies { get; set; }
    }
}
