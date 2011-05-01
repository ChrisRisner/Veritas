using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using Veritas.BusinessLayer.Caching;

namespace Veritas.BusinessLayer.Screens.Home
{
    public class AboutScreen : ScreenBase
    {
        public BlogUser[] BlogUsers { get; set; }

        public AboutScreen()
        {
            LoadScreen();
        }

        protected override void LoadScreen()
        {
            this.BlogUsers = CacheHandler.GetBlogAuthors();
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
