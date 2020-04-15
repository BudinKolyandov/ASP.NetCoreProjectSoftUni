namespace Common
{
    using System.Collections.Generic;

    public class GlobalConstants
    {
        public const string AdministratorRoleName = "Administrator";

        public const string SendGridEmail = "budin_kolyandov@mail.bg";

        public static ICollection<string> Roles => new List<string>(new string[] { "CompanyAdministrator", "ProjectAdministrator", "AwaitingAproval" });
    }
}
