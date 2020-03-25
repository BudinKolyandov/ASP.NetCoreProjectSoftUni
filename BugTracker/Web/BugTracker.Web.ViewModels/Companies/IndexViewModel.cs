namespace BugTracker.Web.ViewModels.Companies
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexCompanyViewModel> Companies { get; set; }
    }
}
