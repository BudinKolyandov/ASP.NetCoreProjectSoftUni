namespace BugTracker.Web.ViewModels.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Data.Models.Enums;
    using BugTracker.Services.Mapping;

    public class CreateAssignmentInputModel : IMapFrom<Assignment>
    {
        public string BugId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public string AssigneeId { get; set; }

        public virtual IEnumerable<CreateAssignnmentUserViewModel> Users { get; set; }
    }
}
