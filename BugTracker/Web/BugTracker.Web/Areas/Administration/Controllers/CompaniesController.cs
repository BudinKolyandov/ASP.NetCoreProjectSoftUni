namespace BugTracker.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Services.Company;
    using BugTracker.Web.ViewModels.Companies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Administration")]
    public class CompaniesController : Controller
    {
        private const int ItemsPerPage = 3;

        private readonly ICompaniesService service;
        private readonly UserManager<User> userManager;

        public CompaniesController(
            ICompaniesService service,
            UserManager<User> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        public IActionResult AdminIndex(int page = 1)
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = new IndexViewModel
            {
                Companies = this.service.GetAllForAdminUser<IndexCompanyViewModel>(userId, ItemsPerPage, (page - 1) * ItemsPerPage),
            };

            var count = this.service.GetCountForAdmin(userId);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;
            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCompanyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var companyId = await this.service.Create(input, user.Id);

            if (companyId == null)
            {
                this.TempData["message"] = "A company with the same name already exists";
                return this.RedirectToAction("Index", "Companies");
            }

            return this.RedirectToAction("Details", "Companies", new { area = string.Empty, id = companyId });
        }

        public async Task<IActionResult> Requests()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var requests = this.service.GetAllForUsersForAproval<RequestsToJoinViewModel>(user.Id);
            return this.View(requests);
        }

        [HttpPost]
        public async Task<IActionResult> Requests(string companyId, string userId)
        {
            if (companyId == null || userId == null)
            {
                return this.NotFound();
            }

            var requests = await this.service.Aprove(userId, companyId);
            if (requests == null)
            {
                return this.NotFound();
            }

            return this.RedirectToAction("Requests");
        }

        public IActionResult Edit(string id)
        {
            var company = this.service.GetById<EditCompanyViewModel>(id);
            if (company == null)
            {
                return this.NotFound();
            }

            return this.View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCompanyInputModel company)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(company);
            }

            if (!this.service.CompanyExists(company.Id))
            {
                return this.NotFound();
            }

            await this.service.EditCompany(company);
            return this.RedirectToAction(nameof(this.AdminIndex));
        }

        public IActionResult Delete(string id)
        {
            var company = this.service.GetById<DeleteCompanyViewModel>(id);
            if (company == null)
            {
                return this.NotFound();
            }

            return this.View(company);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await this.service.DeleteCompany(id);
            return this.RedirectToAction(nameof(this.AdminIndex));
        }
    }
}
