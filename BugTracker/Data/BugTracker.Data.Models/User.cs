namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Projects = new HashSet<ProjectUser>();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string RealName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AccessLevel { get; set; }

        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<ProjectUser> Projects { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
