namespace BugTracker.Web.ViewModels.Projects
{
    using System.Collections.Generic;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class DetailsProjectViewModel : IMapFrom<Project>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public ICollection<Bug> Bugs { get; set; }

        public ICollection<ProjectUser> Developers { get; set; }
    }
}
