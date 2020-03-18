namespace BugTracker.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Services.Bugs;
    using Microsoft.AspNetCore.Mvc;

    public class BugsController : Controller
    {
        private readonly IBugsService service;

        public BugsController(IBugsService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var bug = await this.service.GetBugDetails(id);
            if (bug == null)
            {
                return this.NotFound();
            }

            return this.View(bug);
        }
    }
}
