namespace BugTracker.Services.Projects
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTracker.Web.ViewModels.Projects;

    public interface IProjectsService
    {
        Task<AddProjectViewModel> Create(string name, string status, string description, string username);

        IEnumerable<T> GetAll<T>(string userEmail, int? count = null);

        T GetById<T>(string id);

        string Join(string userEmail, JoinProjectViewModel model);

        Task<ReportBugProjectInputModel> Report(string userEmail, ReportBugProjectInputModel model);
    }
}
