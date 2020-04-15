namespace BugTracker.Web.Controllers
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

        public IActionResult Index(int page = 1)
        {
            var viewModel = new IndexViewModel
            {
                Companies = this.service.GetAllPaged<IndexCompanyViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage),
            };

            var count = this.service.GetCount();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;
            return this.View(viewModel);
        }

        public IActionResult Details(string id)
        {
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

        public IActionResult Join(string id)
        {
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
            if (!this.ModelState.IsValid)
            {
                return this.View(company);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var companyId = await this.service.Join(user.UserName, id);
            if (companyId == null)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("Details", "Companies", new { id = companyId });
        }
    }
}
