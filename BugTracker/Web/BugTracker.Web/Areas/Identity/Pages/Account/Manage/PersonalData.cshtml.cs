namespace BugTracker.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    public class PersonalData : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly ILogger<PersonalData> logger;

        public PersonalData(
            UserManager<User> userManager,
            ILogger<PersonalData> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            return this.Page();
        }
    }
}
