using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Veritas.BusinessLayer.Logging;
using Veritas.UI.Web.Controllers;
using System.Configuration;

namespace Veritas.UI.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("Syndication/{*path}");
            routes.IgnoreRoute("Access/{*path}");

            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);


            routes.MapRoute(
                "Default",                                              // Route name
                "",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
            routes.MapRoute(
                "Home",                                              // Route name
                "Home/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
            routes.MapRoute(
                "Admin",                                              // Route name
                "Admin/{action}/{id}",                           // URL with parameters
                new { controller = "Admin", action = "Index", id = "" }  // Parameter defaults
            );
            routes.MapRoute(
                "Blog",                                              // Route name
                "Blog/{action}/{id}",                           // URL with parameters
                new { controller = "Blog", action = "Index", id = "" }  // Parameter defaults
            );
            routes.MapRoute(
                "Forum",                                              // Route name
                "Forum/{action}/{id}",                           // URL with parameters
                new { controller = "Forum", action = "Index", id = "" }  // Parameter defaults
            );
            routes.MapRoute(
                "BlogTest",                                              // Route name
                "{id}",                           // URL with parameters
                new { controller = "Blog", action = "Archive", id = "" }  // Parameter defaults
            );
            routes.MapRoute(
                "Category",                                               //Route Name
                "Category/{id}",                                          //Url with parameters
                new { controller = "blog", action = "category", id = "" } //parameter defaults
                );
            //routes.MapRoute(
            //    "Error",
            //    "{*url}",
            //    new { controller = "Home", action = "PageNotFound", id = "" }
            //);
            routes.MapRoute(
                "Error",
                "Error/{action}/{id}",
                new { controller = "Error", action = "Index", id = "" }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            HttpException httpException = exception as HttpException;
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            if (httpException == null)
            {
                LoggingHandler.Log(exception, "Global-Error");
                routeData.Values.Add("action", "InternalServerError");
            }
            else
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:      // Page not found.
                        LoggingHandler.Log(exception, "Global-Error", "Warn");
                        routeData.Values.Add("action", "NotFound");
                        break;
                    case 500:     // Server error.                               
                        LoggingHandler.Log(exception, "Global-Error");
                        routeData.Values.Add("action", "InternalServerError");
                        break;
                    default: //Handle any other errors the same as a 500                        
                        LoggingHandler.Log(exception, "Global-Error");
                        routeData.Values.Add("action", "InternalServerError");
                        break;
                }
            }
            // Pass exception details to the target error View. 
            routeData.Values.Add("error", exception);
            // Clear the error on server. 
            Server.ClearError();
            // Call target ErrorController and pass the routeData. 
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            string fullOriginalPath = Request.Url.ToString();
            string environment = ConfigurationManager.AppSettings["environment"];
            //Here we're checking to make sure all requests begin with http://www for SEO purposes
            if (environment == "prod")
            {
                if (fullOriginalPath.StartsWith("http://"))
                {
                    if (fullOriginalPath.StartsWith("http://www."))
                    {
                        fullOriginalPath = fullOriginalPath.Replace("http://www.", "http://");
                        Response.Redirect(fullOriginalPath);
                        return;
                    }
                }
                else if (fullOriginalPath.StartsWith("https://"))
                {
                    if (fullOriginalPath.StartsWith("https://www."))
                    {
                        fullOriginalPath = fullOriginalPath.Replace("https://www.", "https://");
                        Response.Redirect(fullOriginalPath);
                        return;
                    }
                }
            }
        }
    }
}