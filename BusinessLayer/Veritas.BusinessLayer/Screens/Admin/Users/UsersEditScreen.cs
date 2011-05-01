using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using Veritas.BusinessLayer.Session;

namespace Veritas.BusinessLayer.Screens.Admin.Users
{
    public class UsersEditScreen : ScreenBase
    {
        public BlogUser BlogUser { get; set; }
        public string OldUsername { get; set; }
        public string OldEmail { get; set; }
        public string OldAbout { get; set; }

        public UsersEditScreen(int blogUserId)
        {
            LoadScreen(blogUserId);
        }

        private void LoadScreen(int blogUserId)
        {
            if (blogUserId > 0)
            {
                this.BlogUser = repo.GetBlogUserByUserId(this.blogConfig.BlogConfigId, blogUserId);
                this.OldUsername = this.BlogUser.Username;
                this.OldEmail = this.BlogUser.EmailAddress;
                this.OldAbout = this.BlogUser.About;
            }
            else
            {
                this.BlogUser = new BlogUser();
            }
        }

        protected override void LoadScreen()
        {
            throw new NotImplementedException();
        }

        public override bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(this.BlogUser.Username) &&
                    !string.IsNullOrEmpty(this.BlogUser.EmailAddress) &&
                    (!this.blogConfig.ShowAuthorsAbout || !string.IsNullOrEmpty(this.BlogUser.About)) &&
                    (this.BlogUser.BlogUserId > 0 || !string.IsNullOrEmpty(this.BlogUser.Password)));
                    
            }
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(this.BlogUser.Username))
                items.Add("BlogUser.UserName", "You must enter a user name.");
            if (string.IsNullOrEmpty(this.BlogUser.EmailAddress))
                items.Add("BlogUser.EmailAddress", "You must enter an email address.");
            if (this.blogConfig.ShowAuthorsAbout && string.IsNullOrEmpty(this.BlogUser.About))
                items.Add("BlogUser.About", "You must fill in some details about this user.");
            if (this.BlogUser.BlogUserId == 0 && string.IsNullOrEmpty(this.BlogUser.Password))
                items.Add("BlogUser.Password", "You must enter a password for a new user.");

            return items;
        }

        public void SaveUser()
        {
            //Create new audit record
            var newLog = new BlogLog()
            {
                BlogConfigId = this.blogConfig.BlogConfigId,
                CreateDate = DateTime.Now,
                EventLevel = "Audit",
                Logger = "UsersEditScreen"
            };

            if (this.BlogUser.BlogUserId > 0)
            {
                //newLog.Message = "Changing Page with ID = " + this.BlogPage.BlogPageId +
                //    "\nOld Content: " + this.OriginalContent + " \nNew Content: " + this.BlogPage.PageContent +
                //    "\nOld Description: " + this.OriginalDescription + " \nNew Description: " + this.BlogPage.Description +
                //    "\nOld Title: " + this.OriginalTitle + " \nNew Title: " + this.BlogPage.PageTitle +
                //    "\nOld Keywords: " + this.OriginalKeywords + " \nNew Keywords: " + this.BlogPage.Keywords +
                //    "\nBy user: " + SessionHandler.CurrentUser.Username;

                newLog.Message = "New User " +
                    "\nOld Username: " + this.OldUsername + "\nNew Username: " + this.BlogUser.Username +
                    "\nOld Email: " + this.OldEmail + "\nNew email: " + this.BlogUser.EmailAddress +
                    "\nOld About: " + this.OldAbout + "\nNew About: " + this.BlogUser.About +
                    "\nby user: " + SessionHandler.CurrentUser.Username;
            }
            else
            {

                //newLog.Message = "New Page " +
                //    "\nNew Content: " + this.BlogPage.PageContent +
                //    "\nNew Description: " + this.BlogPage.Description +
                //    "\nNew Title: " + this.BlogPage.PageTitle +
                //    "\nNew Keywords: " + this.BlogPage.Keywords;
                newLog.Message = "New User " +
                    "\nNew Username: " + this.BlogUser.Username +
                    "\nNew email: " + this.BlogUser.EmailAddress +
                    "\nNew About: " + this.BlogUser.About +
                    "\nby user: " + SessionHandler.CurrentUser.Username;

                this.BlogUser.CreateDate = DateTime.Now;
                this.BlogUser.BlogConfigId = this.blogConfig.BlogConfigId;

                repo.Add(this.BlogUser);
            }
            this.BlogUser.LastUpdateDate = DateTime.Now;
            repo.Add(newLog);

            repo.Save();
        }
    }
}
