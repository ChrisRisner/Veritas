using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using Veritas.DataLayer;

namespace Veritas.BusinessLayer.Screens.Admin.Feedbacks
{
    public class FeedbacksIndexScreen : ScreenBase
    {
        public BlogFeedback[] BlogFeedbacks { get; set; }
        public string TypeText { get; set; }
        public string TypeQueryString { get; set; }
        public FeedbackStatus? BlogFeedbackStatus { get; set; }


        public FeedbacksIndexScreen(string type)
        {
            LoadScreen(type);
        }

        protected override void LoadScreen()
        {
            LoadScreen(null);
        }

        protected void LoadScreen(string type)
        {
            switch (type)
            {
                case "pending":
                    this.BlogFeedbacks = repo.GetBlogFeedbacksByBlogConfigId(this.blogConfig.BlogConfigId)
                        .Where(p => p.Status == (int)FeedbackStatus.PendingApproval)
                        .OrderByDescending(p => p.BlogFeedbackId).ToArray();
                    this.BlogFeedbackStatus = FeedbackStatus.PendingApproval;
                    this.TypeText = "Pending";
                    this.TypeQueryString = "pending";
                    break;
                case "denied":
                    this.BlogFeedbacks = repo.GetBlogFeedbacksByBlogConfigId(this.blogConfig.BlogConfigId)
                        .Where(p => p.Status == (int)FeedbackStatus.Denied)
                        .OrderByDescending(p => p.BlogFeedbackId).ToArray();
                    this.BlogFeedbackStatus = FeedbackStatus.Denied;
                    this.TypeText = "Denied";
                    this.TypeQueryString = "denied";
                    break;
                case "approved":
                    this.BlogFeedbacks = repo.GetBlogFeedbacksByBlogConfigId(this.blogConfig.BlogConfigId)
                        .Where(p => p.Status == (int)FeedbackStatus.Approved)
                        .OrderByDescending(p => p.BlogFeedbackId).ToArray();
                    this.BlogFeedbackStatus = FeedbackStatus.Approved;
                    this.TypeText = "Approved";
                    this.TypeQueryString = "approved";
                    break;
                default:
                    this.BlogFeedbacks = repo.GetBlogFeedbacksByBlogConfigId(this.blogConfig.BlogConfigId)
                        .OrderByDescending(p => p.BlogFeedbackId).ToArray();
                    this.BlogFeedbackStatus = null;
                    this.TypeText = "All";
                    this.TypeQueryString = "all";
                    break;
            }            
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
