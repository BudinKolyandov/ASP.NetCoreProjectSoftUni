namespace BugTracker.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Services.Projects;
    using BugTracker.Web.ViewModels.Projects;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ProjectsController : Controller
    {
        private const int ItemsPerPage = 5;

        private readonly IProjectsService projectsService;
        private readonly UserManager<User> userManager;

        public ProjectsController(
            IProjectsService projectsService,
            UserManager<User> userManager)
        {
            this.projectsService = projectsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
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
                Projects = this.projectsService.GetAllProjectsByUserEmail<IndexProjectViewModel>(user.UserName, ItemsPerPage, (page - 1) * ItemsPerPage),
            };

            var count = this.projectsService.GetCount();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;
            return this.View(viewModel);
        }

        public IActionResult Details(string id, int page = 1, int? take = null, int skip = 0)
        {
            var project = this.projectsService.GetById<DetailsProjectViewModel>(id);
            if (project == null)
            {
                return this.NotFound();
            }

            if (take.HasValue)
            {
                project.Bugs = project.Bugs
                    .OrderBy(x => x.Priority)
                    .ThenBy(x => x.Severity)
                    .Skip(skip)
                    .Take(take.Value)
                    .ToList();
            }
            else
            {
                project.Bugs = project.Bugs
                    .OrderBy(x => x.Priority)
                    .ThenBy(x => x.Severity)
                    .Skip(skip)
                    .ToList();
            }

            var count = project.Bugs.Count();
            project.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (project.PagesCount == 0)
            {
                project.PagesCount = 1;
            }

            project.CurrentPage = page;

            return this.View(project);
        }

        public IActionResult Report(string id)
        {
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
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userEmail = this.User.Identity.Name;
            var result = await this.projectsService.Report(userEmail, model);
            return this.Redirect($"/Projects/Details/{result.ProjectId}");
        }
    }
}
