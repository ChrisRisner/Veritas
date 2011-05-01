using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using Veritas.DataLayer;

namespace Veritas.BusinessLayer.Screens.Admin
{
    public class IndexScreen : ScreenBase
    {
        public int EntryCount { get; set; }
        public int PageCount { get; set; }
        public int FeedbackTotalCount { get; set; }
        public int FeedbackNotYetApproved { get; set; }
        public int FeedbackDeniedCount { get; set; }
        public int CategoryCount { get; set; }

        public BlogFeedback[] LastThreeFeedbacks { get; set; }        

        public IndexScreen()
        {
            LoadScreen();
        }

        protected override void LoadScreen()
        {
            this.EntryCount = repo.GetBlogEntries(this.blogConfig.BlogConfigId).Count();
            this.FeedbackTotalCount = repo.GetBlogFeedbacksByBlogConfigId(this.blogConfig.BlogConfigId).Count();
            this.FeedbackNotYetApproved = repo.GetBlogFeedbacksByBlogConfigId(this.blogConfig.BlogConfigId)
                .Where(p => p.Status == (int) FeedbackStatus.PendingApproval).Count();
            this.FeedbackDeniedCount = repo.GetBlogFeedbacksByBlogConfigId(this.blogConfig.BlogConfigId)
                .Where(p => p.Status == (int)FeedbackStatus.Denied).Count();
            this.CategoryCount = repo.GetBlogCategories(this.blogConfig.BlogConfigId).Count();
            this.PageCount = repo.GetBlogPages(this.blogConfig.BlogConfigId).Count();

            this.LastThreeFeedbacks = repo.GetBlogFeedbacksByBlogConfigId(this.blogConfig.BlogConfigId)
                .OrderByDescending(p => p.BlogFeedbackId).Take(3).ToArray();
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
