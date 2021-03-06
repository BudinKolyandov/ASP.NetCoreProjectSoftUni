﻿namespace BugTracker.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using System.Reflection;
    using System.Threading.Tasks;

    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;
    using BugTracker.Services.Projects;
    using BugTracker.Web.ViewModels;
    using BugTracker.Web.ViewModels.Projects;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ProjectsServiceTests
    {
        [Fact]
        public async Task CrateShouldAddToDatabase()
        {
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new UserManager<User>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            mockUserRoleStore.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>(), System.Threading.CancellationToken.None)).Returns(Task.FromResult(0)).Verifiable();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var context = new ApplicationDbContext(options);
            context.Roles.Add(new ApplicationRole
            {
                Name = "ProjectAdministrator",
            });
            context.Users.AddRange(this.GetSampleUsers());
            context.Companies.AddRange(this.GetSampleCompanies());
            context.Projects.AddRange(this.GetSampleProjects());
            context.SaveChanges();
            var mockService = new ProjectsService(context, userManager);
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            await mockService.Create("ProjectCreate", "ProjectCreate", "User1", "Company1");
            Assert.Equal(4, context.Projects.Count());
        }

        [Fact]
        public void GetAllProjectsByUserEmailShouldRetunrOnlyUserProjects()
        {
            var service = this.ServiceSetup();
            var collection = service.GetAllProjectsByUserEmail<IndexProjectViewModel>("User1");
            Assert.Equal(3, collection.Count());
        }

        [Fact]
        public void GetByIdShouldReturnOneProject()
        {
            var service = this.ServiceSetup();
            var project = service.GetById<DetailsProjectViewModel>("Project1");
            Assert.Equal("Project1", project.Id);
        }

        [Fact]
        public async Task ReportShouldAddBugToProject()
        {
            var service = this.ServiceSetup();

            await service.Report("User1", new ReportBugProjectInputModel
            {
                Name = "NewBug",
                Description = "NewBug",
                ProjectId = "Project1",
                ProjectName = "Project1",
                DueDate = DateTime.UtcNow.Date,
                Priority = BugTracker.Data.Models.Enums.Priority.High,
                Severity = BugTracker.Data.Models.Enums.Severity.Critical,
                Status = BugTracker.Data.Models.Enums.Status.New,
            });
            var project = service.GetById<DetailsProjectViewModel>("Project1");
            Assert.True(project.Bugs.Count() > 0);
        }

        [Fact]
        public void UserHasCompanyReturnsFalseWhenUserIsNotInUserCompanies()
        {
            var service = this.ServiceSetup();
            var result = service.UserHasCompany("User2");
            Assert.False(result);
        }

        [Fact]
        public void UserHasCompanyReturnsTrueWhenUserIsInUserCompanies()
        {
            var service = this.ServiceSetup();
            var result = service.UserHasCompany("User1");
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteProjectShouldDeleteFromDb()
        {
            var service = this.ServiceSetup();
            await service.DeleteProject("Project1");
            var result = service.GetById<DetailsProjectViewModel>("Project1");
            Assert.Null(result);
        }

        [Fact]
        public void GetByIdWithBugsShouldGetOnlyOpenedBugs()
        {
            var service = this.ServiceSetup();
            var project = service.GetById<DetailsProjectViewModel>("Project1");
            var result = service.GetByIdWithBugs<DetailsProjectBugViewModel>("Project1");
            project.Bugs = result;

            Assert.Equal(2, project.Bugs.Count());
        }

        [Fact]
        public void GetByIdWithBugsShouldGetOnlyClosedBugs()
        {
            var service = this.ServiceSetup();
            var project = service.GetById<DetailsProjectViewModel>("Project1");
            var result = service.GetByIdWithClosedBugs<DetailsProjectBugViewModel>("Project1");
            project.Bugs = result;

            Assert.Equal(2, project.Bugs.Count());
        }

        private ProjectsService ServiceSetup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var context = new ApplicationDbContext(options);
            var mockService = new ProjectsService(context, null);
            context.Roles.AddRange(this.GetSampleRoles());
            context.Users.AddRange(this.GetSampleUsers());
            context.Companies.AddRange(this.GetSampleCompanies());
            context.Projects.AddRange(this.GetSampleProjects());
            context.JoinsRequests.AddRange(this.GetSampleJoinRequests());
            context.CompaniesUsers.AddRange(this.GetSampleCompaniesUsers());
            context.Bugs.AddRange(this.GetSampleBugs());
            context.SaveChanges();
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            return mockService;
        }

        private List<JoinRequest> GetSampleJoinRequests()
        {
            var output = new List<JoinRequest>
            {
                new JoinRequest
                {
                    AdminId = "1",
                    CompanyId = "Company1",
                    UserId = "2",
                },
                new JoinRequest
                {
                    AdminId = "1",
                    CompanyId = "Company1",
                    UserId = "3",
                },
            };

            return output;
        }

        private List<ApplicationRole> GetSampleRoles()
        {
            var output = new List<ApplicationRole>
            {
                new ApplicationRole
                {
                    Id = "Role1",
                    Name = "AwaitingAproval",
                },
            };
            return output;
        }

        private List<User> GetSampleUsers()
        {
            var output = new List<User>
            {
                new User
            {
                Id = "1",
                UserName = "User1",
                Email = "User1",
            },
                new User
            {
                Id = "2",
                UserName = "User2",
                Email = "User3",
            },
                new User
            {
                Id = "3",
                UserName = "User3",
                Email = "User3",
            },
            };
            return output;
        }

        private List<BugTracker.Data.Models.Company> GetSampleCompanies()
        {
            var output = new List<BugTracker.Data.Models.Company>
            {
                new BugTracker.Data.Models.Company
                {
                    Id = "Company1",
                    Name = "Company1",
                    Description = "Company1",
                    AdminId = "1",
                },
                new BugTracker.Data.Models.Company
                {
                    Id = "Company2",
                    Name = "Company2",
                    Description = "Company2",
                    AdminId = "2",
                },
                new BugTracker.Data.Models.Company
                {
                    Id = "Company3",
                    Name = "Company3",
                    Description = "Company3",
                    AdminId = "3",
                },
            };

            return output;
        }

        private List<Project> GetSampleProjects()
        {
            var output = new List<Project>
            {
                new Project
                {
                    Id = "Project1",
                    Name = "Project1",
                    Description = "Project1",
                    AdminId = "1",
                    CompanyId = "Company1",
                },
                new Project
                {
                    Id = "Project2",
                    Name = "Project2",
                    Description = "Project2",
                    AdminId = "2",
                    CompanyId = "Company1",
                },
                new Project
                {
                    Id = "Project3",
                    Name = "Project3",
                    Description = "Project3",
                    AdminId = "3",
                    CompanyId = "Company1",
                },
            };

            return output;
        }

        private List<CompanyUser> GetSampleCompaniesUsers()
        {
            var output = new List<CompanyUser>
            {
                new CompanyUser
                {
                    CompanyId = "Company1",
                    UserId = "1",
                },
                new CompanyUser
                {
                    CompanyId = "Company2",
                    UserId = "1",
                },
                new CompanyUser
                {
                    CompanyId = "Company3",
                    UserId = "1",
                },
            };
            return output;
        }

        private List<Bug> GetSampleBugs()
        {
            var output = new List<Bug>
            {
                new Bug
                {
                    Id = "Bug1",
                    ProjectId = "Project1",
                    ReporterId = "User1",
                    Status = BugTracker.Data.Models.Enums.Status.New,
                },
                new Bug
                {
                    Id = "Bug2",
                    ProjectId = "Project1",
                    ReporterId = "User1",
                    Status = BugTracker.Data.Models.Enums.Status.New,
                },
                new Bug
                {
                    Id = "Bug3",
                    ProjectId = "Project1",
                    ReporterId = "User1",
                    Status = BugTracker.Data.Models.Enums.Status.Closed,
                },
                new Bug
                {
                    Id = "Bug4",
                    ProjectId = "Project1",
                    ReporterId = "User1",
                    Status = BugTracker.Data.Models.Enums.Status.Closed,
                },
            };
            return output;
        }
    }
}
