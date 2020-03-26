namespace BugTracker.Services.Projects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;
    using BugTracker.Web.ViewModels.Projects;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class ProjectsService : IProjectsService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public ProjectsService(
            ApplicationDbContext context,
            UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<AddProjectViewModel> Create(string name, string status, string description, string username)
        {
            if (this.context.Projects.Any(x => x.Name == name))
            {
                return null;
            }

            var user = this.context.Users.FirstOrDefault(x => x.Email == username);
            if (user == null)
            {
                return null;
            }

            var project = new Project
            {
                Name = name,
                Status = status,
                Description = description,
                CompanyId = user.CompanyId,
            };

            await this.userManager.AddToRoleAsync(user, "CompanyAdministrator");
            this.context.Projects.Add(project);
            await this.context.SaveChangesAsync();
            var model = new AddProjectViewModel
            {
                Name = project.Name,
                Status = project.Status,
                Description = project.Description,
            };
            return model;
        }

        public IEnumerable<T> GetAll<T>(string userEmail, int? count = null)
        {
            var user = this.context.Users.Where(x => x.Email == userEmail).First();
            if (user == null)
            {
                return null;
            }

            if (user.CompanyId == null)
            {
                return null;
            }

            IQueryable<Project> query = this.context.Projects.OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var project = this.context.Projects
                .Where(x => x.Id == id).To<T>().FirstOrDefault();
            return project;
        }

        public string Join(string userEmail, JoinProjectViewModel model)
        {
            var user = this.context.Users.Where(x => x.Email == userEmail).First();
            if (user == null)
            {
                return null;
            }

            var projectUser = new ProjectUser
            {
                Project = this.context.Projects.First(x => x.Id == model.Id),
                ProjectId = model.Id,
                UserId = user.Id,
            };
            if (user.Projects.Contains(projectUser))
            {
                return null;
            }

            this.context.ProjectsUsers.Add(projectUser);
            this.context.Projects
                .First(x => x.Id == model.Id)
                .Developers.Add(projectUser);
            this.context.Users.First(x => x.Email == userEmail)
                .Projects.Add(projectUser);
            this.context.SaveChanges();
            return model.Id;
        }

        public async Task<ReportBugProjectInputModel> Report(string userEmail, ReportBugProjectInputModel model)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
            if (user == null)
            {
                return null;
            }

            var bug = new Bug
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                ProjectId = model.ProjectId,
                Priority = model.Priority,
                Severity = model.Severity,
                Status = model.Status,
                ReporterId = user.Id,
                DueDate = model.DueDate,
                Project = this.context.Projects.FirstOrDefault(x => x.Id == model.ProjectId),
                Reporter = this.context.Users.FirstOrDefault(x => x.Id == user.Id),
            };
            var project = await this.context.Projects.FirstOrDefaultAsync(x => x.Id == model.ProjectId);
            project.Bugs.Add(bug);
            this.context.Projects.Update(project);
            this.context.Bugs.Add(bug);
            await this.context.SaveChangesAsync();
            return model;
        }
    }
}
