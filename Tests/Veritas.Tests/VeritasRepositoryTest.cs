using Veritas.DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Veritas.DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Veritas.Tests
{
    
    
    /// <summary>
    ///This is a test class for VeritasRepositoryTest and is intended
    ///to contain all VeritasRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VeritasRepositoryTest : TestBase
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for VeritasRepository Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Veritas.DataLayer.dll")]
        public void VeritasRepositoryConstructorTest()
        {
            VeritasRepository_Accessor target = new VeritasRepository_Accessor();
            Assert.IsNotNull(target);
            //Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for ForceNewInstance
        ///</summary>
        [TestMethod()]
        public void ForceNewInstanceTest()
        {
            VeritasRepository actual;
            actual = VeritasRepository.ForceNewInstance();
            Assert.IsNotNull(actual);
            
        }

        /// <summary>
        ///A test for GetInstance
        ///</summary>
        [TestMethod()]
        public void GetInstanceTest()
        {            
            VeritasRepository actual;
            actual = VeritasRepository.GetInstance();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Add BlogConfig
        ///</summary>
        [TestMethod()]
        public void AddBlogConfigTest()
        {
            BlogConfig blogConfig = FakeModelCreator.GetFakeBlogConfig();
            repo.Add(blogConfig);
            repo.Save();
            //Check the db for changes
            var configs = repo.GetAllBlogConfigs().ToArray();
            var testConfig = configs.Where(p => p.Host == "localhosttest").FirstOrDefault();
            Assert.IsNotNull(testConfig);            
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogConfigTest()
        {
            var blogConfig = repo.GetAllBlogConfigs()
                .Where(p => p.Host == FakeModelCreator.GetFakeBlogConfig().Host).FirstOrDefault();
            Assert.IsNotNull(blogConfig);
            repo.Delete(TestBlogUserRole);
            repo.Delete(TestBlogEntryCategory);
            repo.Delete(TestBlogCategory);            
            repo.Delete(TestBlogEntryViewCount);
            repo.Delete(TestBlogFeedback);
            repo.Delete(TestBlogFeedbackAuthor);
            repo.Delete(TestBlogEntry);                                   
            repo.Delete(TestBlogLog);
            repo.Delete(TestBlogMedia);
            repo.Delete(TestBlogMenuItem);
            repo.Delete(TestBlogPage);
            repo.Delete(TestBlogRole);
            repo.Delete(TestBlogUser);
            repo.Delete(TestBlacklist);
            repo.Delete(blogConfig);
            repo.Save();

            blogConfig = repo.GetAllBlogConfigs().Where(p => p.Host == blogConfig.Host).FirstOrDefault();
            Assert.IsNull(blogConfig);
            
        }

        /// <summary>
        ///A test for GetAllBlogConfigs
        ///</summary>
        [TestMethod()]
        public void GetAllBlogConfigsTest()
        {
            int count = repo.GetAllBlogConfigs().Count();
            Assert.IsTrue(count > 0);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogMenuItemTest()
        {
            var blogMenuItem = FakeModelCreator.GetFakeBlogMenuItem(TestBlogConfig.BlogConfigId);

            repo.Add(blogMenuItem);
            repo.Save();

            var menuItems = repo.GetBlogMenuItems(TestBlogConfig.BlogConfigId);
            Assert.IsNotNull(menuItems);
            Assert.IsTrue(menuItems.Count() > 0);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogEntryCategoryTest()
        {
            var blogEntryCategory = FakeModelCreator.GetFakeBlogEntryCategory(TestBlogEntry.BlogEntryId, TestBlogCategory.BlogCategoryId);
            repo.Add(blogEntryCategory);
            repo.Save();

            var entryCategories = repo.GetBlogEntryCategoriesByEntryId(TestBlogEntry.BlogEntryId);
            Assert.IsNotNull(entryCategories);
            Assert.IsTrue(entryCategories.Count() > 0);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogEntryTest()
        {
            var blogEntry = FakeModelCreator.GetFakeBlogEntry(TestBlogConfig.BlogConfigId, TestBlogUser.BlogUserId);
            repo.Add(blogEntry);
            repo.Save();

            var entries = repo.GetBlogEntries(TestBlogConfig.BlogConfigId);
            Assert.IsNotNull(entries);
            Assert.IsTrue(entries.Count() > 1);

        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogEntryViewCountTest()
        {
            var blogEntryViewCount = FakeModelCreator.GetFakeBlogEntryViewCount(TestBlogConfig.BlogConfigId, TestBlogEntry.BlogEntryId);

            repo.Add(blogEntryViewCount);
            repo.Save();

            var entryViewCount = repo.GetBlogEntryViewCountByEntryId(TestBlogEntry.BlogEntryId);
            Assert.IsNotNull(entryViewCount);
            Assert.IsTrue(entryViewCount.WebCount == 0);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogFeedbackAuthorTest()
        {
            var blogFeedbackAuthor = FakeModelCreator.GetFakeBlogFeedbackAuthor(TestBlogConfig.BlogConfigId);
            repo.Add(blogFeedbackAuthor);
            repo.Save();

            var dbValue = repo.GetBlogFeedbackAuthorById(TestBlogConfig.BlogConfigId, blogFeedbackAuthor.BlogFeedbackAuthorId);
            Assert.IsNotNull(dbValue);
            Assert.AreEqual(dbValue.BlogFeedbackAuthorId, blogFeedbackAuthor.BlogFeedbackAuthorId);
            Assert.AreEqual(dbValue.Email, blogFeedbackAuthor.Email);

        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogLogTest()
        {
            var blogLog = FakeModelCreator.GetFakeBlogLog(TestBlogConfig.BlogConfigId);
            repo.Add(blogLog);
            repo.Save();

            var logs = repo.GetBlogLogs(TestBlogConfig.BlogConfigId).ToArray();
            Assert.IsNotNull(logs);
            Assert.IsTrue(logs.Length > 0);
            Assert.IsNotNull(logs.Where(p => p.BlogLogId == blogLog.BlogLogId).FirstOrDefault());
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogMediaTest()
        {
            var blogMedia = FakeModelCreator.GetFakeBlogMedia(TestBlogConfig.BlogConfigId, TestBlogUser.BlogUserId);
            repo.Add(blogMedia);
            repo.Save();

            var blogMedias = repo.GetBlogMedias(TestBlogConfig.BlogConfigId).ToArray();
            Assert.IsNotNull(blogMedias);
            Assert.IsTrue(blogMedias.Length > 0);
            Assert.IsNotNull(blogMedias.Where(p => p.BlogMediaId == blogMedia.BlogMediaId).FirstOrDefault());
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogFeedbackTest()
        {
            var blogFeedback = FakeModelCreator.GetFakeBlogFeedback(TestBlogConfig.BlogConfigId, TestBlogEntry.BlogEntryId, TestBlogFeedbackAuthor.BlogFeedbackAuthorId);
            repo.Add(blogFeedback);
            repo.Save();

            var feedbacks = repo.GetBlogFeedbackByEntryId(TestBlogEntry.BlogEntryId).ToArray();
            Assert.IsNotNull(feedbacks);
            Assert.IsTrue(feedbacks.Length > 0);
            Assert.IsNotNull(feedbacks.Where(p => p.BlogFeedbackId == blogFeedback.BlogFeedbackId).FirstOrDefault());
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogPageTest()
        {
            var blogPage = FakeModelCreator.GetFakeBlogPage(TestBlogConfig.BlogConfigId, TestBlogUser.BlogUserId);
            repo.Add(blogPage);
            repo.Save();

            var dbBlogPage = repo.GetBlogPageByPageId(blogPage.BlogConfigId, blogPage.BlogPageId);
            Assert.IsNotNull(dbBlogPage);
            Assert.AreEqual(dbBlogPage.BlogPageId, blogPage.BlogPageId);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogUserTest()
        {
            var blogUser = FakeModelCreator.GetFakeBlogUser(TestBlogConfig.BlogConfigId);
            repo.Add(blogUser);
            repo.Save();

            var dbBlogUser = repo.GetBlogUserByUserId(blogUser.BlogConfigId, blogUser.BlogUserId);
            Assert.IsNotNull(dbBlogUser);
            Assert.AreEqual(dbBlogUser.BlogUserId, blogUser.BlogUserId);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlacklistTest()
        {
            var blacklist = FakeModelCreator.GetFakeBlacklist(TestBlogConfig.BlogConfigId);
            repo.Add(blacklist);
            repo.Save();

            var dbBlacklist = repo.GetBlacklistByEmailAddress(TestBlogConfig.BlogConfigId, blacklist.EmailAddress);
            Assert.IsNotNull(dbBlacklist);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogCategoryTest()
        {
            var blogCategory = FakeModelCreator.GetFakeBlogCategory(TestBlogConfig.BlogConfigId, TestBlogUser.BlogUserId);
            repo.Add(blogCategory);
            repo.Save();

            var dbBlogCategories = repo.GetBlogCategories(TestBlogConfig.BlogConfigId).ToArray();
            Assert.IsNotNull(dbBlogCategories);
            Assert.IsNotNull(dbBlogCategories.Where(p => p.BlogCategoryId == blogCategory.BlogCategoryId).SingleOrDefault());
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogRoleTest()
        {
            var blogRole = FakeModelCreator.GetFakeBlogRole(TestBlogConfig.BlogConfigId);
            repo.Add(blogRole);
            repo.Save();

            var dbBlogRoles = repo.GetBlogRoles(TestBlogConfig.BlogConfigId).ToArray();
            Assert.IsNotNull(dbBlogRoles);
            Assert.IsNotNull(dbBlogRoles.Where(p => p.BlogRoleId == blogRole.BlogRoleId).SingleOrDefault());
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddBlogUserRoleTest()
        {
            var blogUserRole = FakeModelCreator.GetFakeBlogUserRole(TestBlogUser.BlogUserId, TestBlogRole.BlogRoleId);
            repo.Add(blogUserRole);
            repo.Save();

            var dbBlogUserRoles = repo.GetBlogUserRolesByUserId(blogUserRole.BlogUserId).ToArray();
            Assert.IsNotNull(dbBlogUserRoles);
            Assert.IsNotNull(dbBlogUserRoles.Where(p => p.BlogUserRoleId == blogUserRole.BlogUserRoleId).SingleOrDefault());

        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogUserRoleTest()
        {
            var dbBlogUserRole = repo.GetBlogUserRolesByUserId(TestBlogUser.BlogUserId).FirstOrDefault();
            Assert.IsNotNull(dbBlogUserRole);
            repo.Delete(dbBlogUserRole);
            repo.Save();
            var dbBlogUserRole2 = repo.GetBlogUserRolesByUserId(TestBlogUser.BlogUserId).FirstOrDefault();
            Assert.IsNull(dbBlogUserRole2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogUserTest()
        {
            var dbBlogUser = repo.GetBlogUsers(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNotNull(dbBlogUser);
            repo.Delete(TestBlogUserRole);
            repo.Delete(TestBlogEntryCategory);
            repo.Delete(TestBlogCategory);
            repo.Delete(TestBlogEntryViewCount);
            repo.Delete(TestBlogFeedback);
            repo.Delete(TestBlogFeedbackAuthor);
            repo.Delete(TestBlogEntry);
            repo.Delete(TestBlogLog);
            repo.Delete(TestBlogMedia);
            repo.Delete(TestBlogMenuItem);
            repo.Delete(TestBlogPage);
            repo.Delete(TestBlogRole);
            repo.Delete(dbBlogUser);
            repo.Save();
            var dbBlogUser2 = repo.GetBlogUsers(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNull(dbBlogUser2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogLogTest()
        {
            var dbBlogLog = repo.GetBlogLogs(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNotNull(dbBlogLog);
            repo.Delete(dbBlogLog);
            repo.Save();
            var dbBlogLog2 = repo.GetBlogLogs(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNull(dbBlogLog2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogMenuItemTest()
        {
            var dbBlogMenuItem = repo.GetBlogMenuItems(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNotNull(dbBlogMenuItem);
            repo.Delete(dbBlogMenuItem);
            repo.Save();
            var dbBlogMenuItem2 = repo.GetBlogMenuItems(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNull(dbBlogMenuItem2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogPageTest()
        {
            var dbBlogPage = repo.GetBlogPages(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNotNull(dbBlogPage);
            repo.Delete(dbBlogPage);
            repo.Save();
            var dbBlogPage2 = repo.GetBlogPages(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNull(dbBlogPage2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteRoleTest()
        {
            var dbBlogRole = repo.GetBlogRoles(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNotNull(dbBlogRole);
            repo.Delete(TestBlogUserRole);
            repo.Delete(dbBlogRole);
            repo.Save();
            var dbBlogRole2 = repo.GetBlogRoles(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNull(dbBlogRole2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogMediaTest()
        {
            var dbBlogMedia = repo.GetBlogMedias(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNotNull(dbBlogMedia);
            repo.Delete(dbBlogMedia);
            repo.Save();
            var dbBlogMedia2 = repo.GetBlogMedias(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNull(dbBlogMedia2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogEntryTest()
        {
            var dbBlogEntry = repo.GetBlogEntries(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNotNull(dbBlogEntry);
            repo.Delete(TestBlogUserRole);
            repo.Delete(TestBlogEntryCategory);
            repo.Delete(TestBlogCategory);
            repo.Delete(TestBlogEntryViewCount);
            repo.Delete(TestBlogFeedback);
            repo.Delete(TestBlogFeedbackAuthor);            
            repo.Delete(dbBlogEntry);
            repo.Save();
            var dbBlogEntry2 = repo.GetBlogEntries(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNull(dbBlogEntry2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlacklistTest()
        {
            var dbBlacklist = repo.GetBlacklistByEmailAddress(TestBlogConfig.BlogConfigId, TestBlacklist.EmailAddress);
            Assert.IsNotNull(dbBlacklist);
            repo.Delete(dbBlacklist);
            repo.Save();
            var dbBlacklist2 = repo.GetBlacklistByEmailAddress(TestBlogConfig.BlogConfigId, TestBlacklist.EmailAddress);
            Assert.IsNull(dbBlacklist2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogCategoryTest()
        {
            var dbBlogCategory = repo.GetBlogCategories(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNotNull(dbBlogCategory);
            repo.Delete(TestBlogUserRole);
            repo.Delete(TestBlogEntryCategory);

            repo.Delete(dbBlogCategory);
            repo.Save();
            var dbBlogCategory2 = repo.GetBlogCategories(TestBlogConfig.BlogConfigId).FirstOrDefault();
            Assert.IsNull(dbBlogCategory2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogFeedbackAuthorTest()
        {
            var dbBlogFeedbackAuthor = repo.GetBlogFeedbackAuthorById(TestBlogFeedbackAuthor.BlogConfigId, TestBlogFeedbackAuthor.BlogFeedbackAuthorId);
            Assert.IsNotNull(dbBlogFeedbackAuthor);
            repo.Delete(TestBlogFeedback);
            repo.Delete(dbBlogFeedbackAuthor);
            repo.Save();
            var dbBlogFeedbackAuthor2 = repo.GetBlogFeedbackAuthorById(TestBlogFeedbackAuthor.BlogConfigId, TestBlogFeedbackAuthor.BlogFeedbackAuthorId);
            Assert.IsNull(dbBlogFeedbackAuthor2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogFeedbackTest()
        {
            var dbBlogFeedback = repo.GetBlogFeedbackByEntryId(TestBlogEntry.BlogEntryId).FirstOrDefault();
            Assert.IsNotNull(dbBlogFeedback);
            repo.Delete(dbBlogFeedback);
            repo.Save();
            var dbBlogFeedback2 = repo.GetBlogFeedbackByEntryId(TestBlogEntry.BlogEntryId).FirstOrDefault();
            Assert.IsNull(dbBlogFeedback2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogEntryCategoryTest()
        {
            var dbBlogEntryCategory = repo.GetBlogEntryCategoriesByEntryId(TestBlogEntry.BlogEntryId).FirstOrDefault();
            Assert.IsNotNull(dbBlogEntryCategory);
            repo.Delete(dbBlogEntryCategory);
            repo.Save();
            var dbBlogEntryCategory2 = repo.GetBlogEntryCategoriesByEntryId(TestBlogEntry.BlogEntryId).FirstOrDefault();
            Assert.IsNull(dbBlogEntryCategory2);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteBlogEntryViewCountTest()
        {
            var dbBlogEntryViewCount = repo.GetBlogEntryViewCountByEntryId(TestBlogEntry.BlogEntryId);
            Assert.IsNotNull(dbBlogEntryViewCount);
            repo.Delete(dbBlogEntryViewCount);
            repo.Save();
            var dbBlogEntryViewCount2 = repo.GetBlogEntryViewCountByEntryId(TestBlogEntry.BlogEntryId);
            Assert.IsNull(dbBlogEntryViewCount2);
        }

        /// <summary>
        ///A test for GetBlogConfigByBlogConfigId
        ///</summary>
        [TestMethod()]
        public void GetBlogConfigByBlogConfigIdTest()
        {
            var blogConfig = repo.GetBlogConfigByBlogConfigId(TestBlogConfig.BlogConfigId);
            Assert.IsNotNull(blogConfig);
            Assert.AreEqual(blogConfig, TestBlogConfig);
        }

        /// <summary>
        ///A test for GetBlogConfigByHostname
        ///</summary>
        [TestMethod()]
        public void GetBlogConfigByHostnameTest()
        {
            var blogConfig = repo.GetBlogConfigByHostname(TestBlogConfig.Host);
            Assert.IsNotNull(blogConfig);
            Assert.AreEqual(blogConfig, TestBlogConfig);
        }

        /// <summary>
        ///A test for GetEntriesFromStartPoint
        ///</summary>
        [TestMethod()]
        public void GetEntriesFromStartPointTest()
        {
            var blogEntry = FakeModelCreator.GetFakeBlogEntry(TestBlogConfig.BlogConfigId, TestBlogUser.BlogUserId);
            repo.Add(blogEntry);
            repo.Save();

            var entriesFromDB = repo.GetEntriesFromStartPoint(TestBlogConfig.BlogConfigId, 10, 0).ToArray();
            Assert.IsNotNull(entriesFromDB);
            var entriesFromDB2 = repo.GetEntriesFromStartPoint(TestBlogConfig.BlogConfigId, 10, 1).ToArray();
            Assert.IsNotNull(entriesFromDB2);
            Assert.IsTrue(entriesFromDB.Length > entriesFromDB2.Length);
        }

        /// <summary>
        ///A test for GetEntriesForCategoryFromStartPoint
        ///</summary>
        [TestMethod()]
        public void GetEntriesForCategoryFromStartPointTest()
        {
            var blogEntry = new BlogEntry()
            {
                BlogConfigId = TestBlogConfig.BlogConfigId,
                BlogAuthorId = TestBlogUser.BlogUserId,
                CreateDate = DateTime.Now,
                EntryName = "Test2",
                FeedbackCount = 0,
                Keywords = "",
                LastUpdateDate = DateTime.Now,
                PostType = (int)PostType.Published,
                PublishDate = DateTime.Now,
                Short = "Test2",
                Text = "Test2",
                Title = "Test2"
            };
            repo.Add(blogEntry);
            repo.Save();

            var blogEntryCategory = new BlogEntryCategory()
            {
               BlogCategoryId = TestBlogCategory.BlogCategoryId,
               BlogEntryId = blogEntry.BlogEntryId,
               CreateDate = DateTime.Now
            };
            repo.Add(blogEntryCategory);
            repo.Save();

            var recentEntries = repo.GetEntriesForCategoryFromStartPoint(TestBlogConfig.BlogConfigId, 2, 0, TestBlogCategory.Title).ToArray();            
            Assert.IsNotNull(recentEntries);
            Assert.IsTrue(recentEntries.Length == 2);
            var recentEntries2 = repo.GetEntriesForCategoryFromStartPoint(TestBlogConfig.BlogConfigId, 2, 1, TestBlogCategory.Title).ToArray();            
            Assert.IsNotNull(recentEntries2);
            Assert.IsTrue(recentEntries2.Length == 1);
        }

        /// <summary>
        ///A test for GetRecentEntries
        ///</summary>
        [TestMethod()]
        public void GetRecentEntriesTest()
        {
            var blogEntry = new BlogEntry()
            {
                BlogConfigId = TestBlogConfig.BlogConfigId,
                BlogAuthorId = TestBlogUser.BlogUserId,
                CreateDate = DateTime.Now,
                EntryName = "Test2",
                FeedbackCount = 0,
                Keywords = "",
                LastUpdateDate = DateTime.Now,
                PostType = (int) PostType.Published,
                PublishDate = DateTime.Now,
                Short = "Test2",
                Text = "Test2",
                Title = "Test2"
            };
            repo.Add(blogEntry);
            repo.Save();

            var recentEntries = repo.GetRecentEntries(TestBlogConfig.BlogConfigId, 1).ToArray();
            Assert.IsNotNull(recentEntries);
            Assert.IsTrue(recentEntries.Length == 1);
            var recentEntries2 = repo.GetRecentEntries(TestBlogConfig.BlogConfigId, 2).ToArray();
            Assert.IsNotNull(recentEntries2);
            Assert.IsTrue(recentEntries2.Length == 2);
        }

        /// <summary>
        ///A test for GetRecentEntries
        ///</summary>
        [TestMethod()]
        public void GetRecentEntriesTest1()
        {
            var recentEntries = repo.GetRecentEntries(TestBlogConfig.BlogConfigId, 1, TestBlogCategory.Title).ToArray();
                       
            Assert.IsNotNull(recentEntries);
            Assert.IsTrue(recentEntries.Length == 1);            
        }

        /// <summary>
        ///A test for GetEntriesForRSS
        ///</summary>
        [TestMethod()]
        public void GetEntriesForRSSTest()
        {
            var entriesFromDB = repo.GetEntriesForRSS(TestBlogConfig.BlogConfigId, 10, null).ToArray();
            var entriesFromDB2 = repo.GetEntriesForRSS(TestBlogConfig.BlogConfigId, 10, new List<int>() { TestBlogCategory.BlogCategoryId }).ToArray();
            Assert.IsNotNull(entriesFromDB);
            Assert.IsNotNull(entriesFromDB2);
            Assert.IsTrue(entriesFromDB.Length > entriesFromDB2.Length);
        }

        /// <summary>
        ///A test for GetTotalEntriesByBlogConfigID
        ///</summary>
        [TestMethod()]
        public void GetTotalEntriesByBlogConfigIDTest()
        {
            var result = repo.GetTotalEntriesByBlogConfigId(TestBlogConfig.BlogConfigId);
            Assert.IsTrue(result > 0);
        }

        /// <summary>
        ///A test for GetTotalEntriesByBlogConfigIDAndCategory
        ///</summary>
        [TestMethod()]
        public void GetTotalEntriesByBlogConfigIdAndCategoryTest()
        {
            var totalEntries = repo.GetTotalEntriesByBlogConfigIdAndCategory(TestBlogConfig.BlogConfigId, TestBlogCategory.Title);
            var totalEntries2 = repo.GetTotalEntriesByBlogConfigIdAndCategory(TestBlogConfig.BlogConfigId, "askjfsakdjf;askjf;saj");
            Assert.IsTrue(totalEntries > 0);
            Assert.IsTrue(totalEntries2 == 0);
        }

        /// <summary>
        ///A test for GetEntryByEntryNameAndBlogConfigID
        ///</summary>
        [TestMethod()]
        public void GetEntryByEntryNameAndBlogConfigIDTest()
        {
            var entryFromDB = repo.GetEntryByEntryNameAndBlogConfigId(TestBlogConfig.BlogConfigId, TestBlogEntry.EntryName);
            Assert.IsNotNull(entryFromDB);
            Assert.IsTrue(entryFromDB.EntryName == TestBlogEntry.EntryName);
        }

        /// <summary>
        ///A test for GetEntryByID
        ///</summary>
        [TestMethod()]
        public void GetEntryByIDTest()
        {
            var entry = repo.GetEntryById(TestBlogEntry.BlogEntryId);
            Assert.IsNotNull(entry);
            Assert.AreEqual(entry, TestBlogEntry);
        }

        /// <summary>
        ///A test for GetEntryByEntryIDBlogCOnfigID
        ///</summary>
        [TestMethod()]
        public void GetEntryByEntryIDBlogConfigIDTest()
        {            
            var entryFromDB = repo.GetEntryByEntryIDBlogConfigId(TestBlogConfig.BlogConfigId, TestBlogEntry.BlogEntryId);

            Assert.IsNotNull(entryFromDB);
            Assert.IsTrue(entryFromDB.BlogEntryId == TestBlogEntry.BlogEntryId);
        }

        /// <summary>
        ///A test for DoesEntryTitleExist
        ///</summary>
        [TestMethod()]
        public void DoesEntryTitleExistTest()
        {
            bool doesExist = repo.DoesEntryTitleExist(TestBlogConfig.BlogConfigId, TestBlogEntry.Title, TestBlogEntry.EntryName);
            Assert.IsTrue(doesExist);
            bool doesExist2 = repo.DoesEntryTitleExist(TestBlogConfig.BlogConfigId, TestBlogEntry.Title + "2", TestBlogEntry.EntryName + "2");
            Assert.IsFalse(doesExist2);            
        }

        /// <summary>
        ///A test for UpdateEntryViewCountForWeb
        ///</summary>
        [TestMethod()]
        public void UpdateEntryViewCountForWebTest()
        {
            int viewCount1 = repo.GetBlogEntryViewCountByEntryId(TestBlogEntryViewCount.BlogEntryId).WebCount;            
            repo.UpdateEntryViewCountForWeb(TestBlogConfig.BlogConfigId, TestBlogEntry.BlogEntryId);
            int viewCount2 = repo.GetBlogEntryViewCountByEntryId(TestBlogEntryViewCount.BlogEntryId).WebCount;
            Assert.IsTrue(viewCount2 == viewCount1+1);
            
        }

        /// <summary>
        ///A test for GetBlogFeedbackByFeedbackID
        ///</summary>
        [TestMethod()]
        public void GetBlogFeedbackByFeedbackIdTest()
        {
            var feedbackDB = repo.GetBlogFeedbackByFeedbackId(TestBlogConfig.BlogConfigId, TestBlogFeedback.BlogFeedbackId);
            Assert.IsNotNull(feedbackDB);
            Assert.IsTrue(feedbackDB.BlogFeedbackId == TestBlogFeedback.BlogFeedbackId);
        }

        /// <summary>
        ///A test for GetBlogFeedbacksByBlogConfigID
        ///</summary>
        [TestMethod()]
        public void GetBlogFeedbacksByBlogConfigIdTest()
        {
            var blogFeedbacks = repo.GetBlogFeedbacksByBlogConfigId(TestBlogConfig.BlogConfigId).ToArray();
            Assert.IsNotNull(blogFeedbacks);
            Assert.IsTrue(blogFeedbacks.Length > 0);
        }

        /// <summary>
        ///A test for GetFeedbackForRSS
        ///</summary>
        [TestMethod()]
        public void GetFeedbackForRSSTest()
        {            
            var feedback = repo.GetFeedbackForRSS(TestBlogConfig.BlogConfigId, 10);

            Assert.IsNotNull(feedback);
            Assert.IsTrue(feedback.Count() > 0);
        }

        /// <summary>
        ///A test for GetBlogCommentAuthors
        ///</summary>
        [TestMethod()]
        public void GetBlogFeedbackAuthorsTest()
        {
            var feedbackAuthors = repo.GetBlogFeedbackAuthors(TestBlogConfig.BlogConfigId).ToArray();
            Assert.IsNotNull(feedbackAuthors);
            Assert.IsTrue(feedbackAuthors.Length > 0);
        }

        /// <summary>
        ///A test for GetBlogCategoryByCategoryID
        ///</summary>
        [TestMethod()]
        public void GetBlogCategoryByCategoryIdTest()
        {
            var blogCategory = repo.GetBlogCategoryByCategoryId(TestBlogCategory.BlogConfigId, TestBlogCategory.BlogCategoryId);
            Assert.IsNotNull(blogCategory);
            Assert.AreEqual(blogCategory, TestBlogCategory);
        }

        /// <summary>
        ///A test for DoesCategoryExist
        ///</summary>
        [TestMethod()]
        public void DoesCategoryExistTest()
        {
            var doesExist = repo.DoesCategoryExist(TestBlogConfig.BlogConfigId, TestBlogCategory.Title);
            Assert.IsNotNull(doesExist);
            Assert.IsTrue(doesExist);

            var doesntExist = repo.DoesCategoryExist(TestBlogConfig.BlogConfigId, "dafasdfaksdfaslfjdklajfkljd;asjfjaj;");
            Assert.IsNotNull(doesntExist);
            Assert.IsFalse(doesntExist);
        }

        /// <summary>
        ///A test for SaveEntryCategoryAssociation
        ///</summary>
        [TestMethod()]
        public void SaveEntryCategoryAssociationTest()
        {
            var blogCategory = FakeModelCreator.GetFakeBlogCategory(TestBlogConfig.BlogConfigId, TestBlogUser.BlogUserId);
            repo.Add(blogCategory);
            repo.Save();

            var entryCategoryAssociation = FakeModelCreator.GetFakeBlogEntryCategory(TestBlogEntry.BlogEntryId, blogCategory.BlogCategoryId);
            repo.Add(entryCategoryAssociation);
            repo.Save();

            var entryCategoriesFromDB = repo.GetBlogEntryCategoriesByCategoryId(blogCategory.BlogCategoryId).ToArray();
            Assert.IsNotNull(entryCategoriesFromDB);
            Assert.IsNotNull(entryCategoriesFromDB.Where(p => p.BlogCategoryId == blogCategory.BlogCategoryId)
                .Where(p => p.BlogEntryId == TestBlogEntry.BlogEntryId).FirstOrDefault());

        }

        /// <summary>
        ///A test for GetCategoryTags
        ///</summary>
        [TestMethod()]
        public void GetCategoryTagsTest()
        {
            var tagsFromDB = repo.GetBlogCategoryTags(TestBlogConfig.BlogConfigId).ToArray();
            Assert.IsNotNull(tagsFromDB);
            Assert.IsTrue(tagsFromDB.Length > 0);
        }

        /// <summary>
        ///A test for GetBlogRoleByRoleId
        ///</summary>
        [TestMethod()]
        public void GetBlogRoleByRoleIdTest()
        {
            var blogRole = repo.GetBlogRoleByRoleId(TestBlogRole.BlogConfigId, TestBlogRole.BlogRoleId);
            Assert.IsNotNull(blogRole);
            Assert.AreEqual(blogRole, TestBlogRole);
        }

        /// <summary>
        ///A test for GetBlacklistsForEmailAddresses
        ///</summary>
        [TestMethod()]
        public void GetBlacklistsForEmailAddressesTest()
        {
            var result = repo.GetBlacklistsForEmailAddresses(TestBlogConfig.BlogConfigId, new string[] { TestBlacklist.EmailAddress }).ToArray();
            Assert.IsNotNull(result);
            Assert.IsTrue(result[0].EmailAddress == TestBlacklist.EmailAddress);
            var result2 = repo.GetBlacklistsForEmailAddresses(TestBlogConfig.BlogConfigId, new string[] { "aafsaf@afsdaf.com" }).ToArray();
            Assert.IsNotNull(result2);
        }

        /// <summary>
        ///A test for ChangePassword
        ///</summary>
        [TestMethod()]
        public void ChangePasswordTest()
        {
            string oldPassword = TestBlogUser.Password;
            repo.ChangePassword(TestBlogConfig.BlogConfigId, TestBlogUser.Username, TestBlogUser.Password, "AFFFF");
            repo.Save();
            var result1 = repo.ValidateUser(TestBlogConfig.BlogConfigId, TestBlogUser.Username, oldPassword);
            Assert.IsFalse(result1);
            var result2 = repo.ValidateUser(TestBlogConfig.BlogConfigId, TestBlogUser.Username, "AFFFF");
            Assert.IsTrue(result2);
        }

        /// <summary>
        ///A test for ValidateUser
        ///</summary>
        [TestMethod()]
        public void ValidateUserTest()
        {
            var result = repo.ValidateUser(TestBlogConfig.BlogConfigId, TestBlogUser.Username, TestBlogUser.Password);
            Assert.IsTrue(result);

            var result2 = repo.ValidateUser(TestBlogConfig.BlogConfigId, TestBlogUser.Username, "asdfsa");
            Assert.IsFalse(result2);
        }

        /// <summary>
        ///A test for IsUserAuthor
        ///</summary>
        [TestMethod()]
        public void IsUserAuthorTest()
        {
            BlogUser user = new BlogUser()
            {
                About = "Test2",
                BlogConfigId = TestBlogConfig.BlogConfigId,
                CreateDate = DateTime.Now,
                EmailAddress = "test2@test.com",
                LastUpdateDate = DateTime.Now,
                NotifyForFeedback = false,
                Password = "test2",
                Username = "test2"
            };
            repo.Add(user);
            repo.Save();

            bool isUser = repo.IsUserAuthor(TestBlogConfig.BlogConfigId, TestBlogUser.Username, TestBlogUser.Password);
            bool isNotUser = repo.IsUserAuthor(TestBlogConfig.BlogConfigId, user.Username, user.Password);
            Assert.IsTrue(isUser);
            Assert.IsFalse(isNotUser);
        }

        /// <summary>
        ///A test for GetBlogUsersInRoles
        ///</summary>
        [TestMethod()]
        public void GetBlogUsersInRolesTest()
        {
            var users = repo.GetBlogUsersInRoles(TestBlogConfig.BlogConfigId, "Author").ToArray();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Length > 0);
        }

        /// <summary>
        ///A test for GetRolesForUser
        ///</summary>
        [TestMethod()]
        public void GetRolesForUserTest()
        {
            var roles = repo.GetRolesForUser(TestBlogConfig.BlogConfigId, TestBlogUser.Username).ToArray();
            Assert.IsNotNull(roles);
            Assert.IsTrue(roles.Length > 0);
        }
    }
}
