using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Screens.Admin.Pages
{
    public class PagesIndexScreen : ScreenBase
    {
        public BlogPage[] BlogPages { get; set; }

        public PagesIndexScreen()
        {
            this.LoadScreen();
        }

        protected override void LoadScreen()
        {
            this.BlogPages = repo.GetBlogPages(this.blogConfig.BlogConfigId).ToArray();
        }

        public override bool IsValid
        {
            get { return true; }
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            throw new NotImplementedException();
        }
    }
}
