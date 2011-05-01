using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Xml.Linq;

namespace Veritas.DataLayer.Models
{
    public class BlogMarketingInfo
    {

        public string AdScriptSideBar { get; set; }
        public string AdScriptEntry { get; set; }
        public bool ShowSideBarAds { get; set; }
        public bool ShowEntryAds { get; set; }
        


        public void LoadFromXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            if (doc.DocumentElement["ShowSideBarAds"] != null)
                this.ShowSideBarAds = Convert.ToBoolean(doc.DocumentElement["ShowSideBarAds"].InnerText);
            if (doc.DocumentElement["ShowEntryAds"] != null)
                this.ShowEntryAds = Convert.ToBoolean(doc.DocumentElement["ShowEntryAds"].InnerText);
            if (doc.DocumentElement["AdScriptSideBar"] != null)
                this.AdScriptSideBar = HttpUtility.HtmlDecode(doc.DocumentElement["AdScriptSideBar"].InnerText);
            if (doc.DocumentElement["AdScriptEntry"] != null)
                this.AdScriptEntry = HttpUtility.HtmlDecode(doc.DocumentElement["AdScriptEntry"].InnerText);           
        }

        public XElement BuildXmlFromData()
        {
            XElement blogMarketingInfoXml =
                new XElement("BlogMarketingInfo",
                    new XElement("ShowSideBarAds", this.ShowSideBarAds),
                    new XElement("ShowEntryAds", this.ShowEntryAds),
                    new XElement("AdScriptSideBar", HttpUtility.HtmlEncode(this.AdScriptSideBar)),
                    new XElement("AdScriptEntry", HttpUtility.HtmlEncode(this.AdScriptEntry))
                    );
            return blogMarketingInfoXml;

        }
        
    }
}
