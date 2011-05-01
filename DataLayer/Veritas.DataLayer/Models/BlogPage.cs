using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veritas.DataLayer.Models
{
    public partial class BlogPage
    {
        public BlogUser CreatedByUser
        {
            get
            {
                return this.BlogUser;
            }
        }

        public BlogUser LastUpdatedByUser
        {
            get
            {
                return this.BlogUser1;
            }
        }
    }
}
