using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using System.Web.Mvc;
using Veritas.BusinessLayer.Session;
using Veritas.DataLayer;
using Veritas.BusinessLayer.Caching;

namespace Veritas.BusinessLayer.Screens.Admin.Entries
{
    [Bind(Exclude = "PostTypeSelectList")]
    public class EntriesEditScreen : ScreenBase
    {
        public BlogEntry BlogEntry { get; set; }
        public SelectList PostTypeSelectList { get; set; }
        public string OriginalKeywords { get; set; }
        public int OriginalPostType { get; set; }
        public string OriginalShort { get; set; }
        public string OriginalText { get; set; }
        public string OriginalTitle { get; set; }

        public EntriesEditScreen(long blogEntryId)
        {
            LoadScreen(blogEntryId);
        }

        protected override void LoadScreen()
        {
            LoadScreen(0);
        }

        protected void LoadScreen(long blogEntryId)
        {
            if (blogEntryId > 0)
                this.BlogEntry = repo.GetEntryByEntryIDBlogConfigId(this.blogConfig.BlogConfigId, blogEntryId);
            else
                this.BlogEntry = new DataLayer.Models.BlogEntry();
            //Set Original Values
            this.OriginalKeywords = this.BlogEntry.Keywords;
            this.OriginalPostType = this.BlogEntry.PostType;
            this.OriginalShort = this.BlogEntry.Short;
            this.OriginalText = this.BlogEntry.Text;
            this.OriginalTitle = this.BlogEntry.Title;


            Dictionary<int, string> postTypes = new Dictionary<int, string>();
            postTypes.Add(0, "Draft");
            postTypes.Add(1, "Published");

            this.PostTypeSelectList = new SelectList(postTypes, "key", "value", this.BlogEntry.PostType);            
        }

        public override bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(this.BlogEntry.EntryName) &&
                    !string.IsNullOrEmpty(this.BlogEntry.Keywords) &&
                    !string.IsNullOrEmpty(this.BlogEntry.Short) &&
                    !string.IsNullOrEmpty(this.BlogEntry.Text) &&
                    !string.IsNullOrEmpty(this.BlogEntry.Title) &&
                    this.EntryTitleDoesNotExistForNewPage());
            }
        }

        private bool EntryTitleDoesNotExistForNewPage()
        {
            if (this.BlogEntry.BlogEntryId > 0)
                return true;
            string encodedTitle = EntryTitleLogic.GetEntryNameFromTitle(this.BlogEntry.Title);
            var existingBlogEntry = repo.GetEntryByEntryNameAndBlogConfigId(this.blogConfig.BlogConfigId, encodedTitle);
            if (existingBlogEntry != null)
                return false;
            return true;
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(this.BlogEntry.EntryName))
                items.Add("BlogPage.EntryName", "You must enter an entry name.");
            //if (string.IsNullOrEmpty(this.BlogPage.EncodedTitle))
            //    items.Add("BlogPage.EncodedTitle", "You must enter a valid encoded title.");
            if (string.IsNullOrEmpty(this.BlogEntry.Keywords))
                items.Add("BlogEntry.Keywords", "You must enter keywords.");
            if (string.IsNullOrEmpty(this.BlogEntry.Short))
                items.Add("BlogEntry.Short", "You must enter a page short.");
            if (string.IsNullOrEmpty(this.BlogEntry.Title))
                items.Add("BlogEntry.Title", "You must enter a valid title.");
            if (string.IsNullOrEmpty(this.BlogEntry.Text))
                items.Add("BlogEntry.Text", "You must enter valid entry content.");
            if (!string.IsNullOrEmpty(this.BlogEntry.Title) &&
                    !EntryTitleDoesNotExistForNewPage())
                items.Add("BlogEntry.Title", "This entry title is already in use.  Please choose another.");

            return items;
        }

        public void CheckAndUpdateEntryTitle()
        {
            if (this.BlogEntry.BlogEntryId == 0 && !string.IsNullOrEmpty(this.BlogEntry.Title))
                this.BlogEntry.EntryName = EntryTitleLogic.GetEntryNameFromTitle(this.BlogEntry.Title);
        }

        public void SaveEntry()
        {
            //Create new audit record
            var newLog = new BlogLog()
            {
                BlogConfigId = this.blogConfig.BlogConfigId,
                CreateDate = DateTime.Now,
                EventLevel = "Audit",
                Logger = "EntriesEditScreen"
            };

            if (this.BlogEntry.BlogEntryId > 0)
            {
                newLog.Message = "Changing Entry with ID = " + this.BlogEntry.BlogEntryId +
                    "\nOld Text: " + this.OriginalText + " \nNew Text: " + this.BlogEntry.Text +
                    "\nOld Short: " + this.OriginalShort + " \nNew Short: " + this.BlogEntry.Short +
                    "\nOld Title: " + this.OriginalTitle + " \nNew Title: " + this.BlogEntry.Title +
                    "\nOld PostType: " + this.OriginalPostType + " \nNew PostType: " + this.BlogEntry.PostType +
                    "\nOld Keywords: " + this.OriginalKeywords + " \nNew Keywords: " + this.BlogEntry.Keywords +
                    "\nBy user: " + SessionHandler.CurrentUser.Username;

                if (this.OriginalPostType == (int)PostType.Draft &&
                    this.BlogEntry.PostType == (int)PostType.Published)
                    this.BlogEntry.PublishDate = DateTime.Now;

                if (this.OriginalPostType == (int)PostType.Draft && this.BlogEntry.PostType == (int)PostType.Published)
                {
                    //Pull the config and update it
                    var currentConfig = repo.GetBlogConfigByBlogConfigId(this.blogConfig.BlogConfigId);
                    currentConfig.PostCount++;
                    //then reset the cache so the next time blogconfig is used, it's repulled
                    CacheHandler.ResetCache();
                }
                else if (this.OriginalPostType == (int)PostType.Published && this.BlogEntry.PostType == (int)PostType.Draft)
                {
                    //Pull the config and update it
                    var currentConfig = repo.GetBlogConfigByBlogConfigId(this.blogConfig.BlogConfigId);
                    currentConfig.PostCount--;
                    //then reset the cache so the next time blogconfig is used, it's repulled
                    CacheHandler.ResetCache();
                }

            }
            else
            {

                newLog.Message = "New Page " +
                    "\nNew Text: " + this.BlogEntry.Text +
                    "\nNew Short: " + this.BlogEntry.Short +
                    "\nNew Title: " + this.BlogEntry.Title +
                    "\nNew PostType: " + this.BlogEntry.PostType +
                    "\nNew Keywords: " + this.BlogEntry.Keywords;

                this.BlogEntry.CreateDate = DateTime.Now;
                this.BlogEntry.BlogAuthorId = SessionHandler.CurrentUserId;
                this.BlogEntry.BlogConfigId = this.blogConfig.BlogConfigId;
                this.BlogEntry.PublishDate = DateTime.Now;
                this.CheckAndUpdateEntryTitle();

                repo.Add(this.BlogEntry);
            }
            this.BlogEntry.LastUpdateDate = DateTime.Now;
            //this.BlogEntry.LastUpdatedById = SessionHandler.CurrentUserId;
            //repo.Add(newLog);

            repo.Save();
        }
    }
}
