using BugTracker.Data;
using BugTracker.Data.Models;
using BugTracker.Web.ViewModels.Projects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Services.Projects
{
    public class ProjectsService : IProjectsService
    {
        private readonly ApplicationDbContext context;

        public ProjectsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<AddProjectViewModel> Create(string name, string status, string description, string username)
        {
            if (this.context.Projects.Any(x => x.Name == name))
            {
                return null;
            }
            var user = this.context.Users.FirstOrDefault(x => x.Email == username);
            var project = new Project
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Status = status,
                Description = description,
                CompanyId = user.CompanyId,
            };

            this.context.Projects.Add(project);
            await this.context.SaveChangesAsync();
            var model = new AddProjectViewModel
            {
                Name = project.Name,
                Status = project.Status,
                Description = project.Description
            };
            return model;
        }

        public async Task<List<AllProjectsViewModel>> GetAll(string userEmail)
        {
            var user = this.context.Users.Where(x => x.Email == userEmail).First();
            if (user == null)
            {
                return null;
            }
            var company = this.context.Companies.Where(x=>x.Id == user.CompanyId).First();
            if (company == null)
            {
                return null;
            }

            return await this.context
                .Projects.Where(x=>x.CompanyId == user.CompanyId)
                .Select(x => new AllProjectsViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status,
                    Description = x.Description,
                    Bugs = x.Bugs
                })
                .ToListAsync();
        }

        public async Task<DetailsProjectViewModel> GetProject(string id)
        {
            var project = await this.context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            var model = new DetailsProjectViewModel
            {
                Id = project.Id,
                Name = project.Name,
                Status = project.Status,
                Description = project.Description,
                Bugs = project.Bugs,
                Developers = project.Developers
            };

            return model;
        }
    }
}
