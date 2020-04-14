namespace BugTracker.Web.ViewModels.Assignments
{
    using System;

    using BugTracker.Services.Mapping;

    public class IndexLoggedNewsViewModel : IMapFrom<BugTracker.Data.Models.News>
    {
        public int Id { get; set; }

        public string ProjectName { get; set; }

        public string UserFullName { get; set; }

        public string Headline { get; set; }

        public string Body { get; set; }

        public DateTime DatePosted { get; set; }
    }
}
