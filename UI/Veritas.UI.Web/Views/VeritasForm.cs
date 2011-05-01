using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Veritas.DataLayer;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Veritas.BusinessLayer;
using System.Web.Routing;
using Veritas.BusinessLayer.Caching;
using System.Text;
using Veritas.DataLayer.Models;

namespace Veritas.UI.Web.Views
{
    public class VeritasForm
    {
        private VeritasRepository repo = VeritasRepository.GetInstance();
        internal const string CACHE_KEY = "_VeritasForm_Cache_Key";
        internal HtmlHelper Hhelper { get; set; }
        internal UrlHelper Uhelper { get; set; }

        /// <summary>
        /// Our static accessor so we can easily access this from the views.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static VeritasForm GetInstance(HtmlHelper htmlHelper, UrlHelper urlHelper)
        {
            if (htmlHelper.ViewContext.HttpContext == null)
                return new VeritasForm(htmlHelper, urlHelper);

            if (htmlHelper.ViewContext.HttpContext.Items.Contains(CACHE_KEY))
                return (VeritasForm)htmlHelper.ViewContext.HttpContext.Items[CACHE_KEY];
            VeritasForm form = new VeritasForm(htmlHelper, urlHelper);
            htmlHelper.ViewContext.HttpContext.Items.Add(CACHE_KEY, form);
            return form;
        }
       
        private VeritasForm(HtmlHelper htmlHelper, UrlHelper urlHelper)
        {
            Hhelper = htmlHelper;
            Uhelper = urlHelper;
        }

        public string ActionLink(string linkText, string actionName, string controller)
        {            
            return Hhelper.ActionLink(linkText, actionName, controller, null, new { Title = linkText }).ToString().AutoRemoveAlias();
        }

        public string ActionLink(string linkText, string actionName, object routeValues)
        {
            return Hhelper.ActionLink(linkText, actionName, routeValues, new { Title = linkText }).ToString().AutoRemoveAlias();
        }

        public string ActionLink(string linkText, string actionName)
        {
            return Hhelper.ActionLink(linkText, actionName, null, new { Title = linkText }).ToString().AutoRemoveAlias();
        }

        public string ActionLink(string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            return Hhelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes).ToString().AutoRemoveAlias();
        }

        public string Action(string actionName)
        {
            return Uhelper.Action(actionName).AutoRemoveAlias();           
        }

        public string Action(string actionName, object routeValues)
        {
            return Uhelper.Action(actionName, routeValues).AutoRemoveAlias();
        }

        public string Action(string actionName, string controllerName)
        {
            return Uhelper.Action(actionName, controllerName).AutoRemoveAlias();
        }

        public string Action(string actionName, RouteValueDictionary routeValues)
        {
            return Uhelper.Action(actionName, routeValues);
        }

        public string Action(string actionName, string controllerName, object routeValues)
        {
            return Uhelper.Action(actionName, controllerName, routeValues).AutoRemoveAlias();
        }

        public string Action(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return Uhelper.Action(actionName, controllerName, routeValues).AutoRemoveAlias();
        }

        public string Action(string actionName, string controllerName, object routeValues, string protocol)
        {
            return Uhelper.Action(actionName, controllerName, routeValues, protocol).AutoRemoveAlias();
        }

        public string Action(string actionName, string controllerName, RouteValueDictionary routeValues, string protocol, string hostName)
        {
            return Uhelper.Action(actionName, controllerName, routeValues, protocol, hostName).AutoRemoveAlias();
        }
 
        public string Content(string contentPath)
        {
            return Uhelper.Content(contentPath);
        }

        public string GetUserNameById(int blogConfigId, int? userId)
        {
            if (userId.HasValue && userId.Value > 0)
                return (repo.GetBlogUserByUserId(blogConfigId, userId.Value).Username);
            return "No username";
        }

        /// <summary>
        /// Returns a css class name for tag category links.
        /// </summary>
        /// <param name="category">Number of entries that are associated with the Category</param>
        /// <param name="articles">Total number of articles</param>
        /// <returns></returns>
        public string GetTagClass(int category, int articles)
        {
            if (articles < 1)
                return "tag1";
            var result = (category * 100) / articles;
            if (result <= 1)
                return "tag1";
            if (result <= 4)
                return "tag2";
            if (result <= 8)
                return "tag3";
            if (result <= 12)
                return "tag4";
            if (result <= 18)
                return "tag5";
            if (result <= 30)
                return "tag5";
            return result <= 50 ? "tag6" : "tag7";
        }

        public string GetCategoryLinkLineForEntry(BlogCategory[] categories)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var category in categories)
            {
                sb.Append(this.ActionLink(category.Title, "category", "home", new { id = category.Title }, null));
                //sb.Append("<a href=\"http://" + (
                //    CacheAccessor.GetBlogConfig().Host) + "/category/" + category.Title + "\" title=\"" + category.Title + "\">" + category.Title + "</a>");
                sb.Append(", ");
            }
            return (sb.ToString().Trim(' ').Trim(','));
        }

        public string GetGravatarLink(string email)
        {
            if (CacheHandler.GetBlogConfig().ShowGravatars)
            {
                return (GravatarHandler.GetGravatarURL(email));
            }
            return null;
        }

        public string GetLinkForFeedbackAuthor(BlogFeedback feedback)
        {
            if (string.IsNullOrEmpty(feedback.BlogFeedbackAuthor.Url) || feedback.BlogFeedbackAuthor.Url.Equals("http://"))
            {
                return (feedback.BlogFeedbackAuthor.Name);
            }
            else
            {
                //return ("<a href=\"" + feedback.Url + "\">" + feedback.Author + "</a>");
                return ("<a href=\"" + feedback.BlogFeedbackAuthor.Url + "\">" + feedback.BlogFeedbackAuthor.Name + "</a>");
            }
        }

        public string GetUsernameFromCookie()
        {
            return "test";
        }

        public string GetEmailAddressFromCookie()
        {
            return "test@test.com";
        }

        public string GetWebsiteFromCookie()
        {
            return "http://test.com";
        }

        public bool GetNotifyMeFromCookie()
        {
            return false;
        }

    }
}
