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
                Company = user.Company
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

        public async Task<List<AllProjectsViewModel>> GetAll()
        {
            return await this.context.Projects
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
    }
}
