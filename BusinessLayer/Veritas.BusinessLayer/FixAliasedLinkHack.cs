using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Veritas.BusinessLayer
{
    public static class FixAliasedLinkHack
    {
        public static string RemoveAlias(this string link, string alias)
        {
            // Find the href param and replace ...
            return link.Replace(alias, string.Empty);
        }

        public static string AutoRemoveAlias(this string link)
        {
            var appRoot = HttpContext.Current.Request.ApplicationPath;
            return appRoot != "/" ? link.RemoveAlias(appRoot) : link;
        }
    }
}
