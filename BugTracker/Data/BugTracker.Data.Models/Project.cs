namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Project
    {
        public Project()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Bugs = new HashSet<Bug>();
            this.Developers = new HashSet<ProjectUser>();
        }

        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(160)]
        public string Description { get; set; }

        public string AdminId { get; set; }

        public User Admin { get; set; }

        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Bug> Bugs { get; set; }

        public virtual ICollection<ProjectUser> Developers { get; set; }
    }
}
