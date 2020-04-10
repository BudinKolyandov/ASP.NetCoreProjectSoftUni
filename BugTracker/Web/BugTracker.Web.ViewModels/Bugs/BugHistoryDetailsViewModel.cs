namespace BugTracker.Web.ViewModels.Bugs
{
    using System;
    using System.ComponentModel;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class BugHistoryDetailsViewModel : IMapFrom<BugHistory>
    {
        public string BugId { get; set; }

        [DisplayName("Field Name")]
        public string FieldName { get; set; }

        [DisplayName("Old Value")]
        public string OldDescriptionValue { get; set; }

        [DisplayName("New Value")]
        public string NewDescriptionValue { get; set; }

        [DisplayName("Modified On")]
        public DateTime ModifiedOn { get; set; }
    }
}
