namespace BugTracker.Web.ViewModels.Companies
{
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class EditCompanyInputModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
