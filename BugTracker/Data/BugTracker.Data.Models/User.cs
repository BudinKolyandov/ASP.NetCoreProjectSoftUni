namespace BugTracker.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string RealName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AccessLevel { get; set; }

        public string CompanyId { get; set; }

        public Company Company { get; set; }

        public ICollection<ProjectUser> Projects { get; set; }
    }
}
