namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class News
    {
        public News()
        {
            this.Users = new HashSet<UserNews>();
        }

        public int Id { get; set; }

        public string PosterId { get; set; }

        public virtual User User { get; set; }

        public string Headline { get; set; }

        public string Body { get; set; }

        public DateTime DatePosted { get; set; }

        public virtual ICollection<UserNews> Users { get; set; }
    }
}
