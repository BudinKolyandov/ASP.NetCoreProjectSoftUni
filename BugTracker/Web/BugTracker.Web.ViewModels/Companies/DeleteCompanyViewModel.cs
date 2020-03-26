namespace BugTracker.Web.ViewModels.Companies
{
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class DeleteCompanyViewModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
