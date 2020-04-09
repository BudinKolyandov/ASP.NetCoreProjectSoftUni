namespace BugTracker.Web.Controllers
{
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Services.News;
    using BugTracker.Services.Projects;
    using BugTracker.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class NewsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly INewsService newsService;
        private readonly IProjectsService projectsService;

        public NewsController(
            UserManager<User> userManager,
            INewsService newsService,
            IProjectsService projectsService)
        {
            this.userManager = userManager;
            this.newsService = newsService;
            this.projectsService = projectsService;
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = new CreateNewsInputModel
            {
                Projects = this.projectsService.GetAllProjectsByUserEmail<CreateNewsProjectInputModel>(user.Email),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var newsId = await this.newsService.CreateNews(user.Id, model);
            if (newsId <= 0)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("IndexLogged", "Home");
        }
    }
}
