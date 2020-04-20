namespace BugTracker.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Autofac.Extras.Moq;
    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Company;
    using BugTracker.Services.Mapping;
    using BugTracker.Services.Messaging;
    using BugTracker.Web.ViewModels;
    using BugTracker.Web.ViewModels.Companies;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class CompaniesServiceTests
    {
        [Fact]
        public void GetAllShouldGetEveryCompany()
        {
            CompaniesService mockService = this.ServiceSetup();
            var result = mockService.GetAll<DetailsCompanyViewModel>(null);
            var resultPaged = mockService.GetAllPaged<DetailsCompanyViewModel>(2, 0);

            Assert.Equal(2, resultPaged.Count());
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void GetAllForAdminShouldGetEveryCompany()
        {
            CompaniesService mockService = this.ServiceSetup();
            var result = mockService.GetAllForAdminUser<DetailsCompanyViewModel>("1");

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllForUsersForAprovalShouldGetEveryApprovalForId()
        {
            CompaniesService mockService = this.ServiceSetup();
            var result = mockService.GetAllForUsersForAproval<RequestsToJoinViewModel>("1");

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetByIdWithPaginationShouldGetAllIdForPagination()
        {
            CompaniesService mockService = this.ServiceSetup();
            var result = mockService.GetByIdWithPagination<ProjectCompanyViewModel>("Company1", 2);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task DeleteRemovesCompanyFromDb()
        {
            CompaniesService mockService = this.ServiceSetup();
            var company = mockService.GetById<DetailsCompanyViewModel>("Company1");
            await mockService.DeleteCompany("Company1");

            var result = mockService.GetAll<DetailsCompanyViewModel>(null);
            Assert.DoesNotContain(company, result);
        }

        [Fact]
        public void CompanyExistShoudBeFalseWhenIdDoesntExistAndTrueWhenExists()
        {
            CompaniesService mockService = this.ServiceSetup();
            var falseResult = mockService.CompanyExists("Company20");
            Assert.False(falseResult);

            var trueResult = mockService.CompanyExists("Company3");
            Assert.True(trueResult);
        }

        [Fact]
        public async Task EditCompanyShouldEditNameAndDescription()
        {
            CompaniesService mockService = this.ServiceSetup();
            await mockService.EditCompany(
                new EditCompanyInputModel
                {
                    Id = "Company1",
                    Name = "NewName",
                    Description = "NewDescription",
                });
            var company = mockService.GetById<DetailsCompanyViewModel>("Company1");

            Assert.Equal("NewName", company.Name);
            Assert.Equal("NewDescription", company.Description);
        }

        [Fact]
        public async Task CreateCompanyShouldCreateAndAddToDB()
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
                Name = "CompanyAdministrator",
            });
            context.Users.AddRange(this.GetSampleUsers());
            context.Companies.AddRange(this.GetSampleCompanies());
            context.SaveChanges();
            var mockService = new CompaniesService(context, userManager, null);
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            await mockService.Create(
                new AddCompanyInputModel
                {
                    Id = "Company4",
                    Name = "Company4",
                    Description = "Company4",
                }, "1");

            var company = mockService.GetAll<DetailsCompanyViewModel>();

            Assert.Equal(4, company.Count());
        }

        [Fact]
        public async Task JoinAddsToJoinRequests()
        {
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var userManager = new UserManager<User>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            var senderMock = new Mock<IEmailSender>();
            senderMock.Setup(x => x.SendEmailAsync("1", "2", "3", "4", "5", null)).Returns(Task.CompletedTask);
            mockUserRoleStore.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>(), System.Threading.CancellationToken.None)).Returns(Task.FromResult(0)).Verifiable();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var context = new ApplicationDbContext(options);
            context.Roles.Add(new ApplicationRole
            {
                Name = "AwaitingAproval",
            });
            context.Users.AddRange(this.GetSampleUsers());
            context.Companies.AddRange(this.GetSampleCompanies());
            context.SaveChanges();
            var mockService = new CompaniesService(context, userManager, senderMock.Object);
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = await mockService.Join("User1", "Company2");

            Assert.Equal("Company2", result);
        }

        [Fact]
        public async Task AproveAddsUserToCompanyAndRemovesJoinRequest()
        {
            var mockUserStore = new Mock<IUserStore<User>>();
            var mockUserRoleStore = mockUserStore.As<IUserRoleStore<User>>();
            var senderMock = new Mock<IEmailSender>();
            senderMock.Setup(x => x.SendEmailAsync("1", "2", "3", "4", "5", null)).Returns(Task.CompletedTask);
            mockUserRoleStore.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>(), System.Threading.CancellationToken.None)).Returns(Task.CompletedTask);
            mockUserRoleStore.Setup(x => x.RemoveFromRoleAsync(It.IsAny<User>(), It.IsAny<string>(), System.Threading.CancellationToken.None)).Returns(Task.CompletedTask);

            var mockedUM = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            mockedUM.Setup(x => x.RemoveFromRoleAsync(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success)).Verifiable();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var context = new ApplicationDbContext(options);
            context.Roles.Add(new ApplicationRole
            {
                Name = "AwaitingAproval",
            });
            context.Roles.AddRange(this.GetSampleRoles());
            context.Users.AddRange(this.GetSampleUsers());
            context.Companies.AddRange(this.GetSampleCompanies());
            context.JoinsRequests.AddRange(this.GetSampleJoinRequests());
            context.SaveChanges();
            var mockService = new CompaniesService(context, mockedUM.Object, null);
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = await mockService.Aprove("2", "Company1");

            Assert.Equal("Company1", result);
        }

        private CompaniesService ServiceSetup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var context = new ApplicationDbContext(options);
            var mockService = new CompaniesService(context, null, null);
            context.Roles.AddRange(this.GetSampleRoles());
            context.Users.AddRange(this.GetSampleUsers());
            context.Companies.AddRange(this.GetSampleCompanies());
            context.Projects.AddRange(this.GetSampleProjects());
            context.JoinsRequests.AddRange(this.GetSampleJoinRequests());
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

        private List<Company> GetSampleCompanies()
        {
            var output = new List<Company>
            {
                new Company
                {
                    Id = "Company1",
                    Name = "Company1",
                    Description = "Company1",
                    AdminId = "1",
                },
                new Company
                {
                    Id = "Company2",
                    Name = "Company2",
                    Description = "Company2",
                    AdminId = "2",
                },
                new Company
                {
                    Id = "Company3",
                    Name = "Company3",
                    Description = "Company3",
                    AdminId = "3",
                },
                new Company
                {
                    Id = "Company4",
                    Name = "Company4",
                    Description = "Company4",
                    AdminId = "1",
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

        private Task<string> GetVs()
        {
            return Task.FromResult("1");
        }
    }
}
