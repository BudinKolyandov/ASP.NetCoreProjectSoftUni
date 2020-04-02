namespace BugTracker.Web.ViewModels.Companies
{
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class DeleteCompanyViewModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Projects Count")]
        public int ProjectsCount { get; set; }
    }
}
