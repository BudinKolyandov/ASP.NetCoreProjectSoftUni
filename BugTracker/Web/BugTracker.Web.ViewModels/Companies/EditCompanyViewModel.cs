namespace BugTracker.Web.ViewModels.Companies
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class EditCompanyViewModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        [Required]
        [DisplayName("Name*")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Description*")]
        public string Description { get; set; }

        public virtual ICollection<ProjectCompanyViewModel> Projects { get; set; }
    }
}
