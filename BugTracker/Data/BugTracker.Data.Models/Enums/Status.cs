namespace BugTracker.Data.Models.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum Status
    {
        [Display(Name = "New")]
        New = 100,

        [Display(Name = "In Progres")]
        InProgres = 200,

        [Display(Name = "Checked")]
        Checked = 300,

        [Display(Name = "Re Opened")]
        ReOpened = 400,

        [Display(Name = "Closed")]
        Closed = 500,
    }
}
