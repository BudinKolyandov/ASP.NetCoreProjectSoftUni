namespace BugTracker.Web.Controllers
{
    using System.Threading.Tasks;

    using BugTracker.Services.Bugs;
    using BugTracker.Web.ViewModels.Bugs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class BugsController : Controller
    {
        private readonly IBugsService service;

        public BugsController(IBugsService service)
        {
            this.service = service;
        }

        public IActionResult Details(string id)
        {
            var bug = this.service.GetById<DetailsBugsViewModel>(id);
            if (bug == null)
            {
                return this.NotFound();
            }

            return this.View(bug);
        }

        public IActionResult Edit(string id)
        {
            var bug = this.service.GetById<EditBugViewModel>(id);
            if (bug == null)
            {
                return this.NotFound();
            }

            return this.View(bug);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBugViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var editedBugId = await this.service.EditBug(model);
            return this.RedirectToAction("Details", "Bugs", new { id = editedBugId });
        }

        public IActionResult AddChange(string id)
        {
            var bug = this.service.GetById<AddBuggChangeInputModel>(id);
            if (bug == null)
            {
                return this.NotFound();
            }

            return this.View(bug);
        }

        [HttpPost]
        public async Task<IActionResult> AddChange(AddBuggChangeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var changedBugId = await this.service.AddBugChange(model);
            return this.RedirectToAction("Details", "Bugs", new { id = changedBugId });
        }
    }
}
