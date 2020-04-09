namespace BugTracker.Services.Assignments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;
    using BugTracker.Services.Messaging;
    using BugTracker.Web.ViewModels.Assignments;
    using Microsoft.EntityFrameworkCore.Internal;

    public class AssignmentsService : IAssignmentsService
    {
        private readonly ApplicationDbContext context;
        private readonly IEmailSender emailSender;

        public AssignmentsService(
            ApplicationDbContext context,
            IEmailSender emailSender)
        {
            this.context = context;
            this.emailSender = emailSender;
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
            var bug = this.context.Bugs.Where(x => x.Id == model.BugId).First();
            var project = this.context.Projects.Where(x => x.Bugs.Any(x => x.Id == model.BugId)).First();
            var assignedBy = this.context.Users.Where(x => x.Id == userId).First();
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
            await this.emailSender.SendEmailAsync(assignedBy.Email, assignedBy.FullName, user.Email, "New assignment", $"You have a new Assignment regarding {project.Name}!");
            return assignment.Id;
        }

        public IEnumerable<T> GetAllForUser<T>(string userId, int? count = null)
        {
            IQueryable<Assignment> query = this.context.Assignments
                .Where(x => x.Assignees.Any(x => x.UserId == userId))
                .OrderBy(x => x.CreatedOn);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
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
