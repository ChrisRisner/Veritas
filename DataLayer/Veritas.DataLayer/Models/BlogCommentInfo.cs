using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Xml.Linq;

namespace Veritas.DataLayer.Models
{
    public class BlogCommentInfo
    {
        public bool UseDefaultComments { get; set; }
        public bool UseDisqusComments { get; set; }
        public string DisqusAccountName { get; set; }
        public string DisqusCommentScript { get; set; }

        public void LoadFromXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            if (doc.DocumentElement["UseDefaultComments"] != null)
                this.UseDefaultComments = Convert.ToBoolean(doc.DocumentElement["UseDefaultComments"].InnerText);
            if (doc.DocumentElement["UseDisqusComments"] != null)
                this.UseDisqusComments = Convert.ToBoolean(doc.DocumentElement["UseDisqusComments"].InnerText);
            if (doc.DocumentElement["DisqusAccountName"] != null)
                this.DisqusAccountName = HttpUtility.HtmlDecode(doc.DocumentElement["DisqusAccountName"].InnerText);
            if (doc.DocumentElement["DisqusCommentScript"] != null)
                this.DisqusCommentScript = HttpUtility.HtmlDecode(doc.DocumentElement["DisqusCommentScript"].InnerText);
        }

        public XElement BuildXmlFromData()
        {
            XElement blogCommentInfoXml =
                new XElement("BlogCommentInfo",
                    new XElement("UseDefaultComments", this.UseDefaultComments),
                    new XElement("UseDisqusComments", this.UseDisqusComments),
                    new XElement("DisqusAccountName", HttpUtility.HtmlEncode(this.DisqusAccountName)),
                    new XElement("DisqusCommentScript", HttpUtility.HtmlEncode(this.DisqusCommentScript))
                    );
            return blogCommentInfoXml;

        }
    }
}
