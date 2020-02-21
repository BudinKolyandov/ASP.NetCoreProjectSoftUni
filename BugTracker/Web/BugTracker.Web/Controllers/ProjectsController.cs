using BugTracker.Services.Projects;
using BugTracker.Web.ViewModels;
using BugTracker.Web.ViewModels.Projects;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BugTracker.Web.Controllers
{
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
            return View(await this.service.GetAll(userEmail));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string companyName, [Bind("Name,Status,Description")] AddProjectViewModel projectViewModel)
        {
            var username =  this.User.Identity.Name;
            var project = await this.service.Create(projectViewModel.Name, projectViewModel.Status, projectViewModel.Description, username);
            if (project == null)
            {
                return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await this.service.GetProject(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
    }
}