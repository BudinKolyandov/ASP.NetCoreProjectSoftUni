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

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetAllForAdminShouldGetEveryCompany()
        {
            CompaniesService mockService = this.ServiceSetup();
            var company = mockService.GetById<DetailsCompanyViewModel>("Company1");
            var result = mockService.GetAllForAdminUser<DetailsCompanyViewModel>("1");

            Assert.Equal(1, result.Count());
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
            var falseResult = mockService.CompanyExists("Company4");
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

        private CompaniesService ServiceSetup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var context = new ApplicationDbContext(options);
            var mockService = new CompaniesService(context, null, null);
            context.Users.AddRange(this.GetSampleUsers());
            context.Companies.AddRange(this.GetSampleCompanies());
            context.SaveChanges();
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            return mockService;
        }

        private List<User> GetSampleUsers()
        {
            var output = new List<User>
            {
                new User
            {
                Id = "1",
                UserName = "User1",
            },
                new User
            {
                Id = "2",
                UserName = "User2",
            },
                new User
            {
                Id = "3",
                UserName = "User3",
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
            };

            return output;
        }

        private Task<string> GetVs()
        {
            return Task.FromResult("1");
        }
    }
}
