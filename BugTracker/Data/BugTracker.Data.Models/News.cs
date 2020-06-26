namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class News
    {
        public News()
        {
            this.Users = new HashSet<UserNews>();
        }

        public int Id { get; set; }

        public string PosterId { get; set; }

        public virtual User User { get; set; }

        public string ProjectId { get; set; }

        public virtual Project Project { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Headline { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(160)]
        public string Body { get; set; }

        public DateTime DatePosted { get; set; }

        public virtual ICollection<UserNews> Users { get; set; }
    }
}
