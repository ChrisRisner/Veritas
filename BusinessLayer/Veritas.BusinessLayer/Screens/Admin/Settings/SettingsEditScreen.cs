using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using Veritas.BusinessLayer.Caching;
using System.IO;
using System.Web;
using Veritas.DataLayer;

namespace Veritas.BusinessLayer.Screens.Admin.Settings
{
    public class SettingsEditScreen : ScreenBase
    {
        public BlogConfig newBlogConfig { get; set; }

        public SettingsEditScreen()
        {
            LoadScreen();
        }

        protected override void LoadScreen()
        {
            //this.newBlogConfig = repo.GetblogConfigByblogConfigId(this.newBlogConfig.blogConfigId);
            CacheHandler.ResetCache();
            //var pullConfigFresh = CacheHandler.GetBlogConfig();
            this.newBlogConfig = repo.GetBlogConfigByHostname(HttpContext.Current.Request.Url.Host);
            this.newBlogConfig.LoadConfigFromXml();
        }

        public override bool IsValid
        {
            get 
            {
                if (this.newBlogConfig.ShowBlogAbout && string.IsNullOrEmpty(this.newBlogConfig.BlogAbout))
                    return false;
                if (this.newBlogConfig.PostsPerPage < 1)
                    return false;
                if (this.newBlogConfig.PostCount < 0)
                    return false;
                if (!string.IsNullOrEmpty(this.newBlogConfig.Skin) &&
                    !File.Exists(HttpContext.Current.Server.MapPath("/content/themes/") + this.newBlogConfig.Skin + ".css"))
                    return false;
                if (string.IsNullOrEmpty(this.newBlogConfig.Title))
                    return false;
                if (string.IsNullOrEmpty(this.newBlogConfig.SmtpServer))
                    return false;

                return true;
            }
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();


            if (this.newBlogConfig.ShowBlogAbout && string.IsNullOrEmpty(this.newBlogConfig.BlogAbout))
                items.Add("blogConfig.ShowBlogAbout", "Show Blog About is selected but the Blog About info is not filled in.");
            if (this.newBlogConfig.PostsPerPage < 1)
                items.Add("blogConfig.PostsPerPage", "The posts per page must be greater than 0.");
            if (this.newBlogConfig.PostCount < 0)
                items.Add("blogConfig.PostCount", "The post count must be zero or greater.");
            if (!string.IsNullOrEmpty(this.newBlogConfig.Skin) &&
                !File.Exists(HttpContext.Current.Server.MapPath("/content/themes/") + this.newBlogConfig.Skin + ".css"))
                items.Add("blogConfig.Skin", "You have chosen a skin that does not exist in themes.");
            if (string.IsNullOrEmpty(this.newBlogConfig.Title))
                items.Add("blogConfig.Title", "You must enter a blog title.");
            if (string.IsNullOrEmpty(this.newBlogConfig.SmtpServer))
                items.Add("blogConfig.SmtpServer", "You must enter a SMTP server for emails.");

            return items;
        }

        public void SaveConfig()
        {
            newBlogConfig.BuildXmlFromConfig();
            repo.Save();
            CacheHandler.ResetCache();
        }
    }
}
