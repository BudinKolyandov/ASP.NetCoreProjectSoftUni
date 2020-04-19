namespace BugTracker.Web.ViewModels.Companies
{
    using System.Collections.Generic;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class DetailsCompanyViewModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public virtual IEnumerable<ProjectCompanyViewModel> Projects { get; set; }
    }
}
