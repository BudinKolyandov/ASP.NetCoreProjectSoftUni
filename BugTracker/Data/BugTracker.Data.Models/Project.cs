namespace BugTracker.Data.Models
{
    using System.Collections.Generic;

    public class Project
    {
        public Project()
        {
            this.Bugs = new HashSet<Bug>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string CompanyId { get; set; }

        public Company Company { get; set; }

        public ICollection<Bug> Bugs { get; set; }

        public ICollection<ProjectUser> Developers { get; set; }
    }
}
