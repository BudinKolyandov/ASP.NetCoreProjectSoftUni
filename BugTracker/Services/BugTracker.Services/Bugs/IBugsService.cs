namespace BugTracker.Services.Bugs
{
    using BugTracker.Web.ViewModels.Bugs;
    using System.Threading.Tasks;

    public interface IBugsService
    {
        Task<DetailsBugsViewModel> GetBugDetails(string id);
    }
}
