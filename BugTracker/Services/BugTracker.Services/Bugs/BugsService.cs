namespace BugTracker.Services.Bugs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;
    using BugTracker.Web.ViewModels.Bugs;

    public class BugsService : IBugsService
    {
        private readonly ApplicationDbContext context;

        public BugsService(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        public async Task<string> EditBug(EditBugViewModel model)
        {
            var bug = this.context.Bugs.Where(x => x.Id == model.Id).FirstOrDefault();
            if (bug == null)
            {
                return null;
            }

            var bugHistory = new BugHistory
            {
                BugId = bug.Id,
                OldDescriptionValue = bug.Description,
                ModifiedOn = DateTime.UtcNow,
                NewDescriptionValue = model.Description,
            };
            await this.context.BugsHistories.AddAsync(bugHistory);
            bug.ModifiedOn = DateTime.UtcNow;
            bug.Description = model.Description;
            bug.Name = model.Name;
            bug.DueDate = model.DueDate;
            bug.Priority = model.Priority;
            bug.Severity = model.Severity;
            bug.Status = model.Status;
            this.context.Bugs.Update(bug);
            await this.context.SaveChangesAsync();
            return bug.Id;
        }

        public T GetById<T>(string id)
        {
            var bug = this.context.Bugs
                .Where(x => x.Id == id).To<T>().FirstOrDefault();
            return bug;
        }
    }
}
