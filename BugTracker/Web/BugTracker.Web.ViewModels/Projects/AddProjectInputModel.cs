namespace BugTracker.Web.ViewModels.Projects
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class AddProjectInputModel : IMapFrom<Project>
    {
        public string Id { get; set; }

        [Required]
        [DisplayName("Project Name*")]
        public string ProjectName { get; set; }

        [Required]
        [DisplayName("Description*")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Company Name*")]
        public string Name { get; set; }

        public List<CreateProjectCompaniesListModel> CompaniesList { get; set; }
    }
}
