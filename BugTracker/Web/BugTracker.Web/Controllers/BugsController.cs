using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Services.Bugs;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Web.Controllers
{
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
                return NotFound();
            }

            var project = await this.service.GetBugDetails(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
    }
}