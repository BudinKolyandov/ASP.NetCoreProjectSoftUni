namespace BugTracker.Services.Company
{
    using BugTracker.Data.Models;
    using BugTracker.Web.ViewModels.CompanyViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
