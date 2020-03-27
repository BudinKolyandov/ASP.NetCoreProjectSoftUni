namespace BugTracker.Web.ViewModels.Projects
{
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class CreateProjectCompaniesListModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
