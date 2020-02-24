using BugTracker.Web.ViewModels.Projects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Services.Projects
{
    public interface IProjectsService
    {
        Task<AddProjectViewModel> Create(string name, string status, string description, string username);

        Task<List<AllProjectsViewModel>> GetAll(string userEmail);

        Task<DetailsProjectViewModel> GetProjectDetails(string id);

        string Join(string userEmail, JoinProjectViewModel model);

        Task<JoinProjectViewModel> GetProjectJoin(string id);
    }
}
