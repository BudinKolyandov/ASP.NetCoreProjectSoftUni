namespace BugTracker.Services.Bugs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Data;
    using BugTracker.Data.Models.Enums;
    using BugTracker.Services.Mapping;
    using BugTracker.Web.ViewModels.Bugs;
    using Microsoft.EntityFrameworkCore;

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

        public async Task<DetailsBugsViewModel> GetBugDetails(string id)
        {
            var bug = await this.context.Bugs.FirstOrDefaultAsync(x => x.Id == id);
            if (bug == null)
            {
                return null;
            }

            var model = new DetailsBugsViewModel
            {
                Id = bug.Id,
                Name = bug.Name,
                Priority = Enum.GetName(typeof(Priority), bug.Priority),
                Severity = Enum.GetName(typeof(Severity), bug.Severity),
                Status = Enum.GetName(typeof(Status), bug.Status),
                DueDate = bug.DueDate,
                ProjectId = bug.ProjectId,
                ReporterId = bug.ReporterId,
            };
            return model;
        }
    }
}
