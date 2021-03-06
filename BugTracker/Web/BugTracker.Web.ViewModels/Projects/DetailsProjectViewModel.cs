﻿namespace BugTracker.Web.ViewModels.Projects
{
    using System.Collections.Generic;
    using System.Linq;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class DetailsProjectViewModel : IMapFrom<Project>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int ClosedBugsCount { get; set; }

        public int ActiveBugsCount { get; set; }

        public IEnumerable<DetailsProjectBugViewModel> Bugs { get; set; }
    }
}
