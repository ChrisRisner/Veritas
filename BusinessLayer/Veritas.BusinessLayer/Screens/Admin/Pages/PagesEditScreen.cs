using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using Veritas.BusinessLayer.Session;

namespace Veritas.BusinessLayer.Screens.Admin.Pages
{
    public class PagesEditScreen : ScreenBase
    {
        public BlogPage BlogPage { get; set; }
        public string OriginalKeywords { get; set; }
        public string OriginalDescription { get; set; }
        public string OriginalTitle { get; set; }
        public string OriginalContent { get; set; }

        public PagesEditScreen(int blogPageId)
        {
            LoadScreen(blogPageId);
        }

        protected override void LoadScreen()
        {
            throw new NotImplementedException();
        }

        protected void LoadScreen(int blogPageId)
        {
            if (blogPageId > 0)
                this.BlogPage = repo.GetBlogPageByPageId(this.blogConfig.BlogConfigId, blogPageId);
            else
                this.BlogPage = new BlogPage();
            this.OriginalKeywords = this.BlogPage.Keywords;
            this.OriginalDescription = this.BlogPage.Description;
            this.OriginalTitle = this.BlogPage.PageTitle;
            this.OriginalContent = this.BlogPage.PageContent;
        }

        public override bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(this.BlogPage.Description) && 
                    !string.IsNullOrEmpty(this.BlogPage.EncodedTitle) &&
                    !string.IsNullOrEmpty(this.BlogPage.Keywords) &&
                    !string.IsNullOrEmpty(this.BlogPage.PageContent) &&
                    !string.IsNullOrEmpty(this.BlogPage.PageTitle) &&
                    this.PageTitleDoesNotExistForNewPage());
            }
        }

        private bool PageTitleDoesNotExistForNewPage()
        {
            if (this.BlogPage.BlogPageId > 0)
                return true;
            string encodedTitle = EntryTitleLogic.GetEntryNameFromTitle(this.BlogPage.PageTitle);
            var existingBlogPage = repo.GetBlogPageByEncodedTitle(this.blogConfig.BlogConfigId, encodedTitle);
            if (existingBlogPage != null)
                return false;
            return true;
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(this.BlogPage.Description))
                items.Add("BlogPage.Description", "You must enter a description.");
            //if (string.IsNullOrEmpty(this.BlogPage.EncodedTitle))
            //    items.Add("BlogPage.EncodedTitle", "You must enter a valid encoded title.");
            if (string.IsNullOrEmpty(this.BlogPage.Keywords))
                items.Add("BlogPage.Keywords", "You must enter keywords.");
            if (string.IsNullOrEmpty(this.BlogPage.PageContent))
                items.Add("BlogPage.PageContent", "You must enter page content.");
            if (string.IsNullOrEmpty(this.BlogPage.PageTitle))
                items.Add("BlogPage.PageTitle", "You must enter a valid page title.");
            if (!string.IsNullOrEmpty(this.BlogPage.PageTitle) &&
                    !PageTitleDoesNotExistForNewPage())
                items.Add("BlogPage.PageTitle", "This page title is already in use.  Please choose another.");

            return items;
        }

        public void SavePage()
        {
            //Create new audit record
            var newLog = new BlogLog()
            {
                BlogConfigId = this.blogConfig.BlogConfigId,
                CreateDate = DateTime.Now,
                EventLevel = "Audit",
                Logger = "PagesEditScreen"
            };

            if (this.BlogPage.BlogPageId > 0)
            {                
                newLog.Message = "Changing Page with ID = " + this.BlogPage.BlogPageId +
                    "\nOld Content: " + this.OriginalContent + " \nNew Content: " + this.BlogPage.PageContent +
                    "\nOld Description: " + this.OriginalDescription + " \nNew Description: " + this.BlogPage.Description +
                    "\nOld Title: " + this.OriginalTitle + " \nNew Title: " + this.BlogPage.PageTitle +
                    "\nOld Keywords: " + this.OriginalKeywords + " \nNew Keywords: " + this.BlogPage.Keywords +
                    "\nBy user: " + SessionHandler.CurrentUser.Username;
            }
            else
            {

                newLog.Message = "New Page " +
                    "\nNew Content: " + this.BlogPage.PageContent +
                    "\nNew Description: " + this.BlogPage.Description +
                    "\nNew Title: " + this.BlogPage.PageTitle +
                    "\nNew Keywords: " + this.BlogPage.Keywords;

                this.BlogPage.CreateDate = DateTime.Now;
                this.BlogPage.CreatedById = SessionHandler.CurrentUserId;
                this.BlogPage.BlogConfigId = this.blogConfig.BlogConfigId;
                this.CheckAndUpdateEncodedTitle();

                repo.Add(this.BlogPage);
            }
            this.BlogPage.LastUpdateDate = DateTime.Now;
            this.BlogPage.LastUpdatedById = SessionHandler.CurrentUserId;
            repo.Add(newLog);

            repo.Save();
        }

        public void CheckAndUpdateEncodedTitle()
        {
            if (this.BlogPage.BlogPageId == 0 && !string.IsNullOrEmpty(this.BlogPage.PageTitle))
                this.BlogPage.EncodedTitle = EntryTitleLogic.GetEntryNameFromTitle(this.BlogPage.PageTitle);
        }
    }
}
