using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Screens.Home
{
    public class ViewContentScreen : ScreenBase
    {
        public BlogPage BlogPage { get; set; }

        public ViewContentScreen(string encodedTitle)
        {
            LoadScreen(encodedTitle);
        }

        protected override void LoadScreen()
        {
            throw new NotImplementedException();
        }

        protected void LoadScreen(string encodedTitle)
        {
            this.BlogPage = repo.GetBlogPageByEncodedTitle(this.blogConfig.BlogConfigId, encodedTitle);
        }

        public override bool IsValid
        {
            get { throw new NotImplementedException(); }
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            throw new NotImplementedException();
        }
    }
}
