﻿namespace Common
{
    using System.Collections.Generic;

    public class GlobalConstants
    {
        public const string AdministratorRoleName = "Administrator";

        public static ICollection<string> Roles => new List<string>(new string[] { "CompanyAdministrator", "ProjectAdministrator" });
    }
}