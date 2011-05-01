using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veritas.DataLayer.Models
{
    public class BlogCategoryTag
    {
        public int BlogCategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public int CategoryUseCount { get; set; }
        public int TotalArticles { get; set; }
    }
}
