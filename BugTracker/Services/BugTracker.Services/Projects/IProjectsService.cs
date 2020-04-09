namespace BugTracker.Services.Projects
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTracker.Web.ViewModels.Projects;

    public interface IProjectsService
    {
        Task<AddProjectInputModel> Create(string name, string status, string description, string username, string companyId);

        IEnumerable<T> GetAllProjectsByUserEmail<T>(string userEmail, int? count = null);

        bool UserHasCompany(string username);

        Task DeleteProject(string id);

        T GetById<T>(string id);

        string Join(string userEmail, JoinProjectViewModel model);

        Task<ReportBugProjectInputModel> Report(string userEmail, ReportBugProjectInputModel model);
    }
}
