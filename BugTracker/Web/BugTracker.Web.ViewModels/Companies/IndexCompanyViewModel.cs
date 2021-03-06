﻿namespace BugTracker.Web.ViewModels.Companies
{
    using System.Collections.Generic;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class IndexCompanyViewModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ProjectCompanyViewModel> Projects { get; set; }
    }
}
