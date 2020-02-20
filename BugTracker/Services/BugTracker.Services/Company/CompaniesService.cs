namespace BugTracker.Services.Company
{
    using BugTracker.Data;
    using System.Threading.Tasks;
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using BugTracker.Web.ViewModels.CompanyViewModels;

    public class CompaniesService : ICompaniesService
    {
        private readonly ApplicationDbContext context;

        public CompaniesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool CompanyExists(string id)
            => this.context.Companies.Any(e => e.Id == id);

        public async Task<AddCompanyViewModel> Create(string name)
        {
            if (this.context.Companies.Any(x=>x.Name == name))
            {
                return null;
            }
            var company = new Data.Models.Company{
                Id = Guid.NewGuid().ToString(),
                Name = name,
            };
            this.context.Companies.Add(company);
            await this.context.SaveChangesAsync();
            var model = new AddCompanyViewModel{
                Name = company.Name,
                Id = company.Id
            };
            return model;
        }

        public async Task DeleteCompany(string id)
        {
            var company = await this.context.Companies.FindAsync(id);
            this.context.Companies.Remove(company);
            await this.context.SaveChangesAsync();
        }

        public async Task EditCompany(Data.Models.Company company)
        {
            this.context.Companies.Update(company);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<Data.Models.Company>> GetAll()
        {
            return await this.context.Companies.ToListAsync();
        }

        public async Task<Data.Models.Company> GetCompany(string id)
        {
            return await this.context.Companies.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<bool> Join(string username, string id)
        {
            var user = this.context.Users.FirstOrDefault(x => x.Email == username);
            var company = this.context.Companies.FirstOrDefault(x => x.Id == id);

            if (user == null || company == null)
            {
                return false;
            }
            user.Company = company;
            company.Employees.Add(user);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
