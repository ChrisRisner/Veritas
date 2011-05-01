using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using System.Configuration;

namespace Veritas.BusinessLayer.Screens.Shared
{
    public class BlogEntryScreen : ScreenBase
    {
        public bool OnlyItemOnPage { get; set; }

        public BlogEntry BlogEntry { get; set; }

        public BlogEntryScreen()
        {
            LoadScreen();
        }

        protected override void LoadScreen()
        {
            this.OnlyItemOnPage = true;
        }

        public override bool IsValid
        {
            get { throw new NotImplementedException(); }
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            throw new NotImplementedException();
        }

        public string DisqusScript
        {
            get
            {
                if (this.blogConfig.BlogCommentInfo.UseDisqusComments
                    && !string.IsNullOrEmpty(this.blogConfig.BlogCommentInfo.DisqusCommentScript))
                {
                    string script = this.blogConfig.BlogCommentInfo.DisqusCommentScript;
                    script = script.Replace("disquswebsiteaccount", this.blogConfig.BlogCommentInfo.DisqusAccountName);
                    script = script.Replace("###disqusidentier###", this.BlogEntry.EntryName);
                    script = script.Replace("###disqustitle###", this.BlogEntry.Title);
                    script = script.Replace("###disqusurl###", "http://" + this.blogConfig.Host + "/" + this.BlogEntry.EntryName);

                    //Set our developer flag to on if we're in test
                    string environment = ConfigurationManager.AppSettings["environment"];
                    if (environment != "prod")
                        script = script.Replace("disqus_developer = 0", "disqus_developer = 1");
                    return script;
                }
                return "";
            }
        }
    }
}
