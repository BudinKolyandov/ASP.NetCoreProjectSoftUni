namespace BugTracker.Web.ViewModels.Bugs
{
    using System;
    using System.ComponentModel;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class BugHistoryDetailsViewModel : IMapFrom<BugHistory>
    {
        public string BugId { get; set; }

        [DisplayName("Value Name")]
        public string ChangedValueName { get; set; }

        [DisplayName("Old Value")]
        public string OldValue { get; set; }

        [DisplayName("New Value")]
        public string NewValue { get; set; }

        [DisplayName("Modified On")]
        public DateTime ModifiedOn { get; set; }
    }
}
