using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;

namespace Veritas.Tests
{
    public class FakeModelCreator
    {
        public static BlogConfig GetFakeBlogConfig()
        {
            BlogConfig blogConfig = new BlogConfig()
            {
                //Host = "test.com",
                Host = "localhosttest",
                LastUpdateDate = DateTime.Now,
                CreateDate = DateTime.Now,
                ConfigXml = "<BlogConfig><LogFilePath>C:\\VeritasTestLogs\\log.txt</LogFilePath><LogToDb>true</LogToDb><LogToFile>true</LogToFile></BlogConfig>"
            };
            return blogConfig;
        }

        public static BlogMenuItem GetFakeBlogMenuItem(int blogConfigId)
        {
            BlogMenuItem blogMenuItem = new BlogMenuItem()
            {
                BlogConfigId = blogConfigId,
                CreateDate = DateTime.Now,
                IsView = false,
                LastUpdateDate = DateTime.Now,
                LinkText = "test",
                LinkUrl = "test",
                MenuItemOrder = 0,
                PageContent = "test",
                ViewName = "test"
            };
            return blogMenuItem;
        }

        public static BlogFeedbackAuthor GetFakeBlogFeedbackAuthor(int blogConfigId)
        {
            var blogAuthor = new BlogFeedbackAuthor()
            {
                BlogConfigId = blogConfigId,
                CreateDate = DateTime.Now,
                Email = "testblogfeedbackauthor@test.com",
                FeedbackTotal = 0,
                LastUpdateDate = DateTime.Now,
                Name = "test",
                Url = "test"
            };
            return blogAuthor;
        }

        public static BlogUser GetFakeBlogUser(int blogConfigId)
        {
            var blogUser = new BlogUser()
            {
                About = "test",
                BlogConfigId = blogConfigId,
                CreateDate = DateTime.Now,
                EmailAddress = "blogUser@test.com",
                LastUpdateDate = DateTime.Now,
                NotifyForFeedback = true,
                Password = "test",
                Username = "test"
            };
            return blogUser;
        }

        public static BlogEntry GetFakeBlogEntry(int blogConfigId, int blogUserId)
        {
            var blogEntry = new BlogEntry()
            {
                BlogAuthorId = blogUserId,
                BlogConfigId = blogConfigId,
                CreateDate = DateTime.Now,
                EntryName = "Test", 
                Keywords = "test", 
                FeedbackCount = 0,
                LastUpdateDate = DateTime.Now,
                PostType = 1,
                PublishDate = DateTime.Now,
                Short = "test",
                Text = "test",
                Title = "test"                
            };
            return blogEntry;
        }

        public static BlogEntryCategory GetFakeBlogEntryCategory(long blogEntryId, int blogCategoryId)
        {
            var blogEntryCategory = new BlogEntryCategory()
            {
                BlogCategoryId = blogCategoryId,
                BlogEntryId = blogEntryId,
                CreateDate = DateTime.Now
            };
            return blogEntryCategory;
        }

        public static BlogCategory GetFakeBlogCategory(int blogConfigId, int blogUserId)
        {
            var blogCategory = new BlogCategory()
            {
                BlogConfigId = blogConfigId,
                CreateDate = DateTime.Now,
                CreatedById = blogUserId,
                Description = "Test", 
                IsActive = true,
                Title = "test"
            };
            return blogCategory;
        }

        public static BlogEntryViewCount GetFakeBlogEntryViewCount(int blogConfigId, long blogEntryId)
        {
            var blogEntryViewCount = new BlogEntryViewCount()
            {
                BlogConfigId = blogConfigId,
                BlogEntryId = blogEntryId,
                WebCount = 0,
                WebLastUpdated = DateTime.Now
            };
            return blogEntryViewCount;
        }

        public static BlogLog GetFakeBlogLog(int blogConfigId)
        {
            var blogLog = new BlogLog()
            {
                BlogConfigId = blogConfigId,
                CreateDate = DateTime.Now,
                EventLevel = "Test",
                Exception = "Test",
                Logger = "Test",
                Message = "Test",
                Url = "Test"
            };
            return blogLog;
        }

        public static BlogMedia GetFakeBlogMedia(int blogConfigId, int blogUserId)
        {
            var blogMedia = new BlogMedia()
            {
                BlogConfigId = blogConfigId,
                CreateDate = DateTime.Now,
                CreatedById = blogUserId,
                FileName = "test",
                FilePath = "test",
                ServerPath = "test"
            };
            return blogMedia;
        }

        public static BlogFeedback GetFakeBlogFeedback(int blogConfigId, long blogEntryId, long blogFeedbackAuthorId)
        {
            var blogFeedback = new BlogFeedback()
            {
                BlogConfigId = blogConfigId,
                BlogEntryId = blogEntryId,
                BlogFeedbackAuthorId = blogFeedbackAuthorId,
                Body = "Test",
                CreateDate = DateTime.Now,
                FeedbackType = 0,
                IpAddress = "test",
                IsBlogAuthor = false,
                LastUpdateDate = DateTime.Now,
                NotifyAuthorOnFeedback = false,
                Status = 1,
                Title = "Test",
                UserAgent = "Test"
            };
            return blogFeedback;
        }

        public static BlogPage GetFakeBlogPage(int blogConfigId, int blogUserId)
        {
            var blogPage = new BlogPage()
            {
                BlogConfigId = blogConfigId,
                CreateDate = DateTime.Now,
                CreatedById = blogUserId,
                Description = "Test",
                EncodedTitle = "Test",
                Keywords = "Test",
                LastUpdateDate = DateTime.Now,
                LastUpdatedById = blogUserId,
                PageContent = "Test",
                PageTitle = "Test"
            };
            return blogPage;
        }

        public static Blacklist GetFakeBlacklist(int blogConfigId)
        {
            var blacklist = new Blacklist()
            {
                BlogConfigId = blogConfigId,
                CreateDate = DateTime.Now,
                EmailAddress = "test@test.com"
            };
            return blacklist;
        }

        public static BlogRole GetFakeBlogRole(int blogConfigId)
        {
            var blogRole = new BlogRole()
            {
                BlogConfigId = blogConfigId,
                CreateDate = DateTime.Now,
                RoleName = "Author"
            };
            return blogRole;
        }

        public static BlogUserRole GetFakeBlogUserRole(int blogUserId, int blogRoleId)
        {
            var blogUserRole = new BlogUserRole()
            {
                BlogRoleId = blogRoleId,
                BlogUserId = blogUserId,
                CreateDate = DateTime.Now
            };
            return blogUserRole;
        }
    }
}
