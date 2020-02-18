namespace BugTracker.Services.Company
{
    using BugTracker.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanyService
    {
        Task<Company> Create(string name);

        Task Join(string email);

        Task<Company> GetCompany(string id);

        Task EditCompany(Company company);

        Task DeleteCompany(string id);

        Task<List<Company>> GetAll();

        bool CompanyExists(string id);
    }
}
