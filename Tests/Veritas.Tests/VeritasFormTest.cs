using Veritas.UI.Web.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Web.Routing;

namespace Veritas.Tests
{
    
    
    /// <summary>
    ///This is a test class for VeritasFormTest and is intended
    ///to contain all VeritasFormTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VeritasFormTest
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
        ///A test for GetInstance
        ///</summary>

        [TestMethod()]
        public void GetInstanceTest()
        {
            var htmlHelper = TestHelper.GetTestHtmlHelper();
            var urlHelper = TestHelper.GetTestUrlHelper();
            Assert.IsNotNull(htmlHelper);            
            var form= VeritasForm.GetInstance(htmlHelper, urlHelper);
            Assert.IsNotNull(form);
        }

        /// <summary>
        ///A test for Helper
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void HtmlHelperTest()
        {
            var htmlHelper = TestHelper.GetTestHtmlHelper();
            Assert.IsNotNull(htmlHelper);           
        }

        [TestMethod()]
        public void UrlHelperTest()
        {
            var urlHelper = TestHelper.GetTestUrlHelper();
            Assert.IsNotNull(urlHelper);
        }
    }
}
