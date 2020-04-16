namespace BugTracker.Services.Projects
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTracker.Web.ViewModels.Projects;

    public interface IProjectsService
    {
        Task<AddProjectInputModel> Create(string name, string description, string username, string companyId);

        IEnumerable<T> GetAllProjectsByUserEmail<T>(string userEmail, int? take = null, int skip = 0);

        bool UserHasCompany(string username);

        Task DeleteProject(string id);

        T GetById<T>(string id);

        IEnumerable<T> GetByIdWithBugs<T>(string id, int? take = null, int skip = 0);

        IEnumerable<T> GetByIdWithClosedBugs<T>(string id, int? take = null, int skip = 0);

        string Join(string userEmail, JoinProjectViewModel model);

        Task<ReportBugProjectInputModel> Report(string userEmail, ReportBugProjectInputModel model);

        public int GetCount();
    }
}
