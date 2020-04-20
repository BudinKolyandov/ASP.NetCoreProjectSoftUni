namespace BugTracker.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Bugs;
    using BugTracker.Services.Mapping;
    using BugTracker.Web.ViewModels;
    using BugTracker.Web.ViewModels.Bugs;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class BugsServiceTests
    {
        [Fact]
        public async Task AddBugChangeShouldAddValueChangeInBugHistories()
        {
            var service = this.ServiceSetup();
            await service.AddBugChange(new AddBuggChangeInputModel
            {
                Id = "Bug1",
                Name = "Change1",
                ChangedValueName = "Value",
                OldValue = "OldValue",
                NewValue = "NewValue",
            });
            var bug = service.GetById<DetailsBugsViewModel>("Bug1");
            Assert.NotEmpty(bug.BugHistories);
        }

        [Fact]
        public async Task EditBugShouldEditTheBug()
        {
            var service = this.ServiceSetup();
            await service.EditBug(new EditBugViewModel
            {
                Id = "Bug1",
                Name = "ChangedName",
                Description = "ChangedDescription",
            });
            var bug = service.GetById<DetailsBugsViewModel>("Bug1");
            Assert.Equal("ChangedName", bug.Name);
            Assert.Equal("ChangedDescription", bug.Description);
        }

        private BugsService ServiceSetup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var context = new ApplicationDbContext(options);
            context.Roles.AddRange(this.GetSampleRoles());
            context.Users.AddRange(this.GetSampleUsers());
            context.Companies.AddRange(this.GetSampleCompanies());
            context.Projects.AddRange(this.GetSampleProjects());
            context.JoinsRequests.AddRange(this.GetSampleJoinRequests());
            context.CompaniesUsers.AddRange(this.GetSampleCompaniesUsers());
            context.Bugs.AddRange(this.GetSampleBugs());
            context.SaveChanges();
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var mockService = new BugsService(context);
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
