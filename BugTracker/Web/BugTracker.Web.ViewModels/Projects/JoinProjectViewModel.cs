﻿namespace BugTracker.Web.ViewModels.Projects
{
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class JoinProjectViewModel : IMapFrom<Project>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
