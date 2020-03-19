namespace BugTracker.Data.Models.Enums
{
    using System.ComponentModel;

    public enum Status
    {
        New = 100,
        [Description("In Progres")]
        InProgres = 200,
        Checked = 300,
        [Description("Re Opened")]
        ReOpened = 400,
        Closed = 500,
    }
}
