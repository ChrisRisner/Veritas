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
    public partial class rss : System.Web.UI.Page
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

        private List<int> _excludeCategories = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            ProcessRequest(this.Context);

        }

        public void ProcessRequest(HttpContext context)
        {
            //See if we need to exclude any categories
            GetCategoriesToExclude();

            string sTxt = "";

            sTxt = BuildXmlString(context);

            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write(sTxt);
        }

        /// <summary>
        /// Check the query string to see if we should exclude any categories.
        /// </summary>
        private void GetCategoriesToExclude()
        {
            string excludeCategories = HttpContext.Current.Request.QueryString["excludeCats"];
            if (!string.IsNullOrEmpty(excludeCategories))
            {
                _excludeCategories = new List<int>();
                foreach (string categoryId in excludeCategories.Split(' '))
                {
                    _excludeCategories.Add(Convert.ToInt32(categoryId));
                }
            }
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
            var repo = VeritasRepository.GetInstance();
            List<BlogEntry> entries = repo.GetEntriesForRSS(CacheHandler.BlogConfigId, 10, _excludeCategories).ToList();
            foreach (BlogEntry entry in entries)
            {
                string sTitle = entry.Title;
                string sLink = GetURLPath(entry.EntryName, context);
                string sDescription = entry.Text;
                //Check to see if we should show the whole entry or only some of it
                //Read the rest of the article here.
                if (CacheHandler.GetBlogConfig().RssShowLimitedEntryInFeed)
                {
                    if (sDescription.Length > 2000)
                    {
                        sDescription = sDescription.Substring(0, 2000) + "...";
                        sDescription += "<br /><br /><br /><span stlye=\"float: left\">Read the rest of the article <a href=\"" +
                            GetURLPath(entry.EntryName, context) + "\">here.</a></span>";
                    }
                }

                string sPubDate = entry.PublishDate.ToString("R");
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
                oBuilder.Append("</item>");
            }
        }

        public string GetURLPath(string entryName, HttpContext context)
        {
            return "http://" + CacheHandler.GetBlogConfig().Host + "/" + entryName;                      
        }        
    }
}