namespace BugTracker.Services.Company
{
    using BugTracker.Data.Models;
    using BugTracker.Web.ViewModels.CompanyViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanyService
    {
        Task<AddCompanyViewModel> Create(string name);

        Task Join(string email);

        Task<Company> GetCompany(string id);

        Task EditCompany(Company company);

        Task DeleteCompany(string id);

        Task<List<Company>> GetAll();

        bool CompanyExists(string id);
    }
}
