using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Web;

namespace Veritas.DataLayer.Models
{
    public partial class BlogConfig
    {
        public bool AllowComments { get; set; }
        public string AkismetApiKey { get; set; }
        public string BlogAbout { get; set; }
        public BlogCommentInfo BlogCommentInfo { get; set; }
        public string BlogHeaderImage { get; set; }
        public bool BlogHeaderIsImage { get; set; }
        public BlogMarketingInfo BlogMarketingInfo { get; set; }
        public string CopyrightText { get; set; }
        public int? DaysUntilCommentsClose { get; set; }
        public string DefaultLogoUrl { get; set; }
        public bool EnableFeedbackAuthorNotifications { get; set; }
        public bool EnableFeedbackRssFeed { get; set; }        
        public string FacebookUrl { get; set; }
        public string FeedburnerName { get; set; }
        public int FeedbackCount { get; set; }
        public bool FeedbackRequiresApproval { get; set; }
        public bool IsActive { get; set; }
        public string GoogleApiKey { get; set; }
        public string GooglePlusUrl { get; set; }
        public string HeaderScript { get; set; }
        public string Language { get; set; }
        public string LogEmailAddress { get; set; }
        public string LogFilePath { get; set; }
        public bool LogToDb { get; set; }
        public bool LogToEmail { get; set; }
        public bool LogToFile { get; set; }
        public bool NotifyAdminsForFeedback { get; set; }
        public int PostCount { get; set; }
        public int PostsPerPage { get; set; }
        public string RssUrl { get; set; }
        public bool RssShowLimitedEntryInFeed { get; set; }
        public string SecurityQuestionOne { get; set; }
        public string SecurityQuestionTwo { get; set; }
        public string SecurityQuestionThree { get; set; }
        public string SecurityQuestionAnswerOne { get; set; }
        public string SecurityQuestionAnswerTwo { get; set; }
        public string SecurityQuestionAnswerThree { get; set; }
        public bool ShowAuthorsAbout { get; set; }
        public bool ShowBlogAbout { get; set; }
        public bool ShowGooglePlusOne { get; set; }
        public bool ShowGravatars { get; set; }
        public bool ShowNextPreviousAtTop { get; set; }
        public bool ShowNextPreviousAtBottom { get; set; }
        private string _skin;
        public string Skin
        {
            get { return (String.IsNullOrEmpty(_skin) ?  "Default" :  _skin); }
            set { _skin = value; }
        }
        public string SmtpPassword { get; set; }
        public int? SmtpPort { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpUserName { get; set; }
        public bool? SmtpUseSsl { get; set; }
        public string SubTitle { get; set; }
        public string TimeZone { get; set; }
        public string Title { get; set; }
        public string TwitterUrl { get; set; }
        public bool UseTwitterCards { get; set; }
        public string WebStatsJavascript { get; set; }

        public BlogConfig()
        {
            this.BlogMarketingInfo = new BlogMarketingInfo();
            this.BlogCommentInfo = new BlogCommentInfo();
        }

        public void LoadConfigFromXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ConfigXml);            
            if (doc.DocumentElement["AllowComments"] != null)
                this.AllowComments = Convert.ToBoolean(doc.DocumentElement["AllowComments"].InnerText);
            if (doc.DocumentElement["AkismetApiKey"] != null)
                this.AkismetApiKey = doc.DocumentElement["AkismetApiKey"].InnerText;
            if (doc.DocumentElement["BlogAbout"] != null)
                this.BlogAbout = doc.DocumentElement["BlogAbout"].InnerText;
            if (doc.DocumentElement["BlogHeaderImage"] != null)
                this.BlogHeaderImage = doc.DocumentElement["BlogHeaderImage"].InnerText;
            if (doc.DocumentElement["BlogHeaderIsImage"] != null)
                this.BlogHeaderIsImage = Convert.ToBoolean(doc.DocumentElement["BlogHeaderIsImage"].InnerText);
            if (doc.DocumentElement["BlogCommentInfo"] != null)
                (this.BlogCommentInfo = new BlogCommentInfo()).LoadFromXml(doc.DocumentElement["BlogCommentInfo"].OuterXml);
            if (doc.DocumentElement["BlogMarketingInfo"] != null)
                (this.BlogMarketingInfo = new BlogMarketingInfo()).LoadFromXml(doc.DocumentElement["BlogMarketingInfo"].OuterXml);
            if (doc.DocumentElement["DefaultLogoUrl"] != null)
                this.DefaultLogoUrl = doc.DocumentElement["DefaultLogoUrl"].InnerText;
            if (doc.DocumentElement["CopyrightText"] != null)
                this.CopyrightText = doc.DocumentElement["CopyrightText"].InnerText;
            if (doc.DocumentElement["DaysUntilCommentsClose"] != null && !string.IsNullOrEmpty(doc.DocumentElement["DaysUntilCommentsClose"].InnerText))
                this.DaysUntilCommentsClose = Convert.ToInt32(doc.DocumentElement["DaysUntilCommentsClose"].InnerText);
            if (doc.DocumentElement["EnableFeedbackAuthorNotifications"] != null)
                this.EnableFeedbackAuthorNotifications = Convert.ToBoolean(doc.DocumentElement["EnableFeedbackAuthorNotifications"].InnerText);
            if (doc.DocumentElement["EnableFeedbackRssFeed"] != null)
                this.EnableFeedbackRssFeed = Convert.ToBoolean(doc.DocumentElement["EnableFeedbackRssFeed"].InnerText);
            if (doc.DocumentElement["FacebookUrl"] != null)
                this.FacebookUrl = doc.DocumentElement["FacebookUrl"].InnerText;
            if (doc.DocumentElement["FeedburnerName"] != null)
                this.FeedburnerName = doc.DocumentElement["FeedburnerName"].InnerText;
            if (doc.DocumentElement["FeedbackCount"] != null)
                this.FeedbackCount = Convert.ToInt32(doc.DocumentElement["FeedbackCount"].InnerText);
            if (doc.DocumentElement["FeedbackRequiresApproval"] != null)
                this.FeedbackRequiresApproval = Convert.ToBoolean(doc.DocumentElement["FeedbackRequiresApproval"].InnerText);
            if (doc.DocumentElement["IsActive"] != null)
                this.IsActive = Convert.ToBoolean(doc.DocumentElement["IsActive"].InnerText);
            if (doc.DocumentElement["GoogleApiKey"] != null)
                this.GoogleApiKey = HttpUtility.HtmlDecode(doc.DocumentElement["GoogleApiKey"].InnerText);
            if (doc.DocumentElement["GooglePlusUrl"] != null)
                this.GooglePlusUrl = doc.DocumentElement["GooglePlusUrl"].InnerText;
            if (doc.DocumentElement["HeaderScript"] != null)
                this.HeaderScript = HttpUtility.HtmlDecode(doc.DocumentElement["HeaderScript"].InnerText);
            if (doc.DocumentElement["Language"] != null)
                this.Language = doc.DocumentElement["Language"].InnerText;
            if (doc.DocumentElement["LogEmailAddress"] != null)
                this.LogEmailAddress = doc.DocumentElement["LogEmailAddress"].InnerText;
            if (doc.DocumentElement["LogFilePath"] != null)
                this.LogFilePath = doc.DocumentElement["LogFilePath"].InnerText;
            if (doc.DocumentElement["LogToDb"] != null)
                this.LogToDb = Convert.ToBoolean(doc.DocumentElement["LogToDb"].InnerText);
            if (doc.DocumentElement["LogToEmail"] != null)
                this.LogToEmail = Convert.ToBoolean(doc.DocumentElement["LogToEmail"].InnerText);
            if (doc.DocumentElement["LogToFile"] != null)
                this.LogToFile = Convert.ToBoolean(doc.DocumentElement["LogToFile"].InnerText);
            if (doc.DocumentElement["NotifyAdminsForFeedback"] != null)
                this.NotifyAdminsForFeedback = Convert.ToBoolean(doc.DocumentElement["NotifyAdminsForFeedback"].InnerText);
            if (doc.DocumentElement["PostCount"] != null)
                this.PostCount = Convert.ToInt32(doc.DocumentElement["PostCount"].InnerText);
            if (doc.DocumentElement["PostsPerPage"] != null)
                this.PostsPerPage = Convert.ToInt32(doc.DocumentElement["PostsPerPage"].InnerText);
            if (doc.DocumentElement["RssShowLimitedEntryInFeed"] != null)
                this.RssShowLimitedEntryInFeed = Convert.ToBoolean(doc.DocumentElement["RssShowLimitedEntryInFeed"].InnerText);            
            if (doc.DocumentElement["RssUrl"] != null)
                this.RssUrl = doc.DocumentElement["RssUrl"].InnerText;
            if (doc.DocumentElement["SecurityQuestionOne"] != null)
                this.SecurityQuestionOne = doc.DocumentElement["SecurityQuestionOne"].InnerText;
            if (doc.DocumentElement["SecurityQuestionTwo"] != null)
                this.SecurityQuestionTwo = doc.DocumentElement["SecurityQuestionTwo"].InnerText;
            if (doc.DocumentElement["SecurityQuestionThree"] != null)
                this.SecurityQuestionThree = doc.DocumentElement["SecurityQuestionThree"].InnerText;
            if (doc.DocumentElement["SecurityQuestionAnswerOne"] != null)
                this.SecurityQuestionAnswerOne = doc.DocumentElement["SecurityQuestionAnswerOne"].InnerText;
            if (doc.DocumentElement["SecurityQuestionAnswerTwo"] != null)
                this.SecurityQuestionAnswerTwo = doc.DocumentElement["SecurityQuestionAnswerTwo"].InnerText;
            if (doc.DocumentElement["SecurityQuestionAnswerThree"] != null)
                this.SecurityQuestionAnswerThree = doc.DocumentElement["SecurityQuestionAnswerThree"].InnerText;
            if (doc.DocumentElement["ShowAuthorsAbout"] != null)
                this.ShowAuthorsAbout = Convert.ToBoolean(doc.DocumentElement["ShowAuthorsAbout"].InnerText);
            if (doc.DocumentElement["ShowBlogAbout"] != null)
                this.ShowBlogAbout = Convert.ToBoolean(doc.DocumentElement["ShowBlogAbout"].InnerText);
            if (doc.DocumentElement["ShowGooglePlusOne"] != null)
                this.ShowGooglePlusOne = Convert.ToBoolean(doc.DocumentElement["ShowGooglePlusOne"].InnerText);
            if (doc.DocumentElement["ShowGravatars"] != null)
                this.ShowGravatars = Convert.ToBoolean(doc.DocumentElement["ShowGravatars"].InnerText);
            if (doc.DocumentElement["ShowNextPreviousAtTop"] != null)
                this.ShowNextPreviousAtTop = Convert.ToBoolean(doc.DocumentElement["ShowNextPreviousAtTop"].InnerText);
            if (doc.DocumentElement["ShowNextPreviousAtBottom"] != null)
                this.ShowNextPreviousAtBottom = Convert.ToBoolean(doc.DocumentElement["ShowNextPreviousAtBottom"].InnerText);
            if (doc.DocumentElement["Skin"] != null)
                this.Skin = doc.DocumentElement["Skin"].InnerText;
            if (doc.DocumentElement["SmtpPassword"] != null)
                this.SmtpPassword = doc.DocumentElement["SmtpPassword"].InnerText;
            if (doc.DocumentElement["SmtpPort"] != null)
                this.SmtpPort = Convert.ToInt32(doc.DocumentElement["SmtpPort"].InnerText);
            if (doc.DocumentElement["SmtpServer"] != null)
                this.SmtpServer = doc.DocumentElement["SmtpServer"].InnerText;
            if (doc.DocumentElement["SmtpUserName"] != null)
                this.SmtpUserName = doc.DocumentElement["SmtpUserName"].InnerText;
            if (doc.DocumentElement["SmtpUseSsl"] != null)
                this.SmtpUseSsl = Convert.ToBoolean(doc.DocumentElement["SmtpUseSsl"].InnerText);
            if (doc.DocumentElement["SubTitle"] != null)
                this.SubTitle = doc.DocumentElement["SubTitle"].InnerText;
            if (doc.DocumentElement["TimeZone"] != null)
                this.TimeZone = doc.DocumentElement["TimeZone"].InnerText;
            if (doc.DocumentElement["Title"] != null)
                this.Title = doc.DocumentElement["Title"].InnerText;
            if (doc.DocumentElement["TwitterUrl"] != null)
                this.TwitterUrl = doc.DocumentElement["TwitterUrl"].InnerText;
            if (doc.DocumentElement["UseTwitterCards"] != null)
                this.UseTwitterCards = Convert.ToBoolean(doc.DocumentElement["UseTwitterCards"].InnerText);
            if (doc.DocumentElement["WebStatsJavascript"] != null)
                this.WebStatsJavascript = HttpUtility.HtmlDecode(doc.DocumentElement["WebStatsJavascript"].InnerText);

        }

        public void BuildXmlFromConfig()
        {           
            XElement blogConfigXml = 
                new XElement("BlogConfig",
                    new XElement("AkismetApiKey", this.AkismetApiKey),
                    new XElement("AllowComments", this.AllowComments),
                    new XElement("BlogAbout", this.BlogAbout),
                    new XElement("BlogHeaderImage", this.BlogHeaderImage),
                    new XElement("BlogHeaderIsImage", this.BlogHeaderIsImage),
                    new XElement("BlogCommentInfo", this.BlogCommentInfo.BuildXmlFromData().Elements()),
                    new XElement("BlogMarketingInfo", this.BlogMarketingInfo.BuildXmlFromData().Elements()),
                    new XElement("CopyrightText", this.CopyrightText),
                    new XElement("DaysUntilCommentsClose", this.DaysUntilCommentsClose),
                    new XElement("DefaultLogoUrl", this.DefaultLogoUrl),
                    new XElement("EnableFeedbackAuthorNotifications", this.EnableFeedbackAuthorNotifications),
                    new XElement("EnableFeedbackRssFeed", this.EnableFeedbackRssFeed),
                    new XElement("FacebookUrl", this.FacebookUrl),
                    new XElement("FeedburnerName", this.FeedburnerName),
                    new XElement("FeedbackCount", this.FeedbackCount),
                    new XElement("FeedbackRequiresApproval", this.FeedbackRequiresApproval),
                    new XElement("IsActive", this.IsActive),
                    new XElement("GoogleApiKey", HttpUtility.HtmlEncode(this.GoogleApiKey)),
                    new XElement("GooglePlusUrl", this.GooglePlusUrl),
                    new XElement("HeaderScript",  HttpUtility.HtmlEncode(this.HeaderScript)),
                    new XElement("Language", this.Language),
                    new XElement("LogEmailAddress", this.LogEmailAddress),
                    new XElement("LogFilePath", this.LogFilePath),
                    new XElement("LogToDb", this.LogToDb),
                    new XElement("LogToEmail", this.LogToEmail),
                    new XElement("LogToFile", this.LogToFile),
                    new XElement("NotifyAdminsForFeedback", this.NotifyAdminsForFeedback),
                    new XElement("PostCount", this.PostCount),
                    new XElement("PostsPerPage", this.PostsPerPage),
                    new XElement("RssShowLimitedEntryInFeed", this.RssShowLimitedEntryInFeed),
                    new XElement("RssUrl", this.RssUrl),
                    new XElement("ShowAuthorsAbout", this.ShowAuthorsAbout),
                    new XElement("ShowBlogAbout", this.ShowBlogAbout),
                    new XElement("ShowGooglePlusOne", this.ShowGooglePlusOne),
                    new XElement("ShowGravatars", this.ShowGravatars),
                    new XElement("ShowNextPreviousAtTop", this.ShowNextPreviousAtTop),
                    new XElement("ShowNextPreviousAtBottom", this.ShowNextPreviousAtBottom),
                    new XElement("Skin", this.Skin),
                    new XElement("SmtpPassword", this.SmtpPassword),
                    new XElement("SmtpPort", this.SmtpPort),
                    new XElement("SmtpServer", this.SmtpServer),
                    new XElement("SmtpUserName", this.SmtpUserName),
                    new XElement("SmtpUseSsl", this.SmtpUseSsl),
                    new XElement("SubTitle", this.SubTitle),
                    new XElement("TimeZone", this.TimeZone),
                    new XElement("Title", this.Title),
                    new XElement("TwitterUrl", this.TwitterUrl),
                    new XElement("UseTwitterCards", this.UseTwitterCards),
                    new XElement("WebStatsJavascript", HttpUtility.HtmlEncode(this.WebStatsJavascript))
                    );

            //this.ConfigXml = blogConfigXml.ToString().Replace("\r\n  ", "").Replace("\r\n", "").Replace(">  <", "><");            
            this.ConfigXml = blogConfigXml.ToString().Replace(">  <", "><");            
        }
    }
}
