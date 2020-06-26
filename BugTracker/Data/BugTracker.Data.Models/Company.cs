namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        public Company()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Projects = new HashSet<Project>();
            this.News = new HashSet<News>();
            this.Employees = new HashSet<CompanyUser>();
        }

        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(60)]
        public string Description { get; set; }

        public string AdminId { get; set; }

        public User Admin { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<News> News { get; set; }

        public virtual ICollection<CompanyUser> Employees { get; set; }
    }
}
