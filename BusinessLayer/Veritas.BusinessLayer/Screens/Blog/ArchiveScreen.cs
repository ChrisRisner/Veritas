using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.BusinessLayer.Screens.Shared;
using Veritas.BusinessLayer.Validation;
using System.Web;
using Veritas.BusinessLayer.Spam;
using Veritas.BusinessLayer.Logging;
using Veritas.DataLayer;
using Veritas.BusinessLayer.Email;
using Veritas.DataLayer.Models;
using System.Configuration;

namespace Veritas.BusinessLayer.Screens.Blog
{
    public class ArchiveScreen : ScreenBase
    {
        public BlogEntryScreen BlogEntryScreen { get; set; }

        private bool? _isSpam;
        private string _feedbackSenderAddress = ConfigurationManager.AppSettings["FeedbackFromAddress"];

        //Fields for Feedback data
        public string Name { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public bool NotifyMeOnFeedback { get; set; }
        public string FeedbackText { get; set; }
        public int iEntryId { get; set; }
        public string EntryName { get; set; }

        public ArchiveScreen(string entryName)
        {
            LoadScreen(entryName);
        }

        protected override void LoadScreen()
        {
            throw new NotImplementedException();
        }

        protected void LoadScreen(string entryName)
        {
            this._isSpam = null;
            this.BlogEntryScreen = new BlogEntryScreen
            {
                BlogEntry = repo.GetEntryByEntryNameAndBlogConfigId(this.blogConfig.BlogConfigId, entryName)
            };
            this.BlogEntryScreen.CheckForPreviousAndNextInSeries();
        }

        public override bool IsValid
        {
            get 
            {
                return (!string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.Email)
                        && !string.IsNullOrEmpty(this.FeedbackText) && !string.IsNullOrEmpty(this.EntryName)
                        && EmailValidator.EmailIsValid(this.Email) && this.iEntryId > 0);
            }
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(this.Name))
                items.Add("Name", "You must enter a name.");
            if (string.IsNullOrEmpty(this.Email))
                items.Add("Email", "You must enter an email address.");
            else if (!EmailValidator.EmailIsValid(this.Email))
                items.Add("Email", "The email address you have entered is invalid.");
            if (string.IsNullOrEmpty(this.FeedbackText))
                items.Add("FeedbackText", "You must enter a comment.");
            if (string.IsNullOrEmpty(this.EntryName))
                items.Add("NA", "There is a problem with commenting.  Please contact the site owner.");
            else if (this.iEntryId < 1)
                items.Add("NA", "There is a problem with commenting.  Please contact the site owner.");

            return items;
        }

        public void HandleUserInput()
        {
            //Encode our values
            this.Name = HttpUtility.HtmlEncode(this.Name);
            this.Email = HttpUtility.HtmlEncode(this.Email);
            this.WebSite = HttpUtility.HtmlEncode(this.WebSite);
            this.FeedbackText = HttpUtility.HtmlEncode(this.FeedbackText);

            //Add HTTP to the web site in case the commenter did not
            if (!string.IsNullOrEmpty(this.WebSite) && !this.WebSite.StartsWith("http://"))
            {
                this.WebSite = "http://" + this.WebSite;
            }
        }

        public bool IsSpam
        {
            get
            {
                if (!this._isSpam.HasValue)
                {
                    this._isSpam = new bool();
                    if (!string.IsNullOrEmpty(this.blogConfig.AkismetApiKey))
                    {
                        this._isSpam = SpamHandler.CheckCommentForSpamUsingAkismet(HttpContext.Current.Request,
                            this.Name, this.Email, this.WebSite, this.FeedbackText);
                    }
                    else
                        this._isSpam = false;
                }
                return (this._isSpam.Value);
            }
        }

        public void LogSpam()
        {
            this.Message = "Your comment was deemed to be spam.  Nice try!";

            StringBuilder commentBody = new StringBuilder();
            commentBody.Append("Name: ").AppendLine(this.Name);
            commentBody.Append("Email: ").AppendLine(this.Email);
            commentBody.Append("Website: ").AppendLine(this.WebSite);
            commentBody.Append("Feedback: ").AppendLine(this.FeedbackText);
            LoggingHandler.Log("Comment was deemed spam", commentBody.ToString(), "Warn", "ArchiveScreen");
        }

        public void SaveAndProcessFeedback()
        {
            //Save Feedback to database
            #region SaveFeedback
            //Update blog entry's feedback count if we the blog doesn't require feedback approval
            if (!this.blogConfig.FeedbackRequiresApproval)            
                this.BlogEntryScreen.BlogEntry.FeedbackCount++;
            //See if this feedback author already exists
            var feedbackAuthor = repo.GetBlogFeedbackAuthorByNameEmailWebsite(this.blogConfig.BlogConfigId,
                this.Name, this.Email, this.WebSite);
            //If feedback author doesn't already exist, create it
            if (feedbackAuthor == null)
            {
                feedbackAuthor = new BlogFeedbackAuthor();
                feedbackAuthor.Url = this.WebSite;
                feedbackAuthor.Name = this.Name;
                feedbackAuthor.LastUpdateDate = DateTime.Now;
                feedbackAuthor.Email = this.Email;
                feedbackAuthor.CreateDate = DateTime.Now;
                feedbackAuthor.FeedbackTotal = 1;
                feedbackAuthor.BlogConfigId = this.blogConfig.BlogConfigId;
                repo.Add(feedbackAuthor);
            }
            //Otherwise, update the existing author
            else
            {
                feedbackAuthor.FeedbackTotal++;
                feedbackAuthor.LastUpdateDate = DateTime.Now;
            }
            //Save our feedback author
            repo.Save();
            //Create our blog feedback
            BlogFeedback blogFeedback = new BlogFeedback()
            {
                BlogFeedbackAuthorId = feedbackAuthor.BlogFeedbackAuthorId,
                BlogConfigId = this.blogConfig.BlogConfigId,
                BlogEntryId = this.BlogEntryScreen.BlogEntry.BlogEntryId,
                Body = this.FeedbackText,
                CreateDate = DateTime.Now,
                IpAddress = HttpContext.Current.Request.UserHostAddress,
                //blogFeedback.IsBlogAuthor = isUserLoggedIn;   
                LastUpdateDate = DateTime.Now,
                NotifyAuthorOnFeedback = this.NotifyMeOnFeedback,
                Title = "re: " + this.BlogEntryScreen.BlogEntry.Title,
                UserAgent = HttpContext.Current.Request.UserAgent
            };
            //Check if we should require approval for this feedback or not
            if (this.blogConfig.FeedbackRequiresApproval)
            {
                blogFeedback.Status = (int)FeedbackStatus.PendingApproval;
            }
            else
            {
                blogFeedback.Status = (int)FeedbackStatus.Approved;
            }            
            repo.Add(blogFeedback);
            //Save our blog feedback
            repo.Save();
            #endregion

            //Notify authors / admins of feedback
            #region Notify Authors / Admins of Feedback
            List<string> recipients = new List<string>();
            if (this.BlogEntryScreen.BlogEntry.BlogUser.NotifyForFeedback)
                recipients.Add(this.BlogEntryScreen.BlogEntry.BlogUser.EmailAddress);
            if (this.blogConfig.NotifyAdminsForFeedback)
            {
                var admins = repo.GetBlogUsersInRoles(this.blogConfig.BlogConfigId, "Admin").Where(p => p.NotifyForFeedback).ToArray();
                foreach (var admin in admins)
                    if (!recipients.Contains(admin.EmailAddress))
                        recipients.Add(admin.EmailAddress);
            }
            if (recipients.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("The following comment was received from entry: ");
                sb.Append("http://");
                sb.Append(this.blogConfig.Host);
                sb.Append("/");
                sb.Append(this.BlogEntryScreen.BlogEntry.EntryName);
                sb.Append("<br />");
                sb.Append(this.BlogEntryScreen.BlogEntry.Title);
                sb.Append("<br /><br />Status: ");
                sb.Append(((FeedbackStatus)blogFeedback.Status).ToString());
                sb.Append("<br />Author: ");
                sb.Append(blogFeedback.BlogFeedbackAuthor.Name);
                sb.Append("<br />Author Email: ");
                sb.Append(blogFeedback.BlogFeedbackAuthor.Email);
                sb.Append("<br />Author Website: ");
                sb.Append(blogFeedback.BlogFeedbackAuthor.Url);
                sb.Append("<br /><br /><strong>Comment:</strong><br />");
                sb.Append(blogFeedback.Body);
                EmailHandler.SendEmail(_feedbackSenderAddress, sb.ToString(), string.Join(";", recipients), 
                    "New Comment: " + this.BlogEntryScreen.BlogEntry.Title, true);
                LoggingHandler.Log("Email Sent for Feedback to admin", "Email sent to " + string.Join(";", recipients), "Info", "ArchiveScreen");
            }
            #endregion
            
            //store creds into a cookie
            #region store creds in cookies            
            HttpContext.Current.Response.Cookies["userName"].Value = this.Name;
            HttpContext.Current.Response.Cookies["userName"].Expires = DateTime.Now.AddMonths(3);
            HttpContext.Current.Response.Cookies["email"].Value = this.Email;
            HttpContext.Current.Response.Cookies["email"].Expires = DateTime.Now.AddMonths(3);
            HttpContext.Current.Response.Cookies["website"].Value = this.WebSite;
            HttpContext.Current.Response.Cookies["website"].Expires = DateTime.Now.AddMonths(3);
            HttpContext.Current.Response.Cookies["NotifyMeOnFeedback"].Value = this.NotifyMeOnFeedback.ToString();
            HttpContext.Current.Response.Cookies["NotifyMeOnFeedback"].Expires = DateTime.Now.AddMonths(3);
            #endregion

            //Notify other commenters
            #region Notify Other Commentors
            if (this.blogConfig.EnableFeedbackAuthorNotifications)
            {
                recipients = new List<string>();
                var feedbacks = this.BlogEntryScreen.BlogEntry.BlogFeedbacks.Where(p => p.NotifyAuthorOnFeedback).ToArray();
                foreach (var notifyFeedback in feedbacks)
                {
                    //Try and find a newer blog entry by this author where they don't want to be notified
                    var negFeedback = feedbacks.Where(p => p.BlogFeedbackAuthor.Email == notifyFeedback.BlogFeedbackAuthor.Email)
                                .Where(p => p.NotifyAuthorOnFeedback == false)
                                .Where(p => p.CreateDate > notifyFeedback.CreateDate).SingleOrDefault();
                    if (negFeedback == null && !recipients.Contains(notifyFeedback.BlogFeedbackAuthor.Email)
                        && notifyFeedback.BlogFeedbackAuthor.Email != Email)
                        recipients.Add(notifyFeedback.BlogFeedbackAuthor.Email);
                }
                //Check the blacklist
                Blacklist[] blacklistedAddresses =
                    repo.GetBlacklistsForEmailAddresses(this.blogConfig.BlogConfigId, recipients.ToArray()).ToArray();
                if (blacklistedAddresses.Count() > 0)
                {
                    foreach (var item in blacklistedAddresses)
                        recipients.Remove(item.EmailAddress);
                }
                //Send email
                if (recipients.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("A comment was made on an entry you asked to be notified on.<br />  The entry is ");
                    sb.Append("http://");
                    sb.Append(this.blogConfig.Host);
                    sb.Append("/");
                    sb.Append(this.BlogEntryScreen.BlogEntry.EntryName);
                    sb.Append("<br />Title: ");
                    sb.Append(this.BlogEntryScreen.BlogEntry.Title);
                    sb.Append("<br />Author: ");
                    sb.Append(blogFeedback.BlogFeedbackAuthor.Name);
                    sb.Append("<br /><br /><strong>Comment:</strong><br />");
                    sb.Append(blogFeedback.Body);
                    sb.Append("<br /><br /><br /><br /><br />");
                    sb.Append("To opt out of receiving any emails from this site in the future, please click here: ");
                   
                    //Send individual email out to each person so we can customize the optout link
                    foreach (string recip in recipients)
                    {
                        string recipient = recip;
                        if (ConfigurationManager.AppSettings["environment"] == "dev")
                            recipient = ConfigurationManager.AppSettings["TestEmailAddress"];
                        string emailBody = sb.ToString() + "http://" + this.blogConfig.Host + "/home/optout/" + recipient;
                        EmailHandler.SendEmail(_feedbackSenderAddress, emailBody, recipient, this.BlogEntryScreen.BlogEntry.Title, true);
                        LoggingHandler.Log("Email Sent for Feedback to commentors", "Email sent to " + recipient, "Info", "ArchiveScreen"); 
                    }
                }
            }
            #endregion

        }

        /// <summary>
        /// Update our blog entry's view count
        /// </summary>
        public void UpdateEntryViewCount()
        {
            if (this.BlogEntryScreen != null && this.BlogEntryScreen.BlogEntry != null)
            {
                var blogEntryViewCount = this.BlogEntryScreen.BlogEntry.BlogEntryViewCounts.FirstOrDefault();
                if (blogEntryViewCount == null)
                {
                    blogEntryViewCount = new BlogEntryViewCount()
                    {
                        BlogConfigId = this.blogConfig.BlogConfigId,
                        BlogEntryId = this.BlogEntryScreen.BlogEntry.BlogEntryId,
                        WebCount = 0,
                        WebLastUpdated = DateTime.Now
                    };
                    repo.Add(blogEntryViewCount);
                }
                blogEntryViewCount.WebCount++;
                repo.Save();
            }
        }
    }
}
