﻿namespace BugTracker.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Services.Company;
    using BugTracker.Web.ViewModels.Companies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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
                Companies = this.service.GetAllAdmin<IndexCompanyViewModel>(user),
            };
            return this.View(viewModel);
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