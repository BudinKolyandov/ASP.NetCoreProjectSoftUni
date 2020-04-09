namespace BugTracker.Services.News
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTracker.Web.ViewModels.News;

    public interface INewsService
    {
        Task<int> CreateNews(string userId, CreateNewsInputModel model);

        IEnumerable<T> GetAllForUser<T>(string userId, int? count = null);
    }
}
