namespace BugTracker.Services.Projects
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTracker.Web.ViewModels.Projects;

    public interface IProjectsService
    {
        Task<AddProjectViewModel> Create(string name, string status, string description, string username);

        Task<List<AllProjectsViewModel>> GetAll(string userEmail);

        Task<DetailsProjectViewModel> GetProjectDetails(string id);

        string Join(string userEmail, JoinProjectViewModel model);

        Task<JoinProjectViewModel> GetProjectJoin(string id);

        Task<ReportBugProjectViewModel> GetProjectReport(string id);

        Task<ReportBugProjectViewModel> Report(string userEmail, ReportBugProjectViewModel model);
    }
}
