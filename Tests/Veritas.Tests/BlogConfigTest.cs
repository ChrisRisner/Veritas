using Veritas.DataLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Veritas.Tests
{
    
    
    /// <summary>
    ///This is a test class for BlogConfigTest and is intended
    ///to contain all BlogConfigTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BlogConfigTest : TestBase
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
        ///A test for LoadConfigFromXml
        ///</summary>
        [TestMethod()]
        public void LoadConfigFromXmlTest()
        {            
            BlogConfig target = new BlogConfig(); // TODO: Initialize to an appropriate value
            target.ConfigXml = "<BlogConfig><AkismetApiKey>abc123</AkismetApiKey><AllowComments>false</AllowComments><BlogAbout>This is what it's all about!</BlogAbout><BlogHeaderImage>../../ImagePath.jpg</BlogHeaderImage><BlogHeaderIsImage>true</BlogHeaderIsImage><BlogMarketingInfo><ShowSideBarAds>true</ShowSideBarAds><ShowEntryAds>true</ShowEntryAds><AdScriptSideBar>test</AdScriptSideBar><AdScriptEntry>test</AdScriptEntry></BlogMarketingInfo><CopyrightText>Copyright Test 2010</CopyrightText><DaysUntilCommentsClose /><EnableFeedbackAuthorNotifications>true</EnableFeedbackAuthorNotifications><EnableFeedbackRssFeed>false</EnableFeedbackRssFeed><FacebookUrl>http://facebook/crap</FacebookUrl><FeedburnerName>AbiteOfDet</FeedburnerName><FeedbackCount>0</FeedbackCount><FeedbackRequiresApproval>true</FeedbackRequiresApproval><IsActive>true</IsActive><GoogleApiKey>abc123</GoogleApiKey><HeaderScript>&amp;lt;script type=&amp;quot;text/javascript&amp;quot;&amp;gt;Test!&amp;lt;/script</HeaderScript><Language>en-US</Language><LogEmailAddress>emailLog@veritasblog.com</LogEmailAddress><LogFilePath>C:\\VeritasTestLogs\\log.txt</LogFilePath><LogToDb>true</LogToDb><LogToEmail>true</LogToEmail><LogToFile>false</LogToFile><NotifyAdminsForFeedback>true</NotifyAdminsForFeedback><PostCount>5</PostCount><PostsPerPage>3</PostsPerPage><RssUrl>http://feedburner.com/test</RssUrl><ShowAuthorsAbout>false</ShowAuthorsAbout><ShowBlogAbout>true</ShowBlogAbout><ShowGravatars>false</ShowGravatars><Skin>TEST</Skin><SmtpPassword>TEST</SmtpPassword><SmtpPort>23</SmtpPort><SmtpServer>google.com</SmtpServer><SmtpUserName>username</SmtpUserName><SmtpUseSsl>false</SmtpUseSsl><SubTitle>a subtitle</SubTitle><TimeZone>EASTERN</TimeZone><Title>THE TITLE</Title><TwitterUrl>http://twitter.com/abite</TwitterUrl><WebStatsJavascript>&amp;lt;script type=&amp;quot;text/javascript&amp;quot;&amp;gt;Web Stats Javascript!&amp;lt;/script</WebStatsJavascript></BlogConfig>";

            target.LoadConfigFromXml();

            //Assert.AreEqual(false, target.AllowComments);
            //Assert.AreEqual("This is what it's all about!", target.BlogAbout);
            //Assert.AreEqual(5, target.PostCount);            
            Assert.AreEqual(target.AkismetApiKey, "abc123");
            Assert.AreEqual(target.AllowComments, false);
            Assert.AreEqual(target.BlogAbout, "This is what it's all about!");
            Assert.AreEqual(target.BlogHeaderImage, "../../ImagePath.jpg");
            Assert.AreEqual(target.BlogHeaderIsImage, true);
            Assert.AreEqual(target.BlogMarketingInfo.AdScriptEntry, "test");
            Assert.AreEqual(target.BlogMarketingInfo.AdScriptSideBar, "test");
            Assert.AreEqual(target.BlogMarketingInfo.ShowEntryAds, true);
            Assert.AreEqual(target.BlogMarketingInfo.ShowSideBarAds, true);
            Assert.AreEqual(target.CopyrightText, "Copyright Test 2010");
            Assert.AreEqual(target.DaysUntilCommentsClose, null);
            Assert.AreEqual(target.EnableFeedbackAuthorNotifications, true);
            Assert.AreEqual(target.EnableFeedbackRssFeed, false);
            Assert.AreEqual(target.FacebookUrl, "http://facebook/crap");
            Assert.AreEqual(target.FeedbackRequiresApproval, true);
            Assert.AreEqual(target.FeedburnerName, "AbiteOfDet");
            Assert.AreEqual(target.GoogleApiKey, "abc123");
            Assert.AreEqual(target.HeaderScript, "<script type=\"text/javascript\">Test!</script");
            Assert.AreEqual(target.IsActive, true);
            Assert.AreEqual(target.Language, "en-US");
            Assert.AreEqual(target.LogEmailAddress, "emailLog@veritasblog.com");
            Assert.AreEqual(target.LogFilePath, "C:\\VeritasTestLogs\\log.txt");
            Assert.AreEqual(target.LogToDb, true);
            Assert.AreEqual(target.LogToEmail, true);
            Assert.AreEqual(target.LogToFile, false);
            Assert.AreEqual(target.NotifyAdminsForFeedback, true);
            Assert.AreEqual(target.PostCount, 5);
            Assert.AreEqual(target.PostsPerPage, 3);
            Assert.AreEqual(target.RssUrl, "http://feedburner.com/test");
            Assert.AreEqual(target.ShowAuthorsAbout, false);
            Assert.AreEqual(target.ShowBlogAbout, true);
            Assert.AreEqual(target.ShowGravatars, false);
            Assert.AreEqual(target.Skin, "TEST");
            Assert.AreEqual(target.SmtpPassword, "TEST");
            Assert.AreEqual(target.SmtpPort, 23);
            Assert.AreEqual(target.SmtpServer, "google.com");
            Assert.AreEqual(target.SmtpUserName, "username");
            Assert.AreEqual(target.SmtpUseSsl, false);
            Assert.AreEqual(target.SubTitle, "a subtitle");
            Assert.AreEqual(target.TimeZone, "EASTERN");
            Assert.AreEqual(target.Title, "THE TITLE");
            Assert.AreEqual(target.TwitterUrl, "http://twitter.com/abite");
            Assert.AreEqual(target.WebStatsJavascript, "<script type=\"text/javascript\">Web Stats Javascript!</script");

        }

        /// <summary>
        ///A test for BuildXmlFromConfig
        ///</summary>
        [TestMethod()]
        public void BuildXmlFromConfigTest()
        {
            BlogConfig target = new BlogConfig(); // TODO: Initialize to an appropriate value

            target.AkismetApiKey = "abc123";
            target.AllowComments = false;          
            target.BlogAbout = "This is what it's all about!";
            target.BlogHeaderImage = "../../ImagePath.jpg";
            target.BlogHeaderIsImage = true;
            target.BlogMarketingInfo.AdScriptEntry = "test";
            target.BlogMarketingInfo.AdScriptSideBar = "test";
            target.BlogMarketingInfo.ShowEntryAds = true;
            target.BlogMarketingInfo.ShowSideBarAds = true;
            target.CopyrightText = "Copyright Test 2010";
            target.DaysUntilCommentsClose = null;
            target.EnableFeedbackAuthorNotifications = true;
            target.EnableFeedbackRssFeed = false;
            target.FacebookUrl = "http://facebook/crap";
            target.FeedbackRequiresApproval = true;
            target.FeedburnerName = "AbiteOfDet";
            target.GoogleApiKey = "abc123";
            target.HeaderScript = "<script type=\"text/javascript\">Test!</script";
            target.IsActive = true;
            target.Language = "en-US";
            target.LogEmailAddress = "emailLog@veritasblog.com";
            target.LogFilePath = "C:\\VeritasTestLogs\\log.txt";
            target.LogToDb = true;
            target.LogToEmail = true;
            target.LogToFile = false;
            target.NotifyAdminsForFeedback = true;
            target.PostCount = 5;
            target.PostsPerPage = 3;
            target.RssUrl = "http://feedburner.com/test";
            target.ShowAuthorsAbout = false;
            target.ShowBlogAbout = true;
            target.ShowGravatars = false;
            target.Skin = "TEST";
            target.SmtpPassword = "TEST";
            target.SmtpPort = 23;
            target.SmtpServer = "google.com";
            target.SmtpUserName = "username";
            target.SmtpUseSsl = false;
            target.SubTitle = "a subtitle";
            target.TimeZone = "EASTERN";
            target.Title = "THE TITLE";
            target.TwitterUrl = "http://twitter.com/abite";
            target.WebStatsJavascript = "<script type=\"text/javascript\">Web Stats Javascript!</script";
            
            
            target.BuildXmlFromConfig();

            //Assert.AreEqual("<BlogConfig><AkismetApiKey>abc123</AkismetApiKey><AllowComments>false</AllowComments><BlogAbout>This is what it's all about!</BlogAbout><BlogHeaderImage>../../ImagePath.jpg</BlogHeaderImage><BlogHeaderIsImage>true</BlogHeaderIsImage><BlogMarketingInfo><ShowSideBarAds>true</ShowSideBarAds><ShowEntryAds>true</ShowEntryAds><AdScriptSideBar>test</AdScriptSideBar><AdScriptEntry>test</AdScriptEntry></BlogMarketingInfo><CopyrightText>Copyright Test 2010</CopyrightText><DaysUntilCommentsClose /><EnableFeedbackAuthorNotifications>true</EnableFeedbackAuthorNotifications><EnableFeedbackRssFeed>false</EnableFeedbackRssFeed><FacebookUrl>http://facebook/crap</FacebookUrl><FeedburnerName>AbiteOfDet</FeedburnerName><FeedbackCount>0</FeedbackCount><FeedbackRequiresApproval>true</FeedbackRequiresApproval><IsActive>true</IsActive><GoogleApiKey>abc123</GoogleApiKey><HeaderScript>&amp;lt;script type=&amp;quot;text/javascript&amp;quot;&amp;gt;Test!&amp;lt;/script</HeaderScript><Language>en-US</Language><LogEmailAddress>emailLog@veritasblog.com</LogEmailAddress><LogFilePath>C:\\VeritasTestLogs\\log.txt</LogFilePath><LogToDb>true</LogToDb><LogToEmail>true</LogToEmail><LogToFile>false</LogToFile><NotifyAdminsForFeedback>true</NotifyAdminsForFeedback><PostCount>5</PostCount><PostsPerPage>3</PostsPerPage><RssUrl>http://feedburner.com/test</RssUrl><ShowAuthorsAbout>false</ShowAuthorsAbout><ShowBlogAbout>true</ShowBlogAbout><ShowGravatars>false</ShowGravatars><Skin>TEST</Skin><SmtpPassword>TEST</SmtpPassword><SmtpPort>23</SmtpPort><SmtpServer>google.com</SmtpServer><SmtpUserName>username</SmtpUserName><SmtpUseSsl>false</SmtpUseSsl><SubTitle>a subtitle</SubTitle><TimeZone>EASTERN</TimeZone><Title>THE TITLE</Title><TwitterUrl>http://twitter.com/abite</TwitterUrl><WebStatsJavascript>&amp;lt;script type=&amp;quot;text/javascript&amp;quot;&amp;gt;Web Stats Javascript!&amp;lt;/script</WebStatsJavascript></BlogConfig>",
            Assert.AreEqual("<BlogConfig><AkismetApiKey>abc123</AkismetApiKey><AllowComments>false</AllowComments><BlogAbout>This is what it's all about!</BlogAbout><BlogHeaderImage>../../ImagePath.jpg</BlogHeaderImage><BlogHeaderIsImage>true</BlogHeaderIsImage><BlogCommentInfo><UseDefaultComments>false</UseDefaultComments><UseDisqusComments>false</UseDisqusComments><DisqusAccountName /></BlogCommentInfo><BlogMarketingInfo><ShowSideBarAds>true</ShowSideBarAds><ShowEntryAds>true</ShowEntryAds><AdScriptSideBar>test</AdScriptSideBar><AdScriptEntry>test</AdScriptEntry></BlogMarketingInfo><CopyrightText>Copyright Test 2010</CopyrightText><DaysUntilCommentsClose /><EnableFeedbackAuthorNotifications>true</EnableFeedbackAuthorNotifications><EnableFeedbackRssFeed>false</EnableFeedbackRssFeed><FacebookUrl>http://facebook/crap</FacebookUrl><FeedburnerName>AbiteOfDet</FeedburnerName><FeedbackCount>0</FeedbackCount><FeedbackRequiresApproval>true</FeedbackRequiresApproval><IsActive>true</IsActive><GoogleApiKey>abc123</GoogleApiKey><HeaderScript>&amp;lt;script type=&amp;quot;text/javascript&amp;quot;&amp;gt;Test!&amp;lt;/script</HeaderScript><Language>en-US</Language><LogEmailAddress>emailLog@veritasblog.com</LogEmailAddress><LogFilePath>C:\\VeritasTestLogs\\log.txt</LogFilePath><LogToDb>true</LogToDb><LogToEmail>true</LogToEmail><LogToFile>false</LogToFile><NotifyAdminsForFeedback>true</NotifyAdminsForFeedback><PostCount>5</PostCount><PostsPerPage>3</PostsPerPage><RssShowLimitedEntryInFeed>false</RssShowLimitedEntryInFeed><RssUrl>http://feedburner.com/test</RssUrl><ShowAuthorsAbout>false</ShowAuthorsAbout><ShowBlogAbout>true</ShowBlogAbout><ShowGravatars>false</ShowGravatars><Skin>TEST</Skin><SmtpPassword>TEST</SmtpPassword><SmtpPort>23</SmtpPort><SmtpServer>google.com</SmtpServer><SmtpUserName>username</SmtpUserName><SmtpUseSsl>false</SmtpUseSsl><SubTitle>a subtitle</SubTitle><TimeZone>EASTERN</TimeZone><Title>THE TITLE</Title><TwitterUrl>http://twitter.com/abite</TwitterUrl><WebStatsJavascript>&amp;lt;script type=&amp;quot;text/javascript&amp;quot;&amp;gt;Web Stats Javascript!&amp;lt;/script</WebStatsJavascript></BlogConfig>",
                target.ConfigXml);
        }
    }
}
