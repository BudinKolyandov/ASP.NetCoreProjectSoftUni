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

        public Bug Bug { get; set; }

        public string FieldName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTime ModifiedOn { get; set; }

        public ICollection<User> Handlers { get; set; }
    }
}
