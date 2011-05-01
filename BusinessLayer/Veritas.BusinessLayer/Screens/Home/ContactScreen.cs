using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.BusinessLayer.Caching;
using System.Web.Mvc;
using Veritas.BusinessLayer.Validation;
using System.Web;
using Veritas.BusinessLayer.Email;

namespace Veritas.BusinessLayer.Screens.Home
{
    [Bind(Exclude="AuthorSelectList")]
    public class ContactScreen : ScreenBase
    {
        public SelectList AuthorSelectList { get; set; }
        public bool MessageSent { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string EmailMessage { get; set; }
        public string SendToUsername { get; set; }

        public ContactScreen()
        {
            LoadScreen();
        }

        protected override void LoadScreen()
        {
            this.MessageSent = false;
            var authors = CacheHandler.GetBlogAuthors();

            List<string> authorNames = (from author in authors
                                        select author.Username).ToList();
            authorNames.Insert(0, "");
            authorNames.Add("All Authors");
            if (string.IsNullOrEmpty(this.SendToUsername))
                this.AuthorSelectList = new SelectList(authorNames);
            else
                this.AuthorSelectList = new SelectList(authorNames, this.SendToUsername);
        }

        public override bool IsValid
        {
            get 
            {
                return (!string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.Email)
                    && !string.IsNullOrEmpty(this.EmailMessage) && !string.IsNullOrEmpty(this.SendToUsername)
                    && EmailValidator.EmailIsValid(this.Email));
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
            if (string.IsNullOrEmpty(this.EmailMessage))
                items.Add("EmailMessage", "You must enter a message to send.");
            if (string.IsNullOrEmpty(this.SendToUsername))
                items.Add("AuthorSelectList", "You must select a recipient.");

            return items;
        }        

        public void ProcessContactRequest()
        {
            if (SendToUsername == "All Authors")
            {
                SendToUsername = "";
                foreach (var author in CacheHandler.GetBlogAuthors())
                    SendToUsername += author.EmailAddress + ";";
                SendToUsername = SendToUsername.Substring(0, SendToUsername.Length - 1);
            }
            else
            {
                SendToUsername = CacheHandler.GetBlogAuthors()
                    .Where(p => p.Username == SendToUsername).SingleOrDefault().EmailAddress;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("Name:  ");
            sb.AppendLine(this.Name);
            sb.Append("<br />Email:  ");
            sb.AppendLine(this.Email);
            sb.Append("<br />IP Address:  ");
            sb.AppendLine(HttpContext.Current.Request.UserHostAddress);
            sb.Append("<br />Web site:  ");
            sb.AppendLine(this.WebSite);
            sb.Append("<br />");
            sb.Append(this.EmailMessage);           
            EmailHandler.SendEmail("contact@" + this.blogConfig.Host + ".com",
                sb.ToString(), SendToUsername, "Message from " + this.blogConfig.Host, true);
            this.MessageSent = true;
        }
    }
}
