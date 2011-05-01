using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CookComputing.XmlRpc;
using Veritas.DataLayer;
using Veritas.DataLayer.Models;
using Veritas.BusinessLayer.Caching;
using Veritas.BusinessLayer.Media;
using Veritas.BusinessLayer;
using Veritas.BusinessLayer.Files;

namespace Veritas.UI.Web.Access
{
    /// <summary>
    /// Summary description for MetaWeblogApi
    /// </summary>
    public class MetaWeblog : XmlRpcService, IMetaWeblog
    {
        #region Public Constructors

        public MetaWeblog()
        {
        }

        #endregion

        #region IMetaWeblog Members

        string IMetaWeblog.AddPost(string blogid, string username, string password,
            Post post, bool publish)
        {
            if (ValidateUser(username, password))
            {
                string id = string.Empty;

                //using (VeritasDataContext con = new VeritasDataContext())
                VeritasRepository repo = VeritasRepository.GetInstance();
                
                BlogUser user = repo.GetBlogUserByUserName(CacheHandler.BlogConfigId, username);

                //Check to see if the entry name exists:
                //if (repo.DoesEntryTitleExist(post.title, post.title.Replace(";", "-").Replace(" ", "-").Replace(".", "-").Replace("?", "-").Replace("%", "-"), CacheAccessor.GetBlogConfigID()))
                if (repo.DoesEntryTitleExist(CacheHandler.BlogConfigId, post.title, HttpUtility.HtmlEncode(post.title)))
                {
                    throw new XmlRpcFaultException(0, "The title you have tried already exists for this blog.");
                }

                BlogEntry entry = new BlogEntry();
                entry.BlogConfigId = CacheHandler.BlogConfigId;
                entry.BlogAuthorId = user.BlogUserId;

                entry.EntryName = EntryTitleLogic.GetEntryNameFromTitle(HttpUtility.HtmlDecode(post.title));
                    
                entry.FeedbackCount = 0;
                entry.PostType = publish ? (int)PostType.Published : (int)PostType.Draft;
                if (post.dateCreated.Year > 0001)
                    entry.PublishDate = post.dateCreated;
                else
                    entry.PublishDate = DateTime.Now;
                //entry.Text = post.description;
                entry.Text = HighSlideHandler.UpdateLiveWriterImagesWithHighslide(post.description);
                entry.Title = HttpUtility.HtmlDecode(post.title);
                entry.LastUpdateDate = DateTime.Now;
                entry.CreateDate = DateTime.Now;
                //id = con.SaveBlogEntry(entry).ToString();
                repo.Add(entry);
                repo.Save();
                id = entry.BlogEntryId.ToString();
                BlogConfig config = repo.GetBlogConfigByBlogConfigId(entry.BlogConfigId);
                config.PostCount++;
                repo.Save();

                BlogEntryCategory entCat = new BlogEntryCategory();                    
                repo.SaveEntryCategoryAssociation(entry.BlogConfigId, entry.BlogEntryId, post.categories);                

                //Save Blog Entry View 
                var existingViewCount = repo.GetBlogEntryViewCountByEntryId(entry.BlogEntryId);
                if (existingViewCount == null)
                {
                    BlogEntryViewCount entryViewCount = new BlogEntryViewCount()
                    {
                        BlogConfigId = entry.BlogConfigId,
                        BlogEntryId = entry.BlogEntryId,
                        WebCount = 0,
                        WebLastUpdated = DateTime.Now
                    };
                    repo.Add(entryViewCount);
                    repo.Save();
                }

                return id;
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        bool IMetaWeblog.UpdatePost(string postid, string username, string password,
            Post post, bool publish)
        {
            if (ValidateUser(username, password))
            {
                bool result = false;

                //using (VeritasDataContext con = new VeritasDataContext())
                VeritasRepository repo = VeritasRepository.GetInstance();
                    
                BlogUser user = repo.GetBlogUserByUserName(CacheHandler.BlogConfigId, username);

                BlogEntry entry = repo.GetEntryByEntryIDBlogConfigId(CacheHandler.BlogConfigId, Convert.ToInt64(postid));
                entry.BlogConfigId = CacheHandler.BlogConfigId;
                entry.BlogAuthorId = user.BlogUserId;

                entry.EntryName = EntryTitleLogic.GetEntryNameFromTitle(HttpUtility.HtmlDecode(post.title));
                entry.FeedbackCount = 0;
                entry.PostType = publish ? 1 : 0;
                entry.Text = HighSlideHandler.UpdateLiveWriterImagesWithHighslide(post.description);

                entry.Title = HttpUtility.HtmlDecode(post.title);
                entry.LastUpdateDate = DateTime.Now;

                repo.Save();
                string id = entry.BlogEntryId.ToString();
                if (id == postid)
                {
                    result = true;
                }
                repo.SaveEntryCategoryAssociation(CacheHandler.BlogConfigId, entry.BlogEntryId, post.categories);                

                return result;
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        Post IMetaWeblog.GetPost(string postid, string username, string password)
        {
            if (ValidateUser(username, password))
            {
                Post post = new Post();

                VeritasRepository repo = VeritasRepository.GetInstance();
                    
                BlogEntry entry = repo.GetEntryByEntryIDBlogConfigId(CacheHandler.BlogConfigId, Convert.ToInt64(postid));
                if (entry != null)
                {
                    post.description = entry.Text;
                    post.dateCreated = entry.CreateDate;
                    post.permalink = "http://" + CacheHandler.GetBlogConfig().Host + "/" + entry.EntryName;
                    post.postid = entry.BlogEntryId.ToString();
                    post.title = entry.Title;
                    post.userid = entry.BlogAuthorId.ToString();

                    List<string> cats = new List<string>();
                    foreach (BlogEntryCategory assoc in entry.BlogEntryCategories.ToList())
                    {
                        cats.Add(assoc.BlogCategory.Title);
                    }
                    post.categories = cats.ToArray();
                }                

                return post;
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        CategoryInfo[] IMetaWeblog.GetCategories(string blogid, string username, string password)
        {
            if (ValidateUser(username, password))
            {
                List<CategoryInfo> categoryInfos = new List<CategoryInfo>();

                VeritasRepository repo = VeritasRepository.GetInstance();
                    
                List<BlogCategory> categories = repo.GetBlogCategories(CacheHandler.BlogConfigId).ToList();
                foreach (var item in categories)
                {
                    CategoryInfo info = new CategoryInfo();
                    info.categoryid = item.BlogCategoryId.ToString();
                    info.description = item.Description != null ? item.Description : "";
                    info.title = item.Title;
                    info.htmlUrl = "http://" + CacheHandler.GetBlogConfig().Host + "/blog/category/" + item.Title;
                    //TODO:  Set RSS URL
                    info.rssUrl = "";
                    categoryInfos.Add(info);
                }                
                return categoryInfos.ToArray();
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        Post[] IMetaWeblog.GetRecentPosts(string blogid, string username, string password,
            int numberOfPosts)
        {
            if (ValidateUser(username, password))
            {
                List<Post> posts = new List<Post>();

                VeritasRepository repo = VeritasRepository.GetInstance();

                List<BlogEntry> entries = repo.GetRecentEntries(CacheHandler.BlogConfigId, numberOfPosts).ToList();
                foreach (var item in entries)
                {
                    Post pst = new Post();

                    pst.dateCreated = item.CreateDate;
                    pst.description = item.Short;
                    pst.postid = item.BlogEntryId.ToString();
                    pst.title = item.Title;
                    pst.userid = item.BlogAuthorId.ToString();
                    pst.permalink = "http://" + CacheHandler.GetBlogConfig().Host + "/" + item.EntryName;

                    List<string> cats = new List<string>();                    
                    foreach (BlogEntryCategory assoc in item.BlogEntryCategories.ToList())
                    {
                        cats.Add(assoc.BlogCategory.Title);
                    }
                    pst.categories = cats.ToArray();
                    posts.Add(pst);
                }                

                return posts.ToArray();
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        MediaObjectInfo IMetaWeblog.NewMediaObject(string blogid, string username, string password,
            MediaObject mediaObject)
        {
            if (ValidateUser(username, password))
            {
                MediaObjectInfo objectInfo = new MediaObjectInfo();

                objectInfo = FileCreator.Create(mediaObject, username);
                return objectInfo;
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        bool IMetaWeblog.DeletePost(string key, string postid, string username, string password, bool publish)
        {
            if (ValidateUser(username, password))
            {
                bool result = false;

                VeritasRepository repo = VeritasRepository.GetInstance();
                BlogEntry entry = repo.GetEntryByEntryIDBlogConfigId(CacheHandler.BlogConfigId, Convert.ToInt64(postid));
                repo.Delete(entry);                

                return result;
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        BlogInfo[] IMetaWeblog.GetUsersBlogs(string key, string username, string password)
        {
            if (ValidateUser(username, password))
            {
                List<BlogInfo> infoList = new List<BlogInfo>();

                BlogConfig config = CacheHandler.GetBlogConfig();
                BlogInfo info = new BlogInfo();
                info.blogid = config.BlogConfigId.ToString();
                info.blogName = config.Title;
                info.url = "http://" + config.Host;
                infoList.Add(info);

                return infoList.ToArray();
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        UserInfo IMetaWeblog.GetUserInfo(string key, string username, string password)
        {
            if (ValidateUser(username, password))
            {
                UserInfo info = new UserInfo();

                VeritasRepository repo = VeritasRepository.GetInstance();
                
                BlogUser author = repo.GetBlogUserByUserName(CacheHandler.BlogConfigId, username);
                info.email = author.EmailAddress;
                //TODO:  Set info.firstname, info.lastname, info.nickname
                info.userid = author.BlogUserId.ToString();            

                return info;
            }
            throw new XmlRpcFaultException(0, "User is not valid!");
        }

        #endregion

        #region Private Methods

        private bool ValidateUser(string username, string password)
        {
            bool result = false;

            VeritasRepository repo = VeritasRepository.GetInstance();

            result = repo.IsUserAuthor(CacheHandler.BlogConfigId, username, password);

            return result;
        }

        #endregion
    }

    public interface IMetaWeblog
    {
        #region MetaWeblog API

        [XmlRpcMethod("metaWeblog.newPost")]
        string AddPost(string blogid, string username, string password, Post post, bool publish);

        [XmlRpcMethod("metaWeblog.editPost")]
        bool UpdatePost(string postid, string username, string password, Post post, bool publish);

        [XmlRpcMethod("metaWeblog.getPost")]
        Post GetPost(string postid, string username, string password);

        [XmlRpcMethod("metaWeblog.getCategories")]
        CategoryInfo[] GetCategories(string blogid, string username, string password);

        [XmlRpcMethod("metaWeblog.getRecentPosts")]
        Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts);

        [XmlRpcMethod("metaWeblog.newMediaObject")]
        MediaObjectInfo NewMediaObject(string blogid, string username, string password,
            MediaObject mediaObject);

        #endregion

        #region Blogger API

        [XmlRpcMethod("blogger.deletePost")]
        [return: XmlRpcReturnValue(Description = "Returns true.")]
        bool DeletePost(string key, string postid, string username, string password, bool publish);

        [XmlRpcMethod("blogger.getUsersBlogs")]
        BlogInfo[] GetUsersBlogs(string key, string username, string password);

        [XmlRpcMethod("blogger.getUserInfo")]
        UserInfo GetUserInfo(string key, string username, string password);

        #endregion
    }

    #region Structs

    public struct BlogInfo
    {
        public string blogid;
        public string url;
        public string blogName;
    }

    public struct Category
    {
        public string categoryId;
        public string categoryName;
    }

    [Serializable]
    public struct CategoryInfo
    {
        public string description;
        public string htmlUrl;
        public string rssUrl;
        public string title;
        public string categoryid;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct Enclosure
    {
        public int length;
        public string type;
        public string url;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct Post
    {
        public DateTime dateCreated;
        public string description;
        public string title;
        public string[] categories;
        public string permalink;
        public object postid;
        public string userid;
        public string wp_slug;
    }


    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct Source
    {
        public string name;
        public string url;
    }

    public struct UserInfo
    {
        public string userid;
        public string firstname;
        public string lastname;
        public string nickname;
        public string email;
        public string url;
    }

    

    #endregion
}
