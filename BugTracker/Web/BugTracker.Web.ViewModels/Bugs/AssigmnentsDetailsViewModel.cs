namespace BugTracker.Web.ViewModels.Bugs
{
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class AssigmnentsDetailsViewModel : IMapFrom<Assignment>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AssignedByUserName { get; set; }

        public bool Completed { get; set; }
    }
}
