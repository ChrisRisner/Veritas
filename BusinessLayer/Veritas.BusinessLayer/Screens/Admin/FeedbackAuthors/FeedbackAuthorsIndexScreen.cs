using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Screens.Admin.FeedbackAuthors
{
    public class FeedbackAuthorsIndexScreen : ScreenBase
    {
        public BlogFeedbackAuthor[] BlogFeedbackAuthors { get; set; }

        public FeedbackAuthorsIndexScreen()
        {
            LoadScreen();
        }

        protected override void LoadScreen()
        {
            this.BlogFeedbackAuthors = repo.GetBlogFeedbackAuthors(this.blogConfig.BlogConfigId).ToArray();
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
