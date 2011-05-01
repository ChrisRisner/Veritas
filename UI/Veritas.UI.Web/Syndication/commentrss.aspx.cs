using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Veritas.BusinessLayer.Caching;
using Veritas.DataLayer;
using Veritas.DataLayer.Models;

namespace Veritas.UI.Web.Syndication
{
    public partial class commentrss : System.Web.UI.Page
    {
        protected string FormatForXML(object input)
        {
            string data = input.ToString(); //cast input to string

            // replace those characters disallowed in XML documents
            data = data.Replace("&", "&amp;");
            data = data.Replace("\"", "&quot;");
            data = data.Replace("'", "&apos;");
            data = data.Replace("<", "&lt;");
            data = data.Replace(">", "&gt;");

            return data;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ProcessRequest(this.Context);

        }
        
        public void ProcessRequest(HttpContext context)
        {
            string sTxt = "";

            sTxt = BuildXmlString(context);
            /*
            if (context.Cache["RSSFeed"] == null)
            {
                sTxt = BuildXmlString();
                context.Cache.Insert("RSSFeed", sTxt);
            }
            else
            {
                sTxt = context.Cache["RSSFeed"].ToString();
            }
            */

            //crdbDataContext con = new crdbDataContext();
            //BlogLog bl = new BlogLog();
            //bl.BlogConfigID = 0;

            //bl.Exception = "Referrer: " + context.Request.UrlReferrer.ToString() + " \n" + sTxt;
            //bl.Date = DateTime.Now;
            //bl.Level = "Rss Feed";
            //bl.Logger = "Application_error";
            //bl.Message = "FUCK";
            //bl.Url = context.Request.Url.ToString();
            //bl.Thread = "what";
            //con.BlogLogs.InsertOnSubmit(bl);
            //con.SubmitChanges();


            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write(sTxt);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string BuildXmlString(HttpContext context)
        {
            var blogConfig = CacheHandler.GetBlogConfig();
            string sTitle = blogConfig.Title;
            string sSiteUrl = "http://" + blogConfig.Host;
            string sDescription = blogConfig.Title;
            string sTTL = "60";

            System.Text.StringBuilder oBuilder = new System.Text.StringBuilder();
            oBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            oBuilder.Append("<rss version=\"2.0\"><channel>");

            oBuilder.Append("<title>");
            oBuilder.Append(sTitle);
            oBuilder.Append("</title>");

            oBuilder.Append("<link>");
            oBuilder.Append(sSiteUrl);
            oBuilder.Append("</link>");

            oBuilder.Append("<description>");
            oBuilder.Append(sDescription);
            oBuilder.Append("</description>");

            oBuilder.Append("<ttl>");
            oBuilder.Append(sTTL);
            oBuilder.Append("</ttl>");


            AppendItems(oBuilder, context);


            oBuilder.Append("</channel></rss>");
            return oBuilder.ToString();
        }

        public void AppendItems(System.Text.StringBuilder oBuilder, HttpContext context)
        {
            //read from database or whatever and retrieve the 10 latest articles     
            //I have a class, ObjClasses.Article and a collection     
            //ObjClasses.ArticleList which does the retrieving      
            var repo = VeritasRepository.GetInstance();
            //TODO:  Change this to pull # of entries from Config filg            
            List<BlogFeedback> feedbacks = repo.GetFeedbackForRSS(CacheHandler.BlogConfigId, 10).ToList();
            foreach (BlogFeedback feedback in feedbacks)
            {
                string sTitle = feedback.Title;
                //string sLink = "http://www.aspcode.net/wharver/showarticle.aspx?id=" + oArtId.ToString() ;         
                //string sLink = "www.chrisrisner.com/blog/archive/" + entry.BlogEntryID.ToString();
                //string sLink = "archive/" + entry.BlogEntryID.ToString();
                //string sLink = "archive/" + entry.EntryName;
                string sLink = GetURLPath(feedback.BlogEntry.EntryName, context);
                //sLink = "blog/archive" + entry.BlogEntryID.ToString();
                string sDescription = feedback.Body;
                string sPubDate = feedback.LastUpdateDate.ToString("R");
                oBuilder.Append("<item>");
                oBuilder.Append("<title><![CDATA[ ");
                oBuilder.Append(sTitle);
                oBuilder.Append(" ]]></title>");
                oBuilder.Append("<link>");
                oBuilder.Append(sLink);
                oBuilder.Append("</link>");
                oBuilder.Append("<guid isPermaLink=\"true\">");
                oBuilder.Append(sLink);
                oBuilder.Append("</guid>");
                oBuilder.Append("<description><![CDATA[ ");
                oBuilder.Append(sDescription);
                oBuilder.Append(" ]]></description>");
                oBuilder.Append("<pubDate>");
                oBuilder.Append(sPubDate);
                oBuilder.Append("</pubDate>");
                oBuilder.Append("<author>");
                oBuilder.Append(feedback.BlogFeedbackAuthor.Email);
                oBuilder.Append(" (");
                oBuilder.Append(feedback.BlogFeedbackAuthor.Name);
                oBuilder.Append(")");
                oBuilder.Append("</author>");
                oBuilder.Append("</item>");
            }
        }

        public string GetURLPath(string entryName, HttpContext context)
        {
            return "http://" + CacheHandler.GetBlogConfig().Host + "/" + entryName;
            //return ("blog/archive/" + entryName);
            if (context.Request.Url.ToString().Contains("blog/archive"))
                return entryName;
            else if (Context.Request.Url.ToString().Contains("blog/"))
                return ("archive/" + entryName);
            else if (Context.Request.Url.ToString().Contains("blogold/"))
                return ("archive/" + entryName);
            else
                return ("blog/archive/" + entryName);
        }

        /*
        public string GetURLPath
        {
            get
            {
                if (Request.Url.ToString().Contains("blog/archive"))
                {
                    return ViewData.Model.entry.EntryName;
                }
                else
                {
                    return "blog/archive/" + ViewData.Model.entry.EntryName;
                }
            }
        }
        */
    }
}
