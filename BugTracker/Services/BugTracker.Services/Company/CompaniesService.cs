namespace BugTracker.Services.Company
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;
    using BugTracker.Services.Messaging;
    using BugTracker.Web.ViewModels.Companies;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class CompaniesService : ICompaniesService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;

        public CompaniesService(
            ApplicationDbContext context,
            UserManager<User> userManager,
            IEmailSender emailSender)
        {
            this.context = context;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public bool CompanyExists(string id)
            => this.context.Companies.Any(e => e.Id == id);

        public async Task<string> Create(AddCompanyInputModel model, string userId)
        {
            if (this.context.Companies.Any(x => x.Name == model.Name))
            {
                return null;
            }

            var user = this.context.Users.Where(x => x.Id == userId).FirstOrDefault();
            var company = new Data.Models.Company
            {
                Name = model.Name,
                Description = model.Description,
                AdminId = user.Id,
            };

            var userCompany = new CompanyUser
            {
                CompanyId = company.Id,
                UserId = user.Id,
            };

            await this.userManager.AddToRoleAsync(user, "CompanyAdministrator");
            user.Companies.Add(userCompany);
            company.Employees.Add(userCompany);
            await this.context.Companies.AddAsync(company);
            await this.context.SaveChangesAsync();
            return company.Id;
        }

        public async Task DeleteCompany(string id)
        {
            var company = await this.context.Companies.FindAsync(id);
            this.context.RemoveRange(this.context.CompaniesUsers.Where(x => x.CompanyId == id));
            this.context.RemoveRange(this.context.Projects.Where(x => x.CompanyId == id));
            this.context.Companies.Remove(company);
            await this.context.SaveChangesAsync();
        }

        public async Task EditCompany(EditCompanyInputModel model)
        {
            var company = await this.context.Companies.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
            company.Name = model.Name;
            company.Description = model.Description;
            this.context.Companies.Update(company);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Company> query = this.context.Companies.OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllForUser<T>(string userId, int? count = null)
        {
            IQueryable<Company> query = this.context.Companies
                .Where(x => x.AdminId == userId)
                .OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var company = this.context.Companies
                .Where(x => x.Id == id).To<T>().FirstOrDefault();
            return company;
        }

        public async Task<string> Join(string username, string id)
        {
            var user = this.context.Users.FirstOrDefault(x => x.Email == username);
            var company = this.context.Companies.FirstOrDefault(x => x.Id == id);

            if (user == null || company == null)
            {
                return null;
            }

            var existingRelationCheck = this.context.CompaniesUsers.Where(x => x.UserId == user.Id && x.CompanyId == company.Id).FirstOrDefault();
            if (existingRelationCheck != null)
            {
                return company.Id;
            }

            var admin = this.context.Users.Where(x => x.Id == company.AdminId).FirstOrDefault();
            await this.emailSender.SendEmailAsync(user.Email, user.FullName, admin.Email, "Join Company", $"Hello,{Environment.NewLine}{user.FullName} wants to join your company. You can Acept or decline from  the administration page in your profile!");

            var companyUser = new CompanyUser
            {
                CompanyId = company.Id,
                UserId = user.Id,
            };

            user.Companies.Add(companyUser);
            company.Employees.Add(companyUser);
            await this.context.SaveChangesAsync();
            return company.Id;
        }
    }
}
