namespace BugTracker.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Services.Assignments;
    using BugTracker.Services.News;
    using BugTracker.Web.ViewModels;
    using BugTracker.Web.ViewModels.Assignments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IAssignmentsService assignmentsService;
        private readonly INewsService newsService;

        public HomeController(
            UserManager<User> userManager,
            IAssignmentsService assignmentsService,
            INewsService newsService)
        {
            this.userManager = userManager;
            this.assignmentsService = assignmentsService;
            this.newsService = newsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult IndexLogged()
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = new IndexLoggedViewModel
            {
                Assignments = this.assignmentsService.GetAllForUser<IndexLoggedAssignmentViewModel>(userId),
                News = this.newsService.GetAllForUser<IndexLoggedNewsViewModel>(userId),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
