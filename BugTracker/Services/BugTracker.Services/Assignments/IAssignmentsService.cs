namespace BugTracker.Services.Assignments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTracker.Web.ViewModels.Assignments;

    public interface IAssignmentsService
    {
        IEnumerable<T> GetAllUsersInCompany<T>(string bugId, int? count = null);

        Task<int> CreateAssignnment(string userId, CreateAssignmentInputModel model);

        IEnumerable<T> GetAllForUser<T>(string userId, int? count = null);
    }
}
