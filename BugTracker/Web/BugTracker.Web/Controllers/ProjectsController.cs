namespace BugTracker.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using BugTracker.Services.Projects;
    using BugTracker.Web.ViewModels;
    using BugTracker.Web.ViewModels.Projects;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectsService service;

        public ProjectsController(IProjectsService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = this.User.Identity.Name;
            var viewModel = await this.service.GetAll(userEmail);
            if (viewModel == null)
            {
                return this.RedirectToAction("Index", "Companies");
            }

            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string companyName, AddProjectViewModel projectViewModel)
        {
            var username = this.User.Identity.Name;
            var project = await this.service.Create(projectViewModel.Name, projectViewModel.Status, projectViewModel.Description, username);
            if (project == null)
            {
                return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var project = await this.service.GetProjectDetails(id);
            if (project == null)
            {
                return this.NotFound();
            }

            return this.View(project);
        }

        public async Task<IActionResult> Join(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var project = await this.service.GetProjectJoin(id);
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
            var result = this.service.Join(userEmail, model);
            return this.Redirect($"/Projects/Details/{result}");
        }

        public async Task<IActionResult> Report(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var project = await this.service.GetProjectReport(id);
            if (project == null)
            {
                return this.NotFound();
            }

            return this.View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Report(ReportBugProjectViewModel model)
        {
            var userEmail = this.User.Identity.Name;
            var result = await this.service.Report(userEmail, model);
            return this.Redirect($"/Projects/Details/{model.ProjectId}");
        }
    }
}
