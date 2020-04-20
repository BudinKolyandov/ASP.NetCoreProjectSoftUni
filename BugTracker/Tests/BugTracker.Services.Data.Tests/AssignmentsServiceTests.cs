namespace BugTracker.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Assignments;
    using BugTracker.Services.Mapping;
    using BugTracker.Services.Messaging;
    using BugTracker.Web.ViewModels;
    using BugTracker.Web.ViewModels.Assignments;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class AssignmentsServiceTests
    {
        [Fact]
        public void GetAllUsersInCompanyShouldGetAllUsers()
        {
            var service = this.ServiceSetup();

            var users = service.GetAllUsersInCompany<CreateAssignnmentUserViewModel>("Bug1");

            Assert.Equal(2, users.Count());
        }

        [Fact]
        public void GetAllForUserShouldGetAllAssignmentsForUser()
        {
            var service = this.ServiceSetup();

            var assignments = service.GetAllForUser<IndexLoggedAssignmentViewModel>("1");

            Assert.Equal(3, assignments.Count());
        }

        [Fact]
        public void CompleteAssignmentShouldChangeAssignmentToCompleted()
        {
            var service = this.ServiceSetup();

            var assignmentId = service.CompleteAssignment(1, "1");
            var assignments = service.GetAllForUser<IndexLoggedAssignmentViewModel>("1");

            Assert.Equal(2, assignments.Count());
        }

        [Fact]
        public void CrateAssignmentShouldCreateANewAssignmentAdnAddItToUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            var senderMock = new Mock<IEmailSender>();
            senderMock.Setup(x => x.SendEmailAsync("1", "2", "3", "4", "5", null)).Returns(Task.CompletedTask);
            var service = new AssignmentsService(context, senderMock.Object);
            context.Roles.AddRange(this.GetSampleRoles());
            context.Users.AddRange(this.GetSampleUsers());
            context.Companies.AddRange(this.GetSampleCompanies());
            context.Projects.AddRange(this.GetSampleProjects());
            context.JoinsRequests.AddRange(this.GetSampleJoinRequests());
            context.CompaniesUsers.AddRange(this.GetSampleCompaniesUsers());
            context.Bugs.AddRange(this.GetSampleBugs());
            context.Assignments.AddRange(this.GetSampleAssignments());
            context.AssignmentsUsers.AddRange(this.GetSampleAssignmentUsers());
            context.SaveChanges();
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var assignmentId = service.CreateAssignnment("1", new CreateAssignmentInputModel
            {
                AssigneeId = "1",
                BugId = "Bug1",
                Title = "CreatedAssignment",
                Description = "CreatedAssignment",
                DueDate = DateTime.UtcNow.Date,
                Priority = BugTracker.Data.Models.Enums.Priority.High,
            });

            Assert.Equal(4, context.Assignments.Count());
            Assert.Equal(4, context.AssignmentsUsers.Count());
        }

        private AssignmentsService ServiceSetup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var context = new ApplicationDbContext(options);
            var mockService = new AssignmentsService(context, null);
            context.Roles.AddRange(this.GetSampleRoles());
            context.Users.AddRange(this.GetSampleUsers());
            context.Companies.AddRange(this.GetSampleCompanies());
            context.Projects.AddRange(this.GetSampleProjects());
            context.JoinsRequests.AddRange(this.GetSampleJoinRequests());
            context.CompaniesUsers.AddRange(this.GetSampleCompaniesUsers());
            context.Bugs.AddRange(this.GetSampleBugs());
            context.Assignments.AddRange(this.GetSampleAssignments());
            context.AssignmentsUsers.AddRange(this.GetSampleAssignmentUsers());
            context.SaveChanges();
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            return mockService;
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
