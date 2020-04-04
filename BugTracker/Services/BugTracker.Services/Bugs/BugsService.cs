namespace BugTracker.Services.Bugs
{
    using System.Linq;

    using BugTracker.Data;
    using BugTracker.Services.Mapping;

    public class BugsService : IBugsService
    {
        private readonly ApplicationDbContext context;

        public BugsService(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        public T GetById<T>(string id)
        {
            var bug = this.context.Bugs
                .Where(x => x.Id == id).To<T>().FirstOrDefault();
            return bug;
        }
    }
}
