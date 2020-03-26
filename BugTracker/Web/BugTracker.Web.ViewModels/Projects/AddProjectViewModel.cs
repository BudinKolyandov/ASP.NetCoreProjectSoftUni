namespace BugTracker.Web.ViewModels.Projects
{
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class AddProjectViewModel : IMapFrom<Project>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }
    }
}
