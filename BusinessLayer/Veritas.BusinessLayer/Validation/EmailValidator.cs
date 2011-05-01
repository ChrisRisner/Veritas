using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Veritas.BusinessLayer.Validation
{
    public static class EmailValidator
    {
        //public const string EmailRegex = @"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$";

        public const string EmailRegex = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        public static bool EmailIsValid(string emailAddress)
        {
            Regex reg = new Regex(EmailRegex);
            Match mat = reg.Match(emailAddress);
            if (mat != null && mat.Success)
                return true;
            return false;
        }
    }
}
