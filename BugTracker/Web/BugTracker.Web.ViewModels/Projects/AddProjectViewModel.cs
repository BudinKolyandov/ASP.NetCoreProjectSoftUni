namespace BugTracker.Web.ViewModels.Projects
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class AddProjectViewModel : IMapFrom<Project>
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Status { get; set; }

        public string Description { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public List<CreateProjectCompaniesListModel> CompaniesList { get; set; }
    }
}
