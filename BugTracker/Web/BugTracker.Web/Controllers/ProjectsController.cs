namespace BugTracker.Web.Controllers
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

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyCheck = this.projectsService.UserHasCompany(user.UserName);
            if (!companyCheck)
            {
                this.TempData["message"] = "You need to join a Company to view it's projects";
                return this.RedirectToAction("Index", "Companies");
            }

            var viewModel = new IndexViewModel
            {
                Projects = this.projectsService.GetAll<IndexProjectViewModel>(user.UserName),
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var companies = this.companiesService.GetAll<CreateProjectCompaniesListModel>();
            var viewModel = new AddProjectViewModel
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
        public async Task<IActionResult> Create(AddProjectViewModel projectViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(projectViewModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var project = await this.projectsService.Create(projectViewModel.ProjectName, projectViewModel.Status, projectViewModel.Description, user.UserName, projectViewModel.Name);
            if (project == null)
            {
                return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
            }

            return this.RedirectToAction(nameof(this.Details), new { id = project.Id });
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var project = this.projectsService.GetById<DetailsProjectViewModel>(id);
            if (project == null)
            {
                return this.NotFound();
            }

            return this.View(project);
        }

        public IActionResult Join(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var project = this.projectsService.GetById<JoinProjectViewModel>(id);
            if (project == null)
            {
                return this.NotFound();
            }

            return this.View(project);
        }

        [HttpPost]
        public IActionResult Join(JoinProjectViewModel model)
        {
            var userEmail = this.User.Identity.Name;
            var result = this.projectsService.Join(userEmail, model);
            return this.Redirect($"/Projects/Details/{result}");
        }

        public IActionResult Report(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var project = this.projectsService.GetById<DetailsProjectViewModel>(id);
            if (project == null)
            {
                return this.NotFound();
            }

            var viewModel = new ReportBugProjectInputModel
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Report(ReportBugProjectInputModel model)
        {
            var userEmail = this.User.Identity.Name;
            var result = await this.projectsService.Report(userEmail, model);
            return this.Redirect($"/Projects/Details/{result.ProjectId}");
        }

        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

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
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
