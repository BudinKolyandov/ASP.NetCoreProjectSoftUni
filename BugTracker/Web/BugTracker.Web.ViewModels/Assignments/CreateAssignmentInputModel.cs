namespace BugTracker.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Data.Models.Enums;
    using BugTracker.Services.Mapping;

    public class CreateAssignmentInputModel : IMapFrom<Assignment>
    {
        public string BugId { get; set; }

        [Required]
        [DisplayName("Title*")]
        [MinLength(3)]
        [MaxLength(30)]

        public string Title { get; set; }

        [Required]
        [DisplayName("Description*")]
        [MinLength(3)]
        [MaxLength(160)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Priority*")]
        public Priority Priority { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Due date*")]
        public DateTime DueDate { get; set; }

        [Required]
        [DisplayName("Assign to:*")]
        public string AssigneeId { get; set; }

        public virtual IEnumerable<CreateAssignnmentUserViewModel> Users { get; set; }
    }
}
