namespace BugTracker.Web.ViewModels.Bugs
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class AddBuggChangeInputModel : IMapFrom<Bug>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Name of the Changed Value*")]
        [Required]
        public string ChangedValueName { get; set; }

        [Required]
        [DisplayName("Old Value*")]
        public string OldValue { get; set; }

        [Required]
        [DisplayName("New Value*")]
        public string NewValue { get; set; }
    }
}
