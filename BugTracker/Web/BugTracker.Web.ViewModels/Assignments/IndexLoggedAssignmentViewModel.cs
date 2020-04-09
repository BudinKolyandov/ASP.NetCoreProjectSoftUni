namespace BugTracker.Web.ViewModels.Assignments
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Data.Models.Enums;
    using BugTracker.Services.Mapping;

    public class IndexLoggedAssignmentViewModel : IMapFrom<Assignment>
    {
        public string AssignedByFullName { get; set; }

        public string BugName { get; set; }

        public string BugId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        public Priority Priority { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
    }
}
