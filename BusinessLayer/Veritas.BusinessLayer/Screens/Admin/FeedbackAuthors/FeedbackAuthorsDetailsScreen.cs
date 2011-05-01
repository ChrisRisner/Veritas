using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Screens.Admin.FeedbackAuthors
{
    public class FeedbackAuthorsDetailsScreen : ScreenBase
    {
        public BlogFeedbackAuthor BlogFeedbackAuthor { get; set; }

        public FeedbackAuthorsDetailsScreen(long blogFeedbackAuthorId)
        {
            LoadScreen(blogFeedbackAuthorId);
        }

        protected override void LoadScreen()
        {
            this.LoadScreen(0);
        }

        protected void LoadScreen(long blogFeedbackAuthorId = 0)
        {
            this.BlogFeedbackAuthor = repo.GetBlogFeedbackAuthorById(this.blogConfig.BlogConfigId, blogFeedbackAuthorId);
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
