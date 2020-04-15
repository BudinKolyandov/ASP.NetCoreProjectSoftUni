namespace BugTracker.Web.Controllers
{
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
        private readonly IProjectsService projectsService;
        private readonly UserManager<User> userManager;

        public ProjectsController(
            IProjectsService projectsService,
            UserManager<User> userManager)
        {
            this.projectsService = projectsService;
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
                Projects = this.projectsService.GetAllProjectsByUserEmail<IndexProjectViewModel>(user.UserName),
            };

            return this.View(viewModel);
        }

        public IActionResult Details(string id)
        {
            var project = this.projectsService.GetById<DetailsProjectViewModel>(id);
            if (project == null)
            {
                return this.NotFound();
            }

            var completedBugs = project.Bugs
                .Where(x => x.Status == Data.Models.Enums.Status.Closed).ToList();
            project.Bugs = project.Bugs
                .Where(x => x.Status != Data.Models.Enums.Status.Closed)
                .OrderBy(x => x.Priority)
                .ThenBy(x => x.Severity)
                .ToList();
            foreach (var completedBug in completedBugs)
            {
                project.Bugs.Add(completedBug);
            }

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
