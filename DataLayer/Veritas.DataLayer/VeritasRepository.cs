using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Veritas.DataLayer.Models;
using System.Data.Common;

namespace Veritas.DataLayer
{
    public class VeritasRepository
    {
        internal const string CACHE_KEY = "_VeritasRepository_Cache_Key";
        private VeritasBlogDBV3Entities db = new VeritasBlogDBV3Entities();
        public DbTransaction Transaction { get; set; }

        private VeritasRepository() { }

        /// <summary>
        /// Static method to get an instance of our Veritas Repostiory.  
        /// Checks to see if we have a context in case we're using 
        /// this in a unit test.
        /// </summary>
        /// <returns></returns>
        public static VeritasRepository GetInstance()
        {
            if (HttpContext.Current == null)
                return new VeritasRepository();

            if (HttpContext.Current.Items.Contains(CACHE_KEY))
                return (VeritasRepository)HttpContext.Current.Items[CACHE_KEY];

            VeritasRepository repo = new VeritasRepository();
            HttpContext.Current.Items[CACHE_KEY] = repo;
            return repo;
        }

        /// <summary>
        /// Forces us to get a new instace for testing purposes
        /// </summary>
        /// <returns></returns>
        public static VeritasRepository ForceNewInstance()
        {
            VeritasRepository repo = new VeritasRepository();
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(CACHE_KEY))
                    HttpContext.Current.Items[CACHE_KEY] = repo;
                else
                    HttpContext.Current.Items.Add(CACHE_KEY, repo);
            }
            return repo;
        }
        
        /// <summary>
        /// Will create a new transation.  
        /// </summary>
        public void StartTransaction()
        {
            db.Connection.Open();
            DbTransaction trans = db.Connection.BeginTransaction();
            this.Transaction = trans;
        }

        /// <summary>
        /// Rolls back a transation. 
        /// </summary>
        public void RollbackTransaction()
        {
            this.Transaction.Rollback();
        }

        /// <summary>
        /// Saves all DB changes and then accepts the changes.
        /// </summary>
        public void Save()
        {
            db.CommandTimeout = 120;
            db.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);
        }

        #region Blacklist

        public Blacklist GetBlacklistByEmailAddress(int blogConfigId, string email)
        {
            return db.Blacklists.Where(p => p.BlogConfigId == blogConfigId).Where(p => p.EmailAddress == email).FirstOrDefault();
        }

        public IEnumerable<Blacklist> GetBlacklistsForEmailAddresses(int blogConfigId, string[] emailAddresses)
        {
            return db.Blacklists.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => emailAddresses.Contains(p.EmailAddress));
        }

        public void Add(Blacklist blacklist)
        {
            db.Blacklists.AddObject(blacklist);
        }

        public void Delete(Blacklist blacklist)
        {
            db.Blacklists.DeleteObject(blacklist);
        }

        #endregion        

        #region BlogCategory

        public IEnumerable<BlogCategory> GetBlogCategories(int blogConfigId)
        {
            return db.BlogCategories.Where(p => p.BlogConfigId == blogConfigId);
        }

        public BlogCategory GetBlogCategoryByCategoryId(int blogConfigId, int blogCategoryId)
        {
            return db.BlogCategories.Where(prop => prop.BlogConfigId == blogConfigId)
                .Where(p => p.BlogCategoryId == blogCategoryId).SingleOrDefault();
        }

        public bool DoesCategoryExist(int blogConfigId, string categoryTitle)
        {
            BlogCategory Category = db.BlogCategories.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.Title == categoryTitle).SingleOrDefault();
            return (Category != null);
        }

        public void Add(BlogCategory blogCategory)
        {
            db.BlogCategories.AddObject(blogCategory);
        }

        public void Delete(BlogCategory blogCategory)
        {
            db.BlogCategories.DeleteObject(blogCategory);
        }

        #endregion

        #region BlogConfig

        public BlogConfig GetBlogConfigByHostname(string hostname)
        {            
            return db.BlogConfigs.Where(p => p.Host == hostname).SingleOrDefault();
        }

        public BlogConfig GetBlogConfigByBlogConfigId(int blogConfigId)
        {
            return db.BlogConfigs.Where(p => p.BlogConfigId == blogConfigId).SingleOrDefault();
        }

        public IEnumerable<BlogConfig> GetAllBlogConfigs()
        {
            return db.BlogConfigs;
        }

        public void Add(BlogConfig blogConfig)
        {
            db.BlogConfigs.AddObject(blogConfig);
        }

        public void Delete(BlogConfig blogConfig)
        {
            db.BlogConfigs.DeleteObject(blogConfig);
        }

        #endregion

        #region BlogEntry

        public IEnumerable<BlogEntry> GetEntriesFromStartPoint(int blogConfigId, int numberPosts, int startPoint)
        {
            return db.BlogEntries.Where(p => p.BlogConfigId == blogConfigId)
                .OrderByDescending(p => p.PublishDate).Skip(startPoint).Take(numberPosts);
        }

        public IEnumerable<BlogEntry> GetEntriesForCategoryFromStartPoint(int blogConfigId, int numberPosts, int startPoint, string categoryTitle)
        {            
            var v = from entry in db.BlogEntries
                    join entryCategory in db.BlogEntryCategories on entry.BlogEntryId equals entryCategory.BlogEntryId
                    join category in db.BlogCategories on entryCategory.BlogCategoryId equals category.BlogCategoryId
                    where entry.BlogConfigId == blogConfigId && category.Title == categoryTitle
                    select entry;
            return v.OrderByDescending(p => p.PublishDate).Skip(startPoint).Take(numberPosts);
        }

        public IEnumerable<BlogEntry> GetRecentEntries(int blogConfigId, int numberPosts)
        {
            return db.BlogEntries.Where(p => p.BlogConfigId == blogConfigId)
                .OrderByDescending(p => p.PublishDate).Take(numberPosts);
        }

        public IEnumerable<BlogEntry> GetRecentEntries(int blogConfigId, int numberPosts, string categoryTitle)
        {            
            var v = from entry in db.BlogEntries
                    join entryCategory in db.BlogEntryCategories on entry.BlogEntryId equals entryCategory.BlogEntryId
                    join category in db.BlogCategories on entryCategory.BlogCategoryId equals category.BlogCategoryId
                    where entry.BlogConfigId == blogConfigId &&
                        category.Title == categoryTitle
                    select entry;
            return v.OrderByDescending(p => p.PublishDate).Take(numberPosts);
        }

        public IEnumerable<BlogEntry> GetEntriesForRSS(int blogConfigId, int numberPosts, List<int> excludeCategories)
        {
            var v = from entries in db.BlogEntries
                    where entries.BlogConfigId == blogConfigId
                        && entries.PostType == (int)PostType.Published
                    select entries;
            if (excludeCategories != null)
            {
                foreach (int categoryId in excludeCategories)
                {
                    v = v.Where(p => !(p.BlogEntryCategories.Select(q => q.BlogCategoryId).Contains(categoryId)));
                }
            }
            return (v.OrderByDescending(p => p.PublishDate).Take(numberPosts));
        }

        public int GetTotalEntriesByBlogConfigId(int blogConfigId)
        {
            return db.BlogEntries.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.PostType == (int)PostType.Published).Count();
        }

        public int GetTotalEntriesByBlogConfigIdAndCategory(int blogConfigId, string categoryTitle)
        {
            var v = from entry in db.BlogEntries
                    join entryCategory in db.BlogEntryCategories on entry.BlogEntryId equals entryCategory.BlogEntryId
                    join category in db.BlogCategories on entryCategory.BlogCategoryId equals category.BlogCategoryId
                    where entry.BlogConfigId == blogConfigId &&
                        category.Title == categoryTitle &&
                        entry.PostType == (int)PostType.Published
                    select entry;
            return v.Count();
        }

        public BlogEntry GetEntryByEntryNameAndBlogConfigId(int blogConfigId, string entryName)
        {
            return db.BlogEntries.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.EntryName == entryName).SingleOrDefault();
        }

        public BlogEntry GetEntryById(long blogEntryId)
        {
            return db.BlogEntries.Where(p => p.BlogEntryId == blogEntryId).SingleOrDefault();
        }

        public BlogEntry GetEntryByEntryIDBlogConfigId(int blogConfigId, Int64 entryId)
        {
            return db.BlogEntries.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.BlogEntryId == entryId).SingleOrDefault();
        }

        public bool DoesEntryTitleExist(int blogConfigId, string blogTitle, string blogEntryName)
        {
            BlogEntry entry = db.BlogEntries.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.Title == blogTitle).SingleOrDefault();
            if (entry != null)
                return true;
            entry = db.BlogEntries.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.EntryName == blogEntryName).SingleOrDefault();
            if (entry != null)
                return true;
            return false;
        }

        public IEnumerable<BlogEntry> GetBlogEntries(int blogConfigId)
        {
            return db.BlogEntries.Where(p => p.BlogConfigId == blogConfigId);
        }

        public void Add(BlogEntry blogEntry)
        {
            db.BlogEntries.AddObject(blogEntry);
        }

        public void Delete(BlogEntry blogEntry)
        {
            db.BlogEntries.DeleteObject(blogEntry);
        }

        public Dictionary<long?, String> GetBlogEntryTitlesAndIds(int blogConfigId)
        {
            var entries = db.BlogEntries.Where(p => p.BlogConfigId == blogConfigId)
                .ToDictionary(p => p.BlogEntryId as long?, p => p.Title);
            return entries;
        }

        #endregion

        #region BlogEntryCategory

        public IEnumerable<BlogEntryCategory> GetBlogEntryCategoriesByEntryId(long blogEntryId)
        {
            return db.BlogEntryCategories.Where(p => p.BlogEntryId == blogEntryId);
        }

        public IEnumerable<BlogEntryCategory> GetBlogEntryCategoriesByCategoryId(int blogCategoryId)
        {
            return db.BlogEntryCategories.Where(p => p.BlogCategoryId == blogCategoryId);
        }

        public void SaveEntryCategoryAssociation(int blogConfigId, long blogEntryId, string[] categoryNames)
        {
            List<BlogEntryCategory> assocs = db.BlogEntryCategories.Where(p => p.BlogEntryId == blogEntryId).ToList();
            foreach (var item in assocs)
                db.BlogEntryCategories.DeleteObject(item);
            this.Save();

            assocs = new List<BlogEntryCategory>();
            foreach (string categoryName in categoryNames)
            {
                BlogCategory category = db.BlogCategories.Where(p => p.BlogConfigId == blogConfigId)
                    .Where(p => p.Title == categoryName).SingleOrDefault();
                if (category != null)
                {
                    BlogEntryCategory assoc = new BlogEntryCategory();
                    assoc.BlogEntryId = blogEntryId;
                    assoc.BlogCategoryId = category.BlogCategoryId;
                    assoc.CreateDate = DateTime.Now;
                    assocs.Add(assoc);
                }
            }            
            foreach (var item in assocs)
                db.BlogEntryCategories.AddObject(item);
            this.Save();
        }

        public IEnumerable<BlogCategoryTag> GetBlogCategoryTags(int blogConfigId)
        {
            int totalArticles = GetTotalEntriesByBlogConfigId(blogConfigId);

            return (from category in db.BlogCategories
                    where category.BlogConfigId == blogConfigId
                    select new BlogCategoryTag
                    {
                        BlogCategoryId = category.BlogCategoryId,
                        CategoryTitle = category.Title,
                        CategoryUseCount = category.BlogEntryCategories.Count(),
                        TotalArticles = totalArticles
                    });
        }

        public void Add(BlogEntryCategory blogEntryCategory)
        {
            db.BlogEntryCategories.AddObject(blogEntryCategory);
        }

        public void Delete(BlogEntryCategory blogEntryCategory)
        {
            db.BlogEntryCategories.DeleteObject(blogEntryCategory);
        }

        #endregion

        #region BlogEntryViewCount

        public IEnumerable<BlogEntryViewCount> GetBlogEntryViewCount(int blogConfigId)
        {
            return db.BlogEntryViewCounts.Where(p => p.BlogConfigId == blogConfigId);
        }

        public BlogEntryViewCount GetBlogEntryViewCountByEntryId(long blogEntryId)
        {
            return db.BlogEntryViewCounts.Where(p => p.BlogEntryId == blogEntryId).FirstOrDefault();
        }

        public void UpdateEntryViewCountForWeb(int blogConfigId, long blogEntryId)
        {
            BlogEntryViewCount evc = db.BlogEntryViewCounts.SingleOrDefault(p => p.BlogEntryId == blogEntryId);
            if (evc != null)
            {
                evc.WebCount++;
                evc.WebLastUpdated = DateTime.Now;
            }
            else
            {
                evc = new BlogEntryViewCount();
                evc.BlogEntryId = blogEntryId;
                evc.BlogConfigId = blogConfigId;
                evc.WebCount = 1;
                evc.WebLastUpdated = DateTime.Now;
                this.Add(evc);                
            }
            this.Save();
        }

        public void Add(BlogEntryViewCount blogEntryViewCount)
        {
            db.BlogEntryViewCounts.AddObject(blogEntryViewCount);
        }

        public void Delete(BlogEntryViewCount blogEntryViewCount)
        {
            db.BlogEntryViewCounts.DeleteObject(blogEntryViewCount);
        }

        #endregion

        #region BlogFeedback

        public IEnumerable<BlogFeedback> GetBlogFeedbackByEntryId(long blogEntryId)
        {
            return db.BlogFeedbacks.Where(p => p.BlogEntryId == blogEntryId);
        }

        public BlogFeedback GetBlogFeedbackByFeedbackId(int blogConfigId, long blogFeedbackId)
        {
            return db.BlogFeedbacks.SingleOrDefault(p => p.BlogFeedbackId == blogFeedbackId);            
        }

        public IEnumerable<BlogFeedback> GetBlogFeedbacksByBlogConfigId(int blogConfigId)
        {
            return db.BlogFeedbacks.Where(p => p.BlogConfigId == blogConfigId);
        }

        public IQueryable<BlogFeedback> GetFeedbackForRSS(int blogConfigId, int numberFeedback)
        {
            return db.BlogFeedbacks.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.Status == (int)FeedbackStatus.Approved)
                .OrderByDescending(p => p.LastUpdateDate).Take(numberFeedback);
        }

        public BlogFeedback SaveFeedback(string entryName, string name, string email, string webSite, string message,
            string requestUserHostAddress, bool isUserLoggedIn, string requestUserAgent, int blogConfigId, bool notifyAuthorOnFeedback)
        {
            throw new Exception("Move this to the business layer.");
            BlogEntry entry = db.BlogEntries.Where(p => p.BlogConfigId == blogConfigId).Where(p => p.EntryName == entryName).SingleOrDefault();
            if (entry != null)
            {
                entry.FeedbackCount++;
                BlogFeedbackAuthor blogFeedbackAuthor = db.BlogFeedbackAuthors.Where(p => p.Name == name)
                    .Where(p => p.Email == email).Where(p => p.Url == webSite).SingleOrDefault();
                if (blogFeedbackAuthor == null)
                {
                    blogFeedbackAuthor = new BlogFeedbackAuthor();
                    blogFeedbackAuthor.Url = webSite;
                    blogFeedbackAuthor.Name = name;
                    blogFeedbackAuthor.LastUpdateDate = DateTime.Now;
                    blogFeedbackAuthor.Email = email;
                    blogFeedbackAuthor.CreateDate = DateTime.Now;
                    blogFeedbackAuthor.FeedbackTotal = 1;
                    blogFeedbackAuthor.BlogConfigId = entry.BlogConfigId;
                    db.BlogFeedbackAuthors.AddObject(blogFeedbackAuthor);
                }
                else
                {
                    blogFeedbackAuthor.FeedbackTotal++;
                    blogFeedbackAuthor.LastUpdateDate = DateTime.Now;
                }
                this.Save();
                BlogConfig config = this.GetBlogConfigByBlogConfigId(blogConfigId);
                BlogFeedback blogFeedback = new BlogFeedback();
                //blogFeedback. = name;
                blogFeedback.BlogFeedbackAuthorId = blogFeedbackAuthor.BlogFeedbackAuthorId;
                blogFeedback.BlogConfigId = entry.BlogConfigId;
                blogFeedback.BlogEntryId = entry.BlogEntryId;
                blogFeedback.Body = message;
                blogFeedback.CreateDate = DateTime.Now;
                
                blogFeedback.FeedbackType = (int)FeedbackStatus.PendingApproval;
                blogFeedback.IpAddress = requestUserHostAddress;
                blogFeedback.IsBlogAuthor = isUserLoggedIn;
                blogFeedback.LastUpdateDate = DateTime.Now;
                blogFeedback.NotifyAuthorOnFeedback = notifyAuthorOnFeedback;
                //if (config.CommentsRequireApproval)
                if (false)
                {
                    blogFeedback.Status = (int)FeedbackStatus.PendingApproval;
                }
                else
                {
                    blogFeedback.Status = (int)FeedbackStatus.Approved;
                    //config++;
                }
                blogFeedback.Title = "re: " + entry.Title;
                //blogFeedback.ur = webSite;
                blogFeedback.UserAgent = requestUserAgent;
                db.BlogFeedbacks.AddObject(blogFeedback);

                this.Save();
                return blogFeedback;
            }
            else
            {
                throw new Exception("Couldn't find entry name to save comment.  EntryName = " + entryName);
            }
        }

        public void Add(BlogFeedback blogFeedback)
        {
            db.BlogFeedbacks.AddObject(blogFeedback);
        }

        public void Delete(BlogFeedback blogFeedback)
        {
            db.BlogFeedbacks.DeleteObject(blogFeedback);
        }

        #endregion

        #region BlogFeedbackAuthor

        public BlogFeedbackAuthor GetBlogFeedbackAuthorById(int blogConfigId, long blogFeedbackAuthorId)
        {
            return db.BlogFeedbackAuthors
                .Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.BlogFeedbackAuthorId == blogFeedbackAuthorId).SingleOrDefault();
        }

        public IEnumerable<BlogFeedbackAuthor> GetBlogFeedbackAuthors(int blogConfigId)
        {
            return db.BlogFeedbackAuthors.Where(p => p.BlogConfigId == blogConfigId);
        }

        public BlogFeedbackAuthor GetBlogFeedbackAuthorByNameEmailWebsite(int blogConfigId, 
                                            string name, string email, string webSite)
        {
            BlogFeedbackAuthor blogFeedbackAuthor = db.BlogFeedbackAuthors.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.Name == name).Where(p => p.Email == email).Where(p => p.Url == webSite).SingleOrDefault();
            return blogFeedbackAuthor;
        }

        public void Add(BlogFeedbackAuthor blogFeedbackAuthor)
        {
            db.BlogFeedbackAuthors.AddObject(blogFeedbackAuthor);
        }

        public void Delete(BlogFeedbackAuthor blogFeedbackAuthor)
        {
            db.BlogFeedbackAuthors.DeleteObject(blogFeedbackAuthor);
        }

        #endregion

        #region BlogLog

        public IEnumerable<BlogLog> GetBlogLogs(int blogConfigId)
        {
            return db.BlogLogs.Where(p => p.BlogConfigId == blogConfigId);
        }   

        public void Add(BlogLog blogLog)
        {
            db.BlogLogs.AddObject(blogLog);
        }

        public void Delete(BlogLog blogLog)
        {
            db.BlogLogs.DeleteObject(blogLog);
        }

        #endregion

        #region BlogMedia

        public IEnumerable<BlogMedia> GetBlogMedias(int blogConfigId)
        {
            return db.BlogMedias.Where(p => p.BlogConfigId == blogConfigId);
        }

        public void Add(BlogMedia blogMedia)
        {
            db.BlogMedias.AddObject(blogMedia);
        }

        public void Delete(BlogMedia blogMedia)
        {
            db.BlogMedias.DeleteObject(blogMedia);
        }

        #endregion

        #region BlogMenuItem

        public IEnumerable<BlogMenuItem> GetBlogMenuItems(int blogConfigId)
        {
            return db.BlogMenuItems.Where(p => p.BlogConfigId == blogConfigId);
        }

        public void Add(BlogMenuItem blogMenuItem)
        {
            db.BlogMenuItems.AddObject(blogMenuItem);
        }

        public void Delete(BlogMenuItem blogMenuItem)
        {
            db.BlogMenuItems.DeleteObject(blogMenuItem);
        }        

        #endregion

        #region BlogPage

        public IEnumerable<BlogPage> GetBlogPages(int blogConfigId)
        {
            return db.BlogPages.Where(p => p.BlogConfigId == blogConfigId);
        }

        public BlogPage GetBlogPageByEncodedTitle(int blogConfigId, string encodedTitle)
        {
            return db.BlogPages.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.EncodedTitle == encodedTitle).SingleOrDefault();
        }

        public BlogPage GetBlogPageByPageId(int blogConfigId, int blogPageId)
        {
            return db.BlogPages
                .Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.BlogPageId == blogPageId).SingleOrDefault();
        }

        public void Add(BlogPage blogPage)
        {
            db.BlogPages.AddObject(blogPage);
        }

        public void Delete(BlogPage blogPage)
        {
            db.BlogPages.DeleteObject(blogPage);
        }

        #endregion

        #region BlogRole

        public IEnumerable<BlogRole> GetBlogRoles(int blogConfigId)
        {
            return db.BlogRoles.Where(p => p.BlogConfigId == blogConfigId);
        }

        public BlogRole GetBlogRoleByRoleId(int blogConfigId, int blogRoleId)
        {
            return db.BlogRoles.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.BlogRoleId == blogRoleId).SingleOrDefault();
        }

        public BlogRole[] GetRolesForUser(int blogConfigId, string username)
        {
            var v = from users in db.BlogUsers
                    join userRoles in db.BlogUserRoles on users.BlogUserId equals userRoles.BlogUserId
                    join roles in db.BlogRoles on userRoles.BlogRoleId equals roles.BlogRoleId
                    where users.Username == username &&
                    users.BlogConfigId == blogConfigId
                    select roles;
            return v.ToArray();
        }

        public void Add(BlogRole blogRole)
        {
            db.BlogRoles.AddObject(blogRole);
        }

        public void Delete(BlogRole blogRole)
        {
            db.BlogRoles.DeleteObject(blogRole);
        }

        #endregion

        #region BlogUser

        public IEnumerable<BlogUser> GetBlogUsers(int blogConfigId)
        {
            return db.BlogUsers.Where(p => p.BlogConfigId == blogConfigId);
        }

        public BlogUser GetBlogUserByUserName(int blogConfigId, string userName)
        {
            return db.BlogUsers.Where(p => p.BlogConfigId == blogConfigId).Where(p => p.Username == userName).SingleOrDefault();
        }

        public BlogUser GetBlogUserByEmail(int blogConfigId, string email)
        {
            return db.BlogUsers.Where(p => p.BlogConfigId == blogConfigId).Where(p => p.EmailAddress == email).SingleOrDefault();
        }  

        public BlogUser GetBlogUserByUserId(int blogConfigId, int blogUserId)
        {
            return db.BlogUsers.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.BlogUserId == blogUserId).SingleOrDefault();
        }

        public bool ChangePassword(int blogConfigId, string username, string oldPassword, string newPassword)
        {
            BlogUser user = db.BlogUsers.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.Username == username).SingleOrDefault();
            if (user != null)
            {
                if (user.Password == oldPassword)
                {
                    user.Password = newPassword;
                    this.Save();
                    return true;
                }
            }
            return false;
        }

        public bool ValidateUser(int blogConfigId, string username, string password)
        {
            BlogUser user = db.BlogUsers.Where(p => p.BlogConfigId == blogConfigId)
                .Where(p => p.Username == username)
                .Where(p => p.Password == password).SingleOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public bool IsUserAuthor(int blogConfigId, string username, string password)
        {
            var v = from user in db.BlogUsers
                    join userRole in db.BlogUserRoles on user.BlogUserId equals userRole.BlogUserId
                    join role in db.BlogRoles on userRole.BlogRoleId equals role.BlogRoleId
                    where (user.Username == username) &&
                    (user.Password == password) &&
                    user.BlogConfigId == blogConfigId &&
                    role.RoleName == "Author"
                    select user;
            if (v.Count() > 0)
                return true;
            return false;
        }

        public BlogUser[] GetBlogUsersInRoles(int blogConfigId, string roleName)
        {
            var v = from roles in db.BlogRoles
                    join userRoles in db.BlogUserRoles on roles.BlogRoleId equals userRoles.BlogRoleId
                    join users in db.BlogUsers on userRoles.BlogUserId equals users.BlogUserId
                    where roles.RoleName == roleName &&
                    users.BlogConfigId == blogConfigId
                    select users;
            return v.ToArray();
        }

        public void Add(BlogUser blogUser)
        {
            db.BlogUsers.AddObject(blogUser);
        }

        public void Delete(BlogUser blogUser)
        {
            db.BlogUsers.DeleteObject(blogUser);
        }        

        #endregion

        #region BlogUserRole

        public IEnumerable<BlogUserRole> GetBlogUserRolesByUserId(int blogUserId)
        {
            return db.BlogUserRoles.Where(p => p.BlogUserId == blogUserId);
        }

        public IEnumerable<BlogUserRole> GetBlogUserRolesByRoleId(int blogRoleId)
        {
            return db.BlogUserRoles.Where(p => p.BlogRoleId == blogRoleId);
        }

        public void Add(BlogUserRole blogUserRole)
        {
            db.BlogUserRoles.AddObject(blogUserRole);
        }

        public void Delete(BlogUserRole blogUserRole)
        {
            db.BlogUserRoles.DeleteObject(blogUserRole);
        }

        #endregion

        #region Membership Methods

        



        #endregion

        #region Forum

        //#region ForumFolder

        //public IEnumerable<ForumFolder> GetForumFoldersByBlogId(int blogId)
        //{
        //    return db.ForumFolders.Where(p => p.BlogConfigId == blogId);
        //}

        //public ForumFolder GetForumFolderById(int folderId)
        //{
        //    return db.ForumFolders.Where(p => p.ForumFolderId == folderId).SingleOrDefault();
        //}

        //public void Add(ForumFolder folder)
        //{
        //    db.ForumFolders.InsertOnSubmit(folder);
        //}

        //#endregion

        //#region ForumTopic

        //public IEnumerable<ForumTopic> GetForumTopicsByBlogId(int blogId)
        //{
        //    return db.ForumTopics.Where(p => p.BlogConfigId == blogId);
        //}

        //public IEnumerable<ForumTopic> GetForumTopicsByFolderId(int folderId)
        //{
        //    return db.ForumTopics.Where(p => p.ForumFolderId == folderId);
        //}

        //public ForumTopic GetForumTopicByTopicId(long topicId)
        //{
        //    return db.ForumTopics.Where(p => p.ForumTopicId == topicId).SingleOrDefault();
        //}

        //public void Add(ForumTopic topic)
        //{
        //    db.ForumTopics.InsertOnSubmit(topic);
        //}

        //#endregion

        //#region ForumPost

        //public IEnumerable<ForumPost> GetForumPostsByTopicId(long topicId)
        //{
        //    return db.ForumPosts.Where(p => p.ForumTopicId == topicId);
        //}

        //public ForumPost GetForumPostByPostId(long postId)
        //{
        //    return db.ForumPosts.Where(p => p.ForumPostId == postId).SingleOrDefault();
        //}

        //public void Add(ForumPost post)
        //{
        //    db.ForumPosts.InsertOnSubmit(post);
        //}

        //internal ForumPost GetFirstForumPostForForumTopic(long forumTopicId)
        //{
        //    ForumPost post = db.ForumPosts.Where(p => p.ForumTopicId == forumTopicId).OrderBy(p => p.ForumPostId).FirstOrDefault();
        //    return post;
        //}

        //#endregion

        #endregion
    }
}
