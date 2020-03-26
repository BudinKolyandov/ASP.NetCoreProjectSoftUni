namespace BugTracker.Web.Controllers
{
    using BugTracker.Services.Bugs;
    using BugTracker.Web.ViewModels.Bugs;
    using Microsoft.AspNetCore.Mvc;

    public class BugsController : Controller
    {
        private readonly IBugsService service;

        public BugsController(IBugsService service)
        {
            this.service = service;
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var bug = this.service.GetById<DetailsBugsViewModel>(id);
            if (bug == null)
            {
                return this.NotFound();
            }

            return this.View(bug);
        }
    }
}
