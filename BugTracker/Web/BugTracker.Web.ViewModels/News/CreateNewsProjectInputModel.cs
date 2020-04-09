namespace BugTracker.Web.ViewModels.News
{
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class CreateNewsProjectInputModel : IMapFrom<Project>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
