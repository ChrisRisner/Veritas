using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Screens.Admin.Entries
{
    public class EntriesIndexScreen : ScreenBase
    {
        public BlogEntry[] BlogEntries { get; set; }

        public EntriesIndexScreen()
        {
            LoadScreen();
        }

        protected override void LoadScreen()
        {
            this.BlogEntries = repo.GetBlogEntries(this.blogConfig.BlogConfigId).OrderByDescending(p => p.BlogEntryId).ToArray();
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
