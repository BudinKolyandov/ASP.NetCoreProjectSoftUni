namespace BugTracker.Web.ViewModels.Bugs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Data.Models.Enums;
    using BugTracker.Services.Mapping;

    public class EditBugViewModel : IMapFrom<Bug>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [Display(Name = "Name*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [Display(Name = "Description*")]
        public string Description { get; set; }

        public string ProjectId { get; set; }

        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Priority*")]
        public Priority Priority { get; set; }

        [Required]
        [Display(Name = "Severity*")]
        public Severity Severity { get; set; }

        [Required]
        [Display(Name = "Status*")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "The Due date field is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Due date*")]
        public DateTime DueDate { get; set; }
    }
}
