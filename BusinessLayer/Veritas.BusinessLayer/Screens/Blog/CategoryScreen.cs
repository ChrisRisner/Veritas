using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.BusinessLayer.Screens.Shared;
using Veritas.BusinessLayer.Caching;

namespace Veritas.BusinessLayer.Screens.Blog
{
    public class CategoryScreen : ScreenBase
    {
        public BlogEntryScreen[] BlogEntryScreens { get; set; }
        public string CurrentStartAt { get; set; }
        public string CategoryName { get; set; }

        public CategoryScreen(string categoryName, string startAt)
        {
            int NumToStartAt = 0;
            Int32.TryParse(startAt, out NumToStartAt);
            this.CurrentStartAt = startAt;
            LoadScreen(categoryName, NumToStartAt);
        }

        public CategoryScreen(string categoryName, int startAt)
        {
            LoadScreen(categoryName, startAt);
        }

        protected override void LoadScreen()
        {
            throw new NotImplementedException();
        }

        protected void LoadScreen(string categoryName, int numToStartAt)
        {
            this.CategoryName = categoryName;
            this.BlogEntryScreens =
                (from entry in
                    repo.GetEntriesForCategoryFromStartPoint(this.blogConfig.BlogConfigId,
                        this.blogConfig.PostsPerPage, numToStartAt, categoryName).ToArray()
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

            int totalForCategory = 
                repo.GetTotalEntriesByBlogConfigIdAndCategory(this.blogConfig.BlogConfigId, this.CategoryName);

            if ((startAt + blogConfig.PostsPerPage) >= totalForCategory)
            {
                return "";
            }
            else
            {
                return "<a href=\"/Category/" + this.CategoryName + "?startat=" + (startAt + blogConfig.PostsPerPage) +
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
                return "<a href=\"/Category/" + this.CategoryName +"\">Newer Posts >></a>";
            //Otherwise, build our link
            return "<a href=\"/Category/" + this.CategoryName + "?startat=" + (startAt - blogConfig.PostsPerPage) +
                "\">Newer Posts >></a>";
        }
    }
}
