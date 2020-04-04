namespace BugTracker.Web.ViewModels.Assignments
{
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class CreateAssignnmentUserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string FullName { get; set; }
    }
}
