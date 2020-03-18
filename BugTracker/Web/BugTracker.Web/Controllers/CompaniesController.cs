namespace BugTracker.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Services.Company;
    using BugTracker.Web.ViewModels;
    using BugTracker.Web.ViewModels.Companies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class CompaniesController : Controller
    {
        private readonly ICompaniesService service;

        public CompaniesController(ICompaniesService service)
        {
            this.service = service;
        }

        // GET: Company
        public async Task<IActionResult> Index()
        {
            return this.View(await this.service.GetAll());
        }

        // GET: Company/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var company = await this.service.GetCompany(id);
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
        public async Task<IActionResult> Create(string companyName, AddCompanyViewModel companyViewModel)
        {
            var company = await this.service.Create(companyViewModel.Name);
            if (company == null)
            {
                return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var company = await this.service.GetCompany(id);
            if (company == null)
            {
                return this.NotFound();
            }

            return this.View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Company company)
        {
            if (id != company.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.service.EditCompany(company);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.service.CompanyExists(company.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(company);
        }

        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var company = await this.service.GetCompany(id);
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

        public async Task<IActionResult> Join(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var company = await this.service.GetCompany(id);
            if (company == null)
            {
                return this.NotFound();
            }

            return this.View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Join(string id, Company company)
        {
            if (id != company.Id)
            {
                return this.NotFound();
            }

            var username = this.User.Identity.Name;
            var result = await this.service.Join(username, id);
            if (!result)
            {
                return this.View(company);
            }

            return this.Redirect("/Project/All");
        }
    }
}
