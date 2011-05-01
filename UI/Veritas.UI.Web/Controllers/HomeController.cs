using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veritas.BusinessLayer.Screens.Home;
using Veritas.BusinessLayer.Email;
using Veritas.BusinessLayer.Logging;

namespace Veritas.UI.Web.Controllers
{
    public class HomeController : ControllerBase
    {

        public ActionResult About()
        {
            AboutScreen screen = new AboutScreen();
            ViewData.Model = screen;
            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Contact()
        {
            ContactScreen screen = new ContactScreen();
            ViewData.Model = screen;
        return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Contact(ContactScreen screen)
        {
            screen.SendToUsername = Request.Form["AuthorSelectList"];

            if (screen.IsValid)
            {
                screen.ProcessContactRequest();
                screen.Message = "Your message has been sent.  Thank you.";
                ModelState.Clear();
            }
            else
            {
                //Add our validation errors
                foreach (var item in screen.GetValidationErrors())
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
            }

            ViewData.Model = screen;
            return View();
        }

        public ActionResult Index(string startAt)
        {
            IndexScreen screen = new IndexScreen(startAt);
            ViewData.Model = screen;
            return View();
        }

        public ActionResult Upload()
        {
            UploadScreen screen = new UploadScreen();
            ViewData.Model = screen;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload(FormCollection col)
        {
            UploadScreen screen = new UploadScreen();
            TryUpdateModel(screen);

            if (!screen.AnswersAreValid)
            {
                ViewData.Model = screen;

                foreach (var item in screen.GetAnswerValidationErrors())
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }

                return View();
            }
            ModelState.Clear();
            
            //User didn't select a file
            if (base.Request.Files.Count != 1 || string.IsNullOrEmpty(base.Request.Files[0].FileName))
            {
                ModelState.AddModelError("fileUpload", "Please choose a file to upload.");
                ViewData.Model = screen;
                return View();
            }

            string fileName = base.Request.Files[0].FileName;
            if (System.IO.File.Exists(base.Server.MapPath("/Upload/") + fileName))
            {
                LoggingHandler.Log("Upload file failed due to name", "Attempt to upload file that already exists with name " + fileName, "Error", "Home/Upload");
                ModelState.AddModelError("fileUpload", "A file with that name already exists.  Please rename and try again.");
                ViewData.Model = screen;
                return View();
            }

            string webPath = screen.SaveFile(Request.Files[0]);

            MessageScreen messageScreen = new MessageScreen();
            messageScreen.Message = "Upload successful, web path = " + webPath;
            ViewData.Model = messageScreen;
            return View("Message");
        }

        public ActionResult ViewContent(string id)
        {
            ViewContentScreen screen = new ViewContentScreen(id);
            ViewData.Model = screen;

            return View();
        }

        public ActionResult OptOut(string id)
        {
            OptOutHandler.ProcessOptOut(id);
            MessageScreen screen = new MessageScreen();
            screen.Message = "Your address has been opted out.";
            ViewData.Model = screen;

            return View("Message");
        }
    }
}

