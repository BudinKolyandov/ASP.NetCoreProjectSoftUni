namespace BugTracker.Web.Controllers
{
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Services.News;
    using BugTracker.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class SeenController : ControllerBase
    {
        private readonly INewsService newsService;
        private readonly UserManager<User> userManager;

        public SeenController(
            INewsService newsService,
            UserManager<User> userManager)
        {
            this.newsService = newsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<NewsResponseModel>> Seen(NewsInputModel inputModel)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.newsService.SeenChange(inputModel.NewsId, userId);
            return new NewsResponseModel { NewsId = inputModel.NewsId };
        }
    }
}
