namespace BugTracker.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Mapping;
    using BugTracker.Services.News;
    using BugTracker.Web.ViewModels;
    using BugTracker.Web.ViewModels.Assignments;
    using BugTracker.Web.ViewModels.News;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class NewsServiceTests
    {
        [Fact]
        public void CompanyCheckShouldBeNulleWhenUserIsNotInACompany()
        {
            var service = this.ServiceSetup();
            var id = service.CompanyCheck("3");
            Assert.Null(id);
        }

        [Fact]
        public void CompanyCheckShouldHaveValueWhenUserIsInACompany()
        {
            var service = this.ServiceSetup();
            var id = service.CompanyCheck("1");
            Assert.NotNull(id);
        }

        [Fact]
        public void CreateNewsShouldAddNewsToDbAndUsers()
        {
            var service = this.ServiceSetup();
            var id = service.CreateNews("1", new CreateNewsInputModel
            {
                ProjectId = "Project1",
                Headline = "CreatedNews",
                Body = "CreatedNews",
            });
            var allForUser = service.GetAllForUser<IndexLoggedNewsViewModel>("1");
            Assert.NotNull(id);
            Assert.Equal(4, allForUser.Count());
        }

        [Fact]
        public async Task SeenChangeShouldChangeSeenToTrue()
        {
            var service = this.ServiceSetup();
            var id = await service.SeenChange(1, "1");
            var allForUser = service.GetAllForUser<IndexLoggedNewsViewModel>("1");

            Assert.Equal(2, allForUser.Count());
        }

        private NewsService ServiceSetup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var context = new ApplicationDbContext(options);
            var mockService = new NewsService(context);
            context.Roles.AddRange(this.GetSampleRoles());
            context.Users.AddRange(this.GetSampleUsers());
            context.Companies.AddRange(this.GetSampleCompanies());
            context.Projects.AddRange(this.GetSampleProjects());
            context.JoinsRequests.AddRange(this.GetSampleJoinRequests());
            context.CompaniesUsers.AddRange(this.GetSampleCompaniesUsers());
            context.Bugs.AddRange(this.GetSampleBugs());
            context.Assignments.AddRange(this.GetSampleAssignments());
            context.AssignmentsUsers.AddRange(this.GetSampleAssignmentUsers());
            context.News.AddRange(this.GetSampleNews());
            context.UsersNews.AddRange(this.GetSampleUserNews());
            context.SaveChanges();
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            return mockService;
        }

        private List<UserNews> GetSampleUserNews()
        {
            var output = new List<UserNews>
            {
                new UserNews
                {
                    UserId = "1",
                    NewsId = 1,
                    Seen = false,
                },
                new UserNews
                {
                    UserId = "1",
                    NewsId = 2,
                    Seen = false,
                },
                new UserNews
                {
                    UserId = "1",
                    NewsId = 3,
                    Seen = false,
                },
            };
            return output;
        }

        private List<News> GetSampleNews()
        {
            var output = new List<News>
            {
                new News
                {
                    Id = 1,
                    Headline = "News1",
                    Body = "News1",
                    PosterId = "1",
                    DatePosted = DateTime.UtcNow,
                },
                new News
                {
                    Id = 2,
                    Headline = "News2",
                    Body = "News2",
                    PosterId = "1",
                    DatePosted = DateTime.UtcNow,
                },
                new News
                {
                    Id = 3,
                    Headline = "News3",
                    Body = "News3",
                    PosterId = "1",
                    DatePosted = DateTime.UtcNow,
                },
            };
            return output;
        }

        private List<AssignmentUser> GetSampleAssignmentUsers()
        {
            var output = new List<AssignmentUser>
            {
                new AssignmentUser
                {
                    AssignmentId = 1,
                    UserId = "1",
                },
                new AssignmentUser
                {
                    AssignmentId = 2,
                    UserId = "1",
                },
                new AssignmentUser
                {
                    AssignmentId = 3,
                    UserId = "1",
                },
            };
            return output;
        }

        private List<Assignment> GetSampleAssignments()
        {
            var output = new List<Assignment>
            {
                new Assignment
                {
                    Id = 1,
                    AssignedById = "1",
                    BugId = "Bug1",
                    Title = "Assignment1",
                },
                new Assignment
                {
                    Id = 2,
                    AssignedById = "1",
                    BugId = "Bug1",
                    Title = "Assignment2",
                },
                new Assignment
                {
                    Id = 3,
                    AssignedById = "1",
                    BugId = "Bug1",
                    Title = "Assignment3",
                },
            };
            return output;
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
                new CompanyUser
                {
                    CompanyId = "Company1",
                    UserId = "2",
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
