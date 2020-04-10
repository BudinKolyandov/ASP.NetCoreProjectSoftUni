namespace BugTracker.Web.ViewModels.Companies
{
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;

    public class RequestsToJoinViewModel : IMapFrom<JoinRequest>
    {
        public string UserId { get; set; }

        public string UserFullName { get; set; }

        public string CompanyId { get; set; }

        public string CompanyName { get; set; }
    }
}
