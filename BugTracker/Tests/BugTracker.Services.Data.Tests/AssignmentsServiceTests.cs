namespace BugTracker.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Assignments;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class AssignmentsServiceTests
    {
        // [Fact]
        public void GetAllUsersInCompanyShouldGetAllUsers()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AllUsersInCompany")
                .EnableSensitiveDataLogging()
                .Options;

            var context = new ApplicationDbContext(options);
            var service = new AssignmentsService(context, null);

            context.Companies.AddRange(this.GetTestCompany());
            context.Users.AddRange(this.GetTestEmployees());
            context.Projects.AddRange(this.GetTestProjects());
            context.ProjectsUsers.AddRange(this.GetTestProjectUserts());
            context.CompaniesUsers.AddRange(this.GetTestCompanyUser());

            var users = service.GetAllUsersInCompany<IEnumerable<User>>("1");

            Assert.Equal(2, users.Count());
        }

        private IEnumerable<User> GetTestEmployees()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = "User1",
                    UserName = "John@example.com",
                },
                new User()
                {
                    Id = "User2",
                    UserName = "Doe@example.com",
                },
            };
        }

        private IEnumerable<Bug> GetTestBugs()
        {
            return new List<Bug>()
            {
                new Bug()
                {
                    Id = "Bug1",
                    Name = "Bug1",
                },
                new Bug()
                {
                    Id = "Bug2",
                    Name = "Bug2",
                },
            };
        }

        private IEnumerable<Project> GetTestProjects()
        {
            return new List<Project>()
            {
                new Project()
                {
                    Id = "Project1",
                    Name = "Project1",
                    Bugs = (ICollection<Bug>)this.GetTestBugs(),
                },
                new Project()
                {
                    Id = "Project2",
                    Name = "Project2",
                },
            };
        }

        private IEnumerable<ProjectUser> GetTestProjectUserts()
        {
            return new List<ProjectUser>()
            {
                new ProjectUser()
                {
                    ProjectId = "Project1",
                    UserId = "User1",
                },
                new ProjectUser()
                {
                    ProjectId = "Project2",
                    UserId = "User1",
                },
            };
        }

        private IEnumerable<Company> GetTestCompany()
        {
            return new List<Company>()
            {
                new Company()
                {
                    Id = "Company1",
                    Name = "Company1",
                },
                new Company()
                {
                    Id = "Company2",
                    Name = "Company2",
                },
            };
        }

        private IEnumerable<CompanyUser> GetTestCompanyUser()
        {
            return new List<CompanyUser>()
            {
                new CompanyUser()
                {
                    CompanyId = "Company1",
                    UserId = "User1",
                },
                new CompanyUser()
                {
                    CompanyId = "Company1",
                    UserId = "User2",
                },
            };
        }
    }
}
