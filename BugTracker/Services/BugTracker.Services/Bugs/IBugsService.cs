namespace BugTracker.Services.Bugs
{
    using System.Threading.Tasks;

    using BugTracker.Web.ViewModels.Bugs;

    public interface IBugsService
    {
        Task<DetailsBugsViewModel> GetBugDetails(string id);
    }
}
