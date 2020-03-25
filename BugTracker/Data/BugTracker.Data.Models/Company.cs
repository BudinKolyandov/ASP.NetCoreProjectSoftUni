namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Company
    {
        public Company()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Projects = new HashSet<Project>();
            this.News = new HashSet<News>();
            this.Employees = new HashSet<User>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<User> Employees { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
