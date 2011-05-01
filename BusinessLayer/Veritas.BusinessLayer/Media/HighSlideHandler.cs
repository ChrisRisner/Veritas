using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Veritas.BusinessLayer.Media
{
    public class HighSlideHandler
    {
        public static string UpdateLiveWriterImagesWithHighslide(string content)
        {
            Regex reg = new Regex("<a.*?href=[\"\"'](?<url>.*?)[\"\"'].*?>(?<name>.*?)</a>");
            MatchCollection regMatches = reg.Matches(content);
            foreach (Match mat in regMatches)
            {
                if (mat.Value.Contains("<img"))
                {
                    int indexHref = mat.Value.IndexOf("href=");
                    int indexEndOfHref = mat.Value.Substring(indexHref + 6).IndexOf("\"");
                    int indexSrc = mat.Value.IndexOf("src=");
                    int indexEndOfSrc = mat.Value.Substring(indexSrc + 5).IndexOf("\"");

                    string href = mat.Value.Substring(indexHref + 6, indexEndOfHref);
                    string src = mat.Value.Substring(indexSrc + 5, indexEndOfSrc);

                    indexEndOfHref = href.LastIndexOf("/");
                    indexEndOfSrc = src.LastIndexOf("/");

                    href = href.Substring(0, indexEndOfHref);
                    src = src.Substring(0, indexEndOfSrc);
                    if (href == src)
                    {
                        string newValue = mat.Value;
                        int indexImg = mat.Value.IndexOf("<img");
                        if (mat.Value.IndexOf("class") < indexImg && mat.Value.IndexOf("class") > -1)
                        {
                            int indexClass = mat.Value.IndexOf("class=");
                            int indexEndOfClass = mat.Value.Substring(indexClass + 7).IndexOf("\"");
                            string aClass = mat.Value.Substring(indexClass + 7, indexEndOfClass);
                            newValue = newValue.Replace("class=" + aClass, "class=\"highslide\"");
                        }
                        else
                        {
                            newValue = newValue.Insert(2, " class=\"highslide\"");
                        }

                        if (mat.Value.IndexOf("onclick") < indexImg && mat.Value.IndexOf("onclick") > -1)
                        {
                            int indexOnclick = mat.Value.IndexOf("onclick=");
                            int indexEndOfOnclick = mat.Value.Substring(indexOnclick + 9).IndexOf("\"");
                            string aOnclick = mat.Value.Substring(indexOnclick + 9, indexEndOfOnclick);
                            newValue = newValue.Replace("onclick=" + aOnclick, "onclick=\"return hs.expand(this)\"");
                        }
                        else
                        {
                            newValue = newValue.Insert(2, " onclick=\"return hs.expand(this)\"");
                        }
                        content = content.Replace(mat.Value, newValue);
                    }
                }
            }
            return content;
        }
    }
}
