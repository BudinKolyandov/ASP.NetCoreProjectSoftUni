namespace BugTracker.Web.Areas.Administration.Controllers
{
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
        private readonly ICompaniesService service;
        private readonly UserManager<User> userManager;

        public CompaniesController(
            ICompaniesService service,
            UserManager<User> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        public IActionResult AdminIndex()
        {
            var user = this.userManager.GetUserId(this.User);
            var viewModel = new IndexViewModel
            {
                Companies = this.service.GetAllForUser<IndexCompanyViewModel>(user),
            };
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

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

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
            if (!this.service.CompanyExists(company.Id))
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(company);
            }

            await this.service.EditCompany(company);
            return this.RedirectToAction(nameof(this.AdminIndex));
        }

        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

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
