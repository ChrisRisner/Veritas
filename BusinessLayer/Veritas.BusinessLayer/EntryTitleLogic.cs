using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veritas.BusinessLayer
{
    public static class EntryTitleLogic
    {
        public static string GetEntryNameFromTitle(string title)
        {
            return (title.Replace(";", "-").Replace(" ", "-").Replace(":", "-")
                    .Replace(".", "-").Replace("<", "&lt;").Replace(">", "&gt;")
                    .Replace("?", "-").Replace("%", "-").Replace("'", "%27")
                    .Replace("’", "%27"));
        }
    }
}
