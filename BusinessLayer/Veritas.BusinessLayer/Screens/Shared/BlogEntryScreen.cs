using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using System.Configuration;
using Veritas.BusinessLayer.Caching;

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
            //Set the previous and next link URL            
        }

        public void CheckForPreviousAndNextInSeries()
        {
            if (this.BlogEntry.PreviousEntryInSeries.HasValue)
            {
                this.PreviousEntryUrlName = repo.GetEntryById(this.BlogEntry.PreviousEntryInSeries.Value).EntryName;
                //this.PreviousEntryLinkUrl = "http://" + CacheHandler.GetBlogConfig().Host + "/" +
                //    repo.GetEntryById(this.BlogEntry.PreviousEntryInSeries.Value).EntryName;
            }
            else
                this.PreviousEntryUrlName = null;
            if (this.BlogEntry.NextEntryInSeries.HasValue)
            {
                this.NextEntryUrlName = repo.GetEntryById(this.BlogEntry.NextEntryInSeries.Value).EntryName;
                //this.NextEntryLinkUrl = "http://" + CacheHandler.GetBlogConfig().Host + "/" +
                //    repo.GetEntryById(this.BlogEntry.NextEntryInSeries.Value).EntryName;
            }
            else
                this.NextEntryUrlName = null;
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

        public string PreviousEntryLinkText
        {
            get
            {
                if (this.BlogEntry.PreviousEntryInSeries.HasValue)
                {
                    if (String.IsNullOrEmpty(this.BlogEntry.PreviousEntryText))
                    {
                        return "<< Previous article in series";
                    }
                    else
                    {
                        return "<< " + this.BlogEntry.PreviousEntryText;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public string NextEntryLinkText
        {
            get
            {
                if (this.BlogEntry.NextEntryInSeries.HasValue)
                {
                    if (String.IsNullOrEmpty(this.BlogEntry.NextEntryText))
                    {
                        return "Next article in series >>";
                    }
                    else
                    {
                        return this.BlogEntry.NextEntryText + " >>";
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public string PreviousEntryUrlName { get; protected set; }
        public string NextEntryUrlName { get; protected set; }
    }
}
