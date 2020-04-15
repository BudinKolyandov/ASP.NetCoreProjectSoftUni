namespace BugTracker.Services.Data.Tests
{
    using System.Collections.Generic;

    using BugTracker.Data;
    using BugTracker.Data.Models;
    using BugTracker.Services.Assignments;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class AssignmentsServiceTests
    {
        [Fact]
        public void GetAllUsersInCompanyShouldGetAllUsers()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AllUsersInCompany")
                .Options;
            var context = new ApplicationDbContext(options);
            var service = new AssignmentsService(context, null);

            service.GetAllUsersInCompany<string[]>("1");

            // context.Assignments
            // service.GetAllForUser<>("1");
        }
    }
}
