using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Net;
using Veritas.BusinessLayer.Screens;

namespace Veritas.UI.Web.Controllers
{
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ErrorController()
        {
        }

        /// <summary>
        /// Internal server error
        /// </summary>
        /// <returns></returns>
        public ActionResult InternalServerError()
        {
            ErrorScreen screen = new ErrorScreen();
            ViewData.Model = screen;
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View("Error");
        }

        /// <summary>
        /// Session expired
        /// </summary>
        /// <returns></returns>
        public ActionResult SessionExpired()
        {
            ErrorScreen screen = new ErrorScreen();
            ViewData.Model = screen;
            return View("SessionExpired");
        }

        /// <summary>
        /// Not found
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            ErrorScreen screen = new ErrorScreen();
            ViewData.Model = screen;
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View("404");
        }

    }

    public class UnHandledErrorInfo
    {
        public Exception Exception
        { get; set; }
    }
}
