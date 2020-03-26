namespace BugTracker.Web.Controllers
{
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Services.Company;
    using BugTracker.Web.ViewModels.Companies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
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

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Companies = this.service.GetAll<IndexCompanyViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var company = this.service.GetById<DetailsCompanyViewModel>(id);
            if (company == null)
            {
                return this.NotFound();
            }

            return this.View(company);
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

            return this.RedirectToAction(nameof(this.Details), new { id = companyId });
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
                return this.RedirectToAction(nameof(this.Index));
            }

            await this.service.EditCompany(company);
            return this.RedirectToAction(nameof(this.Details), new { id = company.Id });
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
            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult Join(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var company = this.service.GetById<JoinCompanyViewModel>(id);
            if (company == null)
            {
                return this.NotFound();
            }

            return this.View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Join(string id, JoinCompanyViewModel company)
        {
            if (id != company.Id)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var result = await this.service.Join(user.UserName, id);
            if (result == null)
            {
                return this.View(company);
            }

            return this.Redirect("/Projects/Index");
        }
    }
}
