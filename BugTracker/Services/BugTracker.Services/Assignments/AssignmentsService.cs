namespace BugTracker.Services.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;
    using BugTracker.Web.ViewModels.Assignments;

    public class AssignmentsService : IAssignmentsService
    {
        private readonly ApplicationDbContext context;

        public AssignmentsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateAssignnment(string userId, CreateAssignmentInputModel model)
        {
            var assignment = new Assignment
            {
                AssignedById = userId,
                BugId = model.BugId,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                DueDate = model.DueDate,
                Priority = model.Priority,
                Title = model.Title,
            };
            var user = this.context.Users.Where(x => x.Id == model.AssigneeId).First();
            if (user == null)
            {
                return 0;
            }

            var assignmentUser = new AssignmentUser
            {
                AssignmentId = assignment.Id,
                UserId = user.Id,
            };

            user.Assignments.Add(assignmentUser);
            assignment.Assignees.Add(assignmentUser);
            await this.context.Assignments.AddAsync(assignment);
            await this.context.SaveChangesAsync();
            return assignment.Id;
        }

        public IEnumerable<T> GetAllUsersInCompany<T>(string bugId, int? count = null)
        {
            var bug = this.context.Bugs.Where(x => x.Id == bugId).FirstOrDefault();
            var project = this.context.Projects
                .Where(x => x.Bugs.Any(s => s.Id == bug.Id)).FirstOrDefault();
            var company = this.context.Companies.Where(x => x.Projects.Any(p => p.Id == project.Id)).FirstOrDefault();
            var companyUsers = this.context.CompaniesUsers.Where(x => x.CompanyId == company.Id);
            List<string> ids = new List<string>();
            foreach (var user in companyUsers)
            {
                ids.Add(user.UserId);
            }

            IQueryable<User> query = this.context.Users.Where(x => ids.Contains(x.Id))
                .OrderBy(x => x.UserName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
