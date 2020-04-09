namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class BugHistory
    {
        public BugHistory()
        {
            this.Handlers = new HashSet<User>();
        }

        public int Id { get; set; }

        public string BugId { get; set; }

        public virtual Bug Bug { get; set; }

        public string FieldName { get; set; }

        public string OldDescriptionValue { get; set; }

        public string NewDescriptionValue { get; set; }

        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<User> Handlers { get; set; }
    }
}
