namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Projects = new HashSet<ProjectUser>();
        }

        public string RealName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AccessLevel { get; set; }

        public string CompanyId { get; set; }

        public Company Company { get; set; }

        public ICollection<ProjectUser> Projects { get; set; }
    }
}
