using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Veritas.DataLayer;
using Veritas.BusinessLayer.Caching;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Session
{
    public static class SessionHandler
    {
        static VeritasRepository repo = VeritasRepository.GetInstance();

        public static int CurrentUserId
        {
            get
            {
                if (HttpContext.Current.Session["CurrentUserId"] != null)
                    return (Convert.ToInt32(HttpContext.Current.Session["CurrentUserId"]));
                else if (HttpContext.Current.Request.IsAuthenticated)
                {
                    int userId = repo.GetBlogUserByUserName(CacheHandler.BlogConfigId, HttpContext.Current.User.Identity.Name).BlogUserId;
                    HttpContext.Current.Session["CurrentUserId"] = userId;
                    return userId;
                }
                return 0;
            }
            set
            {
                HttpContext.Current.Session["CurrentUserId"] = value;
            }
        }

        public static BlogUser CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session["CurrentUser"] != null)
                    return (HttpContext.Current.Session["CurrentUser"] as BlogUser);
                else if (HttpContext.Current.Request.IsAuthenticated)
                {
                    BlogUser user = repo.GetBlogUserByUserName(CacheHandler.BlogConfigId,
                            HttpContext.Current.User.Identity.Name);
                    HttpContext.Current.Session["CurrentUser"] = user;
                    return user;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session["CurrentUserId"] = value;
            }
        }

    }
}
