namespace BugTracker.Web.ViewModels.Projects
{
    using BugTracker.Data.Models;
    using System.Collections.Generic;

    public class DetailsProjectViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public ICollection<Bug> Bugs { get; set; }

        public ICollection<ProjectUser> Developers { get; set; }
    }
}
