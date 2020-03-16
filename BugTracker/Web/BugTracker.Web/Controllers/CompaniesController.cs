using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data.Models;
using BugTracker.Services.Company;
using BugTracker.Web.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using BugTracker.Web.ViewModels.Companies;
using System;

namespace BugTracker.Web.Controllers
{
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
            return View(await this.service.GetAll());
        }

        // GET: Company/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await this.service.GetCompany(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Company/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(string companyName, AddCompanyViewModel companyViewModel)
        {
            var company = await this.service.Create(companyViewModel.Name);
            if (company == null)
            {
                return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await this.service.GetCompany(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(string id, Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this.service.EditCompany(company);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.service.CompanyExists(company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await this.service.GetCompany(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await this.service.DeleteCompany(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Join(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await this.service.GetCompany(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Join(string id, Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }
            var username = this.User.Identity.Name;
            var result = await this.service.Join(username, id);
            if (!result)
            {
                return this.View(company);
            }
            return Redirect("/Project/All");
        }
    }
}
