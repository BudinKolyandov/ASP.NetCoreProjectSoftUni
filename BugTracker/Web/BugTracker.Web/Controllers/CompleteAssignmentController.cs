namespace BugTracker.Web.Controllers
{
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Services.Assignments;
    using BugTracker.Web.ViewModels.Assignments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CompleteAssignmentController : ControllerBase
    {
        private readonly IAssignmentsService newsService;
        private readonly UserManager<User> userManager;

        public CompleteAssignmentController(
            IAssignmentsService newsService,
            UserManager<User> userManager)
        {
            this.newsService = newsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AssignmentResponseModel>> Complete(AssignmentInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            var result = await this.newsService.CompleteAssignment(inputModel.AssignmentId, userId);
            if (result == 0)
            {
                return this.BadRequest();
            }

            return new AssignmentResponseModel { AssignmentId = result };
        }
    }
}
