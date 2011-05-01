using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Web.Routing;
using System.IO;

namespace Veritas.Tests
{
    public static class TestHelper
    {
        public static HtmlHelper GetTestHtmlHelper()
        {
            ViewDataDictionary vdd = new ViewDataDictionary();
            var mockViewContext = new Mock<ViewContext>(
                new ControllerContext(
                    new Mock<HttpContextBase>().Object,
                    new RouteData(),
                    new Mock<ControllerBase>().Object),
                  new Mock<IView>().Object,
                  vdd,
                new TempDataDictionary(), new StringWriter());

            var mockViewDataContainer = new Mock<IViewDataContainer>();
            mockViewDataContainer.Setup(v => v.ViewData).Returns(vdd);
            return new HtmlHelper(mockViewContext.Object, mockViewDataContainer.Object);
        }

        public static UrlHelper GetTestUrlHelper()
        {           
            var mockRequestContext = new Mock<RequestContext>();

            return new UrlHelper(mockRequestContext.Object);
        }
    }
}
