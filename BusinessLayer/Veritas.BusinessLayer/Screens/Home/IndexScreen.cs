using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using Veritas.BusinessLayer.Screens.Shared;
using Veritas.BusinessLayer.Caching;
using Veritas.DataLayer;

namespace Veritas.BusinessLayer.Screens.Home
{
    public class IndexScreen : ScreenBase
    {
        public BlogEntryScreen[] BlogEntryScreens { get; set; }
        public string CurrentStartAt { get; set; }


        public IndexScreen(string startAt)
        {
            
            int NumToStartAt = 0;
            Int32.TryParse(startAt, out NumToStartAt);
            this.CurrentStartAt = startAt;
            LoadScreen(NumToStartAt);
        }

        public IndexScreen(int startAt)
        {
            LoadScreen(startAt);
        }

        protected override void LoadScreen()
        {
            LoadScreen(0);
        }

        protected void LoadScreen(int startAt)
        {
            this.BlogEntryScreens = 
                (from entry in
                    repo.GetEntriesFromStartPoint(this.blogConfig.BlogConfigId,
                        this.blogConfig.PostsPerPage,
                        startAt)
                        .Where(p => p.PostType == (int) PostType.Published)
                        .ToArray()
                select new BlogEntryScreen
                {
                    BlogEntry = entry,
                    OnlyItemOnPage = false
                }).ToArray();

            if (this.BlogEntryScreens.Length > 0)
                this.BlogEntryScreens[0].BlogEntry.IsMostRecentEntry = true;
        }

        public override bool IsValid
        {
            get { throw new NotImplementedException(); }
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            throw new NotImplementedException();
        }


        public string LinkToOlderEntries(string currentStartAt)
        {
            int startAt = 0;
            Int32.TryParse(currentStartAt, out startAt);

            if ((startAt + blogConfig.PostsPerPage) >= CacheHandler.GetTotalBlogEntryPost())
            {
                return "";
            }
            else
            {
                return "<a href=\"/?startat=" + (startAt + blogConfig.PostsPerPage) +
                    "\"><< Older Posts</a>";
            }                
        }

        public string LinkToNewerEntries(string currentStartAt)
        {
            int startAt = 0;
            Int32.TryParse(currentStartAt, out startAt);
            //If we're at the beginning, don't show anything
            if (startAt == 0)
                return "";

            startAt -= blogConfig.PostsPerPage;
            if (startAt <= 0)
                return "<a href=\"/\">Newer Posts >></a>";
            //Otherwise, build our link
            return "<a href=\"/?startat=" + (startAt - blogConfig.PostsPerPage) +
                "\">Newer Posts >></a>";            
        }
    }
}
