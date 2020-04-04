namespace BugTracker.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Services.Assignments;
    using BugTracker.Web.ViewModels.Assignments;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class AssignmentsController : Controller
    {
        private readonly IAssignmentsService assignmentsService;
        private readonly UserManager<User> userManager;

        public AssignmentsController(
            IAssignmentsService assignmentsService,
            UserManager<User> userManager)
        {
            this.assignmentsService = assignmentsService;
            this.userManager = userManager;
        }

        public IActionResult Create(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var viewModel = new CreateAssignmentInputModel
            {
                Users = new List<CreateAssignnmentUserViewModel>(),
            };
            viewModel.Users = this.assignmentsService.GetAllUsersInCompany<CreateAssignnmentUserViewModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAssignmentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.userManager.GetUserId(this.User);
            var assignmentId = await this.assignmentsService.CreateAssignnment(userId, model);
            if (assignmentId == 0)
            {
                return this.BadRequest();
            }
            else
            {
                return this.RedirectToAction("Index", "Projects", new { area = string.Empty });
            }
        }
    }
}
