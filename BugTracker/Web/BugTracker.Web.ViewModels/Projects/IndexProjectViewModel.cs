namespace BugTracker.Web.ViewModels.Projects
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class IndexProjectViewModel : IMapFrom<Project>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        [DisplayName("Closed Bugs")]
        public int ClosedBugsCount => this.Bugs.Where(x => x.Status == Data.Models.Enums.Status.Closed).Count();

        [DisplayName("Active Bugs")]
        public int ActiveBugsCount => this.Bugs.Where(x => x.Status != Data.Models.Enums.Status.Closed).Count();

        public ICollection<Bug> Bugs { get; set; }
    }
}
