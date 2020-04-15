namespace BugTracker.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Services.Company;
    using BugTracker.Services.Projects;
    using BugTracker.Web.ViewModels;
    using BugTracker.Web.ViewModels.Projects;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Administration")]
    public class ProjectsController : Controller
    {
        private readonly IProjectsService projectsService;
        private readonly ICompaniesService companiesService;
        private readonly UserManager<User> userManager;

        public ProjectsController(
            IProjectsService projectsService,
            ICompaniesService companiesService,
            UserManager<User> userManager)
        {
            this.projectsService = projectsService;
            this.companiesService = companiesService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            var userId = this.userManager.GetUserId(this.User);
            var companies = this.companiesService.GetAllForAdminUser<CreateProjectCompaniesListModel>(userId);
            var viewModel = new AddProjectInputModel
            {
                CompaniesList = new List<CreateProjectCompaniesListModel>(),
            };
            foreach (var company in companies)
            {
                viewModel.CompaniesList.Add(company);
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddProjectInputModel projectViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(projectViewModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var project = await this.projectsService.Create(projectViewModel.ProjectName, projectViewModel.Description, user.UserName, projectViewModel.Name);
            if (project == null)
            {
                return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
            }

            return this.RedirectToAction("Details", "Projects", new { id = project.Id, area = string.Empty });
        }

        public IActionResult Delete(string id)
        {
            var company = this.projectsService.GetById<DeleteProjectViewModel>(id);
            if (company == null)
            {
                return this.NotFound();
            }

            return this.View(company);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await this.projectsService.DeleteProject(id);
            return this.RedirectToAction("AdminIndex", "Companies");
        }
    }
}
