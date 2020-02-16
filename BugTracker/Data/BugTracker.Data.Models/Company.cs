namespace BugTracker.Data.Models
{
    using System.Collections.Generic;

    public class Company
    {
        public Company()
        {
            this.Projects = new HashSet<Project>();
            this.News = new HashSet<News>();
            this.Employees = new HashSet<User>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; }

        public ICollection<User> Employees { get; set; }

        public ICollection<News> News { get; set; }
    }
}
