using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veritas.DataLayer.Models
{
    public partial class BlogEntry
    {
        public bool IsMostRecentEntry { get; set; }

        public int PageViews
        {
            get
            {
                if (this.BlogEntryViewCounts.Count == 0)
                    return 0;
                return (this.BlogEntryViewCounts.First().WebCount);
            }
        }

        public string PostTypeText
        {
            get
            {
                if (this.PostType == (int)Veritas.DataLayer.PostType.Draft)
                    return "Draft";
                else if (this.PostType == (int)Veritas.DataLayer.PostType.Published)
                    return "Published";
                return "unknown";                
            }
        }
    }
}
