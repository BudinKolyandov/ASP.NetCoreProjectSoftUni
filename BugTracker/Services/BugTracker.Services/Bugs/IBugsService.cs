namespace BugTracker.Services.Bugs
{
    using System.Threading.Tasks;

    using BugTracker.Web.ViewModels.Bugs;

    public interface IBugsService
    {
        T GetById<T>(string id);

        Task<string> EditBug(EditBugViewModel model);
    }
}
