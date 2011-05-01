using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer;
using Veritas.DataLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Web.Routing;
using System.IO;
using Veritas.BusinessLayer.Caching;

namespace Veritas.Tests
{
    [TestClass()]
    public class TestBase
    {
        public VeritasRepository repo;// = VeritasRepository.GetInstance();
        public BlogConfig TestBlogConfig { get; set; }
        public BlogUser TestBlogUser { get; set; }
        public BlogEntry TestBlogEntry { get; set; }
        public BlogCategory TestBlogCategory { get; set; }
        public BlogFeedbackAuthor TestBlogFeedbackAuthor { get; set; }
        public BlogRole TestBlogRole { get; set; }
        public BlogUserRole TestBlogUserRole { get; set; }
        public BlogLog TestBlogLog { get; set; }
        public BlogMenuItem TestBlogMenuItem { get; set; }
        public BlogPage TestBlogPage { get; set; }
        public BlogMedia TestBlogMedia { get; set; }
        public BlogFeedback TestBlogFeedback { get; set; }
        public Blacklist TestBlacklist { get; set; }
        public BlogEntryCategory TestBlogEntryCategory { get; set; }
        public BlogEntryViewCount TestBlogEntryViewCount { get; set; }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("test.aspx", "http://localhosttest/test.aspx", ""),
                new HttpResponse(new StringWriter())
                );

            repo = VeritasRepository.GetInstance();
            repo.StartTransaction();

            //Insert our fake BlogConfig
            BlogConfig blogConfig = FakeModelCreator.GetFakeBlogConfig();
            repo.Add(blogConfig);
            TestBlogConfig = blogConfig;
            
            repo.Save();
            TestBlogConfig.LoadConfigFromXml();

            TestBlogUser = FakeModelCreator.GetFakeBlogUser(TestBlogConfig.BlogConfigId);
            repo.Add(TestBlogUser);
            repo.Save();

            TestBlogEntry = FakeModelCreator.GetFakeBlogEntry(TestBlogConfig.BlogConfigId, TestBlogUser.BlogUserId);
            repo.Add(TestBlogEntry);
            TestBlogCategory = FakeModelCreator.GetFakeBlogCategory(TestBlogConfig.BlogConfigId, TestBlogUser.BlogUserId);
            repo.Add(TestBlogCategory);
            TestBlogFeedbackAuthor = FakeModelCreator.GetFakeBlogFeedbackAuthor(TestBlogConfig.BlogConfigId);
            repo.Add(TestBlogFeedbackAuthor);
            TestBlogRole = FakeModelCreator.GetFakeBlogRole(TestBlogConfig.BlogConfigId);
            repo.Add(TestBlogRole);
            TestBlogUserRole = FakeModelCreator.GetFakeBlogUserRole(TestBlogUser.BlogUserId, TestBlogRole.BlogRoleId);
            repo.Add(TestBlogUserRole);
            TestBlogLog = FakeModelCreator.GetFakeBlogLog(TestBlogConfig.BlogConfigId);
            repo.Add(TestBlogLog);
            TestBlogMenuItem = FakeModelCreator.GetFakeBlogMenuItem(TestBlogConfig.BlogConfigId);
            repo.Add(TestBlogMenuItem);
            TestBlogPage = FakeModelCreator.GetFakeBlogPage(TestBlogConfig.BlogConfigId, TestBlogUser.BlogUserId);
            repo.Add(TestBlogPage);
            TestBlogMedia = FakeModelCreator.GetFakeBlogMedia(TestBlogConfig.BlogConfigId, TestBlogUser.BlogUserId);
            repo.Add(TestBlogMedia);
            TestBlogFeedback = FakeModelCreator.GetFakeBlogFeedback(TestBlogConfig.BlogConfigId, TestBlogEntry.BlogEntryId, TestBlogFeedbackAuthor.BlogFeedbackAuthorId);
            repo.Add(TestBlogFeedback);
            TestBlacklist = FakeModelCreator.GetFakeBlacklist(TestBlogConfig.BlogConfigId);
            repo.Add(TestBlacklist);
            TestBlogEntryCategory = FakeModelCreator.GetFakeBlogEntryCategory(TestBlogEntry.BlogEntryId, TestBlogCategory.BlogCategoryId);
            repo.Add(TestBlogEntryCategory);
            TestBlogEntryViewCount = FakeModelCreator.GetFakeBlogEntryViewCount(TestBlogConfig.BlogConfigId, TestBlogEntry.BlogEntryId);
            repo.Add(TestBlogEntryViewCount);

            repo.Save();
            if (!Directory.Exists("C:\\VeritasTestLogs"))
                Directory.CreateDirectory("C:\\VeritasTestLogs");
            
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            repo.RollbackTransaction();
            HttpContext.Current.Cache.Remove(CacheHandler.BlogConfigCacheKey);

            if (File.Exists(TestBlogConfig.LogFilePath))
                File.Delete(TestBlogConfig.LogFilePath);
            if (Directory.Exists("C:\\VeritasTestLogs"))
                Directory.Delete("C:\\VeritasTestLogs");
        }
        
    }
}
