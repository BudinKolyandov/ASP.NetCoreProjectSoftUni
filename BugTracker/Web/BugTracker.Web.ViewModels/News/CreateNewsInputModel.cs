namespace BugTracker.Web.ViewModels.News
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTracker.Services.Mapping;

    public class CreateNewsInputModel : IMapFrom<BugTracker.Data.Models.News>
    {
        [Required]
        [Display(Name = "Title of the news*")]
        public string Headline { get; set; }

        [Required]
        [Display(Name = "Content of the news*")]
        public string Body { get; set; }

        public string ProjectId { get; set; }

        public virtual IEnumerable<CreateNewsProjectInputModel> Projects { get; set; }
    }
}
