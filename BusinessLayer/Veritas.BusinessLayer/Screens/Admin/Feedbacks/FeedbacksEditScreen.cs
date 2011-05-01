using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using Veritas.DataLayer;
using System.Web.Mvc;
using Veritas.BusinessLayer.Session;
using Veritas.BusinessLayer.Caching;

namespace Veritas.BusinessLayer.Screens.Admin.Feedbacks
{

    [Bind(Exclude = "StatusSelectList")]
    public class FeedbacksEditScreen : ScreenBase
    {       
        public BlogFeedback BlogFeedback { get; set; }
        public SelectList StatusSelectList { get; set; }
        public String OriginalBody { get; set; }
        public int OriginalStatus { get; set; }


        public FeedbacksEditScreen(long feedbackId)
        {
            if (feedbackId > 0)
                LoadScreen(feedbackId);
            else
                LoadScreen();
        }

        protected override void LoadScreen()
        {
            LoadScreen(0);
        }

        protected void LoadScreen(long feedbackId)
        {
            if (feedbackId > 0)
                this.BlogFeedback = repo.GetBlogFeedbackByFeedbackId(this.blogConfig.BlogConfigId, feedbackId);
            else
                this.BlogFeedback = new BlogFeedback();
            this.OriginalBody = this.BlogFeedback.Body;
            this.OriginalStatus = this.BlogFeedback.Status;

            Dictionary<int, string> statuses = new Dictionary<int, string>();
            statuses.Add(0, "Pending");
            statuses.Add(1, "Approved");
            statuses.Add(2, "Denied");

            this.StatusSelectList = new SelectList(statuses, "key", "value", this.BlogFeedback.Status);            
        }

        public override bool IsValid
        {
            get 
            {
                return (!string.IsNullOrEmpty(this.BlogFeedback.Body) && !string.IsNullOrEmpty(this.BlogFeedback.Title)
                    && this.BlogFeedback.BlogFeedbackId > 0 && this.BlogFeedback.BlogConfigId == this.blogConfig.BlogConfigId
                    && this.BlogFeedback.BlogEntryId > 0);
            }
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(this.BlogFeedback.Body))
                items.Add("BlogFeedback.Body", "You must enter a feedback body.");
            if (string.IsNullOrEmpty(this.BlogFeedback.Title))
                items.Add("BlogFeedback.Title", "You must enter a title.");
            if (this.BlogFeedback.BlogFeedbackId < 1)
                items.Add("BlogFeedback.BlogFeedbackId", "You cannot add a new feedback using this interface.");
            if (this.BlogFeedback.BlogConfigId != this.blogConfig.BlogConfigId)
                items.Add("BlogFeedback.BlogConfigId", "A blog config must be associated with this feedback.");
            if (this.BlogFeedback.BlogEntryId < 1)
                items.Add("BlogFeedback.BlogEntryIdId", "You must associate a blog entry with this feedback.");

            return items;
        }

        public void DenyFeedback()
        {
            if (this.BlogFeedback.Status == (int)FeedbackStatus.Approved)
            {
                this.BlogFeedback.BlogEntry.FeedbackCount--;
                //Pull the config and update it
                var currentConfig = repo.GetBlogConfigByBlogConfigId(this.blogConfig.BlogConfigId);
                currentConfig.FeedbackCount--;
                //then reset the cache so the next time blogconfig is used, it's repulled
                CacheHandler.ResetCache();
            }
            this.BlogFeedback.Status = (int)FeedbackStatus.Denied;            
            repo.Save();
        }

        public void ApproveFeedback()
        {
            if (this.BlogFeedback.Status != (int)FeedbackStatus.Approved)
            {
                this.BlogFeedback.BlogEntry.FeedbackCount++;
                //Pull the config and update it
                var currentConfig = repo.GetBlogConfigByBlogConfigId(this.blogConfig.BlogConfigId);
                currentConfig.FeedbackCount++;
                //then reset the cache so the next time blogconfig is used, it's repulled
                CacheHandler.ResetCache();
            }
            this.BlogFeedback.Status = (int)FeedbackStatus.Approved;
            repo.Save();
        }

        public void SaveFeedback()
        {
            //Create new audit record
            var newLog = new BlogLog()
            {
                BlogConfigId = this.blogConfig.BlogConfigId,
                CreateDate = DateTime.Now,
                EventLevel = "Audit",
                Logger = "FeedbacksEditScreen"
            };

            if (this.BlogFeedback.BlogFeedbackId > 0)
            {
                //category = repo.GetBlogCategoryByCategoryId(this.blogConfig.BlogConfigId, this.BlogCategoryId.Value);

                newLog.Message = "Changing Feedback with ID = " + this.BlogFeedback.BlogFeedbackId +
                    "\nOld Body: " + this.OriginalBody + " \nNew Body: " + this.BlogFeedback.Body +
                    "\nOld Status: " + this.OriginalStatus + " \nNew Status: " + this.BlogFeedback.Status +
                    "\nBy user: " + SessionHandler.CurrentUser.Username;
            }
            else
            {
                throw new Exception("Not currently capable of adding new feedback here.  Need to handle creating the author (if necessary) or " +
                    " looking up the author from the feedback author table as well as setting all the other inital data");
                newLog.Message = "New Feedback " +
                    "\nNew Body: " + this.BlogFeedback.Body +
                    "\nNew Status: " + this.BlogFeedback.Status;

                this.BlogFeedback.CreateDate = DateTime.Now;
                

                repo.Add(this.BlogFeedback);
            }
            this.BlogFeedback.LastUpdateDate = DateTime.Now;            
            repo.Add(newLog);

            repo.Save();
        }
    }
}

