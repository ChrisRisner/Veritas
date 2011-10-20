using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veritas.BusinessLayer.Screens.Home;
using Veritas.BusinessLayer.Screens.Blog;
using Veritas.UI.Web.Views;
using Veritas.BusinessLayer;

namespace Veritas.UI.Web.Controllers
{
    public class BlogController : ControllerBase
    {
        public ActionResult Index(string startAt)
        {
            IndexScreen screen = new IndexScreen(startAt);
            ViewData.Model = screen;
            return View();
        }        

        [AcceptVerbs("GET")]
        public ActionResult Category(string id, string startAt)
        {
            CategoryScreen screen = new CategoryScreen(id, startAt);
            ViewData.Model = screen;
            return View();
        }

        [AcceptVerbs("GET")]
        public ActionResult Archive(string id)
        {
            id = EntryTitleLogic.GetEntryNameFromTitle(id);
            ArchiveScreen screen = new ArchiveScreen(id);
            ViewData.Model = screen;
            
            //The id didn't produce a valid blog entry
            if (screen.BlogEntryScreen.BlogEntry == null)
                return  RedirectToAction("NotFound", "Error");
            
            screen.UpdateEntryViewCount();

            return View();
        }

        [ValidateInput(false)]
        [AcceptVerbs("POST")]
        //public ActionResult Archive(string EntryName, string iEntryID, string Name, string Email, string WebSite, string Message, bool NotifyMeOnFeedback)
        public ActionResult Archive(string id, FormCollection collection)
        {
            ArchiveScreen screen = new ArchiveScreen(id);
            TryUpdateModel(screen);

            screen.HandleUserInput();
            if (screen.IsSpam)
            {
                screen.LogSpam();
            }
            else if (screen.IsValid)
            {
                ModelState.Clear();
                screen.SaveAndProcessFeedback();

                //reload data
                screen = new ArchiveScreen(id);
                return Redirect(Url.Content("~/" + screen.BlogEntryScreen.BlogEntry.EntryName));
            }
            else
            {
                screen.Message = "Oops - there was a problem.";
                //Add our validation errors
                foreach (var item in screen.GetValidationErrors())
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
            }
          
            ViewData.Model = screen;
            return View();
        }
    }
}
