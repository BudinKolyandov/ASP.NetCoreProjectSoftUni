namespace BugTracker.Web.ViewModels.Companies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class EditCompanyViewModel : IMapFrom<Company>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
