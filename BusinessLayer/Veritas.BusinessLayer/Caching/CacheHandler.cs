using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using System.Web;
using Veritas.DataLayer;

namespace Veritas.BusinessLayer.Caching
{
    public static class CacheHandler
    {
        public const string BlogConfigCacheKey = "--BlogConfigCacheKey--";
        public const string BlogMenuItemsCacheKey = "--BlogMenuItemsCacheKey--";
        public const string BlogCategoryTagCacheKey = "--BlogCategoryTagCacheKey--";
        public const string BlogAuthorsCacheKey = "--BlogAuthorsCacheKey--";
        public const string BlogEntryCountCacheKey = "--BlogEntryCountCacheKey--";

        public static BlogConfig GetBlogConfig()
        {
            if (HttpContext.Current == null || HttpContext.Current.Cache == null)
            {
                var repo = VeritasRepository.GetInstance();
                return repo.GetAllBlogConfigs().FirstOrDefault();
            }
            if (HttpContext.Current.Cache[BlogConfigCacheKey] == null)
            {
                var host = HttpContext.Current.Request.Url.Host;
                var repo = VeritasRepository.GetInstance();
                var config = repo.GetBlogConfigByHostname(host);
                config.LoadConfigFromXml();
                HttpContext.Current.Cache[BlogConfigCacheKey] = config;
                return config;
            }
            return (HttpContext.Current.Cache[BlogConfigCacheKey] as BlogConfig);
        }

        public static int BlogConfigId
        {
            get { return CacheHandler.GetBlogConfig().BlogConfigId; }
        }

        internal static BlogMenuItem[] GetBlogMenuItems()
        {
            if (HttpContext.Current == null || HttpContext.Current.Cache == null)
            {
                var repo = VeritasRepository.GetInstance();
                return repo.GetBlogMenuItems(BlogConfigId).ToArray();
            }
            if (HttpContext.Current.Cache[BlogMenuItemsCacheKey] == null)
            {
                var repo = VeritasRepository.GetInstance();

                var menuItems = repo.GetBlogMenuItems(BlogConfigId).ToArray();
                HttpContext.Current.Cache[BlogMenuItemsCacheKey] = menuItems;
                return menuItems;
            }
            return (HttpContext.Current.Cache[BlogMenuItemsCacheKey] as BlogMenuItem[]);
        }

        internal static BlogCategoryTag[] GetBlogCategoryTags()
        {
            if (HttpContext.Current == null || HttpContext.Current.Cache == null)
            {
                var repo = VeritasRepository.GetInstance();
                return repo.GetBlogCategoryTags(BlogConfigId).ToArray();                   
            }
            if (HttpContext.Current.Cache[BlogCategoryTagCacheKey] == null)
            {
                var repo = VeritasRepository.GetInstance();

                var categoryTags = repo.GetBlogCategoryTags(BlogConfigId).ToArray();
                HttpContext.Current.Cache[BlogCategoryTagCacheKey] = categoryTags;
                return categoryTags;
            }
            return (HttpContext.Current.Cache[BlogCategoryTagCacheKey] as BlogCategoryTag[]);
        }

        internal static BlogUser[] GetBlogAuthors()
        {
            if (HttpContext.Current == null || HttpContext.Current.Cache == null)
            {
                var repo = VeritasRepository.GetInstance();
                return repo.GetBlogUsersInRoles(BlogConfigId, "Author");
            }
            if (HttpContext.Current.Cache[BlogAuthorsCacheKey] == null)
            {
                var repo = VeritasRepository.GetInstance();

                var blogUsers = repo.GetBlogUsersInRoles(BlogConfigId, "Author");
                foreach (var user in blogUsers)
                    user.Password = "";
                HttpContext.Current.Cache[BlogAuthorsCacheKey] = blogUsers;                
                return blogUsers;
            }
            return (HttpContext.Current.Cache[BlogAuthorsCacheKey] as BlogUser[]);
        }

        internal static int GetTotalBlogEntryPost()
        {
            if (HttpContext.Current == null || HttpContext.Current.Cache == null)
            {
                var repo = VeritasRepository.GetInstance();
                return repo.GetTotalEntriesByBlogConfigId(BlogConfigId);
            }
            if (HttpContext.Current.Cache[BlogEntryCountCacheKey] == null)
            {
                var repo = VeritasRepository.GetInstance();

                int entryCount = repo.GetTotalEntriesByBlogConfigId(BlogConfigId);
                HttpContext.Current.Cache[BlogEntryCountCacheKey] = entryCount;
                return entryCount;
            }
            return ((int) HttpContext.Current.Cache[BlogEntryCountCacheKey]);
        }

        public static void ResetCache()
        {
            if (HttpContext.Current != null && HttpContext.Current.Cache != null)
            {
                HttpContext.Current.Cache.Remove(BlogConfigCacheKey);
                HttpContext.Current.Cache.Remove(BlogMenuItemsCacheKey);
                HttpContext.Current.Cache.Remove(BlogCategoryTagCacheKey);
                HttpContext.Current.Cache.Remove(BlogAuthorsCacheKey);
                HttpContext.Current.Cache.Remove(BlogEntryCountCacheKey);
            }            
        }
    }
}
