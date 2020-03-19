namespace BugTracker.Services.Bugs
{
    using System;
    using System.Threading.Tasks;

    using BugTracker.Data;
    using BugTracker.Data.Models.Enums;
    using BugTracker.Web.ViewModels.Bugs;
    using Microsoft.EntityFrameworkCore;

    public class BugsService : IBugsService
    {
        private readonly ApplicationDbContext context;

        public BugsService(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
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
