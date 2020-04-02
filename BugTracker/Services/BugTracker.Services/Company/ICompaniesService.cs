namespace BugTracker.Services.Company
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTracker.Data.Models;
    using BugTracker.Web.ViewModels.Companies;

    public interface ICompaniesService
    {
        Task<string> Create(AddCompanyInputModel model, string userId);

        Task<string> Join(string username, string id);

        T GetById<T>(string id);

        Task EditCompany(EditCompanyInputModel model);

        Task DeleteCompany(string id);

        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllAdmin<T>(string userId, int? count = null);

        bool CompanyExists(string id);
    }
}
