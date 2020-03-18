namespace BugTracker.Services.Company
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Web.ViewModels.Companies;

    public interface ICompaniesService
    {
        Task<AddCompanyViewModel> Create(string name);

        Task<bool> Join(string username, string id);

        Task<Company> GetCompany(string id);

        Task EditCompany(Company company);

        Task DeleteCompany(string id);

        Task<List<Company>> GetAll();

        bool CompanyExists(string id);
    }
}
