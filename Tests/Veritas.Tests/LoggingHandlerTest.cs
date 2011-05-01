using Veritas.BusinessLayer.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using System.IO;

namespace Veritas.Tests
{
    
    
    /// <summary>
    ///This is a test class for LoggingHandlerTest and is intended
    ///to contain all LoggingHandlerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LoggingHandlerTest : TestBase
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
        ///A test for LogToDb
        ///</summary>
        [TestMethod()]
        public void LogToDbTest()
        {
            string message = "Exception Message"; 
            string details = "Exception Details"; 
            string level = "Error";
            string logger = "LogToDbTest";
            LoggingHandler.LogToDb(message, details, level, logger);

            var logs = repo.GetBlogLogs(TestBlogConfig.BlogConfigId).ToArray();
            Assert.IsNotNull(logs.Where(p => p.Message == "Exception Message").FirstOrDefault());
            Assert.IsNotNull(logs.Where(p => p.Exception == "Exception Details").FirstOrDefault());
            Assert.IsNotNull(logs.Where(p => p.EventLevel == "Error").FirstOrDefault());
            Assert.IsNotNull(logs.Where(p => p.Logger == "LogToDbTest").FirstOrDefault());
            
        }

        /// <summary>
        ///A test for LogToDb
        ///</summary>
        [TestMethod()]
        public void LogToDbExceptionTest()
        {
            Exception ex = new Exception("Test Exception", new Exception("Inner Test Exception"));
            LoggingHandler.LogToDb(ex, "LogToDbExceptionTest");

            var logs = repo.GetBlogLogs(TestBlogConfig.BlogConfigId).ToArray();
            Assert.IsTrue(logs.Length > 0);
            Assert.IsNotNull(logs.Where(p => p.Logger == "LogToDbExceptionTest"));                            
        }

        /// <summary>
        ///A test for LogToFile
        ///</summary>
        [TestMethod()]
        public void LogToFileExceptionTest()
        {
            Exception ex = new Exception("Test Exception", new Exception("Inner Test Exception"));
            string logger = "LogToFileExceptionTest";
            LoggingHandler.LogToFile(ex, logger);

            string fileText = File.ReadAllText(TestBlogConfig.LogFilePath);
            Assert.IsTrue(!string.IsNullOrEmpty(fileText));
            
        }

        /// <summary>
        ///A test for LogToFile
        ///</summary>
        [TestMethod()]
        public void LogToFileDetailsTest()
        {
            string message = "Exception Message";
            string details = "Exception Details";
            string level = "Error";
            string logger = "LogToDbTest";
            LoggingHandler.LogToFile(message, details, level, logger);
            string fileText = File.ReadAllText(TestBlogConfig.LogFilePath);
            Assert.IsTrue(!string.IsNullOrEmpty(fileText));
            
        }
    }
}
