using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veritas.BusinessLayer.Screens.Admin;
using Veritas.BusinessLayer.Caching;
using Veritas.BusinessLayer.Session;
using Veritas.BusinessLayer.Screens.Admin.Categories;
using Veritas.BusinessLayer.Screens.Admin.Uploads;
using Veritas.BusinessLayer.Logging;
using Veritas.BusinessLayer.Screens.Admin.Feedbacks;
using Veritas.BusinessLayer.Screens.Admin.FeedbackAuthors;
using Veritas.BusinessLayer.Screens.Admin.Logs;
using Veritas.BusinessLayer.Screens.Admin.Pages;
using Veritas.BusinessLayer.Screens.Admin.Entries;
using Veritas.BusinessLayer.Screens.Admin.Settings;
using Veritas.BusinessLayer.Screens.Admin.Roles;
using Veritas.BusinessLayer.Screens.Admin.Users;

namespace Veritas.UI.Web.Controllers
{
    public class AdminController : ControllerBase
    {
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            IndexScreen screen = new IndexScreen();
            ViewData.Model = screen;
            return View();
        }

        #region LogOn/Off

        public ActionResult LogOn()
        {
            LogOnScreen screen = new LogOnScreen();
            ViewData.Model = screen;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl)
        {

            if (!ValidateLogOn(userName, password))
            {
                LogOnScreen screen = new LogOnScreen();
                ViewData.Model = screen;
                return View();
            }

            FormsAuth.SignIn(userName, rememberMe);

            bool isRealUser = repo.ValidateUser(CacheHandler.BlogConfigId, userName, password);

            var user = repo.GetBlogUserByUserName(CacheHandler.BlogConfigId, userName);

            
            SessionHandler.CurrentUser = user;
            SessionHandler.CurrentUserId = user.BlogUserId;

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        public ActionResult LogOff()
        {
            FormsAuth.SignOut();

            return RedirectToAction("Index", "Home");
        }

        #endregion

        protected override void HandleUnknownAction(string actionName)
        {
            Response.StatusCode = 404;
            View("~/Views/Home/404.aspx").ExecuteResult(this.ControllerContext);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Categories(string id, int? categoryId)
        {            
            if (!string.IsNullOrEmpty(id))
            {
                if (id.ToLower() == "edit")
                {
                    CategoriesEditScreen screen = new CategoriesEditScreen(categoryId.HasValue ? categoryId.Value : 0);
                    TryUpdateModel(screen);
                    ViewData.Model = screen;
                    return View("Categories/Edit");
                }
                else if (id.ToLower() == "save")
                {
                    CategoriesEditScreen screen = new CategoriesEditScreen();
                    TryUpdateModel(screen);
                    if (screen.IsValid)
                    {
                        screen.SaveCategory();
                        return RedirectToAction("Categories/Index", "Admin");
                    }

                    ViewData.Model = screen;
                    //Add our validation errors
                    foreach (var item in screen.GetValidationErrors())
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }
                    return View("Categories/Edit");
                }
                else if (id.ToLower() == "delete")
                {
                }
            }

            ViewData.Model = new CategoriesIndexScreen();
            return View("Categories/Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Uploads()
        {
            UploadsIndexScreen screen = new UploadsIndexScreen();
            ViewData.Model = screen;

            return View("Uploads/Index");
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Uploads(string id)
        {
            UploadsIndexScreen screen = new UploadsIndexScreen();

            if (id.ToLower() == "save")
            {
                TryUpdateModel(screen);
                //User didn't select a file
                if (base.Request.Files.Count != 1 || string.IsNullOrEmpty(base.Request.Files[0].FileName))
                {
                    ModelState.AddModelError("fileUpload", "Please choose a file to upload.");
                    ViewData.Model = screen;
                    return View("Uploads/Index");
                }

                string fileName = base.Request.Files[0].FileName;
                if (System.IO.File.Exists(base.Server.MapPath("/Upload/") + fileName))
                {
                    LoggingHandler.Log("Upload file failed due to name", "Attempt to upload file that already exists with name " + fileName, "Error", "Home/Upload");
                    ModelState.AddModelError("fileUpload", "A file with that name already exists.  Please rename and try again.");
                    ViewData.Model = screen;
                    return View("Uploads/Index");
                }

                screen.SaveFile(Request.Files[0]);

                return RedirectToAction("Uploads");
            }

            ViewData.Model = screen;
            return View("Uploads/Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Feedbacks(string id, int? feedbackId, string type)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (id.ToLower() == "edit")
                {
                    FeedbacksEditScreen screen = new FeedbacksEditScreen(feedbackId.HasValue ? feedbackId.Value : 0);
                    TryUpdateModel(screen);
                    ViewData.Model = screen;
                    return View("Feedbacks/Edit");
                }
                else if (id.ToLower() == "save")
                {
                    FeedbacksEditScreen screen = new FeedbacksEditScreen(feedbackId.HasValue ? feedbackId.Value : 0);
                    //CategoriesEditScreen screen = new CategoriesEditScreen();
                    TryUpdateModel(screen);

                    screen.BlogFeedback.Status = Convert.ToInt32(Request.Form["StatusSelectList"]);
                    if (screen.IsValid)
                    {
                        screen.SaveFeedback();
                        return RedirectToAction("Feedbacks/Index", new { type = type });
                    }

                    ViewData.Model = screen;
                    //Add our validation errors
                    foreach (var item in screen.GetValidationErrors())
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }
                    return RedirectToAction("Feedbacks/Edit", new { type = type });
                }
                else if (id.ToLower() == "approve")
                {
                    if (feedbackId.HasValue)
                    {
                        FeedbacksEditScreen editScreen = new FeedbacksEditScreen(feedbackId.Value);
                        editScreen.ApproveFeedback();                        
                    }
                    return RedirectToAction("Feedbacks/Index", new { type = type });
                    
                }
                else if (id.ToLower() == "deny")
                {
                    if (feedbackId.HasValue)
                    {
                        FeedbacksEditScreen editScreen = new FeedbacksEditScreen(feedbackId.Value);
                        editScreen.DenyFeedback();
                    }
                    return RedirectToAction("Feedbacks/Index", new { type = type });
                }
            }

            ViewData.Model = new FeedbacksIndexScreen(type);
            return View("Feedbacks/Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult FeedbackAuthors(string id, int? feedbackAuthorId)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (id.ToLower() == "details")
                {
                    FeedbackAuthorsDetailsScreen screen = new FeedbackAuthorsDetailsScreen(feedbackAuthorId.HasValue ? feedbackAuthorId.Value : 0);                    
                    ViewData.Model = screen;
                    return View("FeedbackAuthors/Details");
                }                
            }

            ViewData.Model = new FeedbackAuthorsIndexScreen();
            return View("FeedbackAuthors/Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Logs()
        {
            LogsIndexScreen screen = new LogsIndexScreen();
            ViewData.Model = screen;
            return View("Logs/Index");
        }

        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult Pages(string id, int? pageId)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (id.ToLower() == "edit")
                {
                    PagesEditScreen screen = new PagesEditScreen(pageId.HasValue ? pageId.Value : 0);
                    ViewData.Model = screen;
                    return View("Pages/Edit");
                    
                }
                else if (id.ToLower() == "save")
                {
                    PagesEditScreen screen = new PagesEditScreen(pageId.HasValue ? pageId.Value : 0);
                    TryUpdateModel(screen);
                    screen.CheckAndUpdateEncodedTitle();

                    if (screen.IsValid)
                    {
                        screen.SavePage();
                        return RedirectToAction("Pages/Index");
                    }
                    ViewData.Model = screen;
                    //Add our validation errors
                    foreach (var item in screen.GetValidationErrors())
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }
                    return View("Pages/Edit");

                }
            }

            ViewData.Model = new PagesIndexScreen();
            return View("Pages/Index");
        }

        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult Entries(string id, int? entryId)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (id.ToLower() == "edit")
                {
                    EntriesEditScreen screen = new EntriesEditScreen(entryId.HasValue ? entryId.Value : 0);
                    ViewData.Model = screen;
                    return View("Entries/Edit");
                }
                else if (id.ToLower() == "save")
                {
                    
                    EntriesEditScreen screen = new EntriesEditScreen(entryId.HasValue ? entryId.Value : 0);
                    TryUpdateModel(screen);
                    screen.BlogEntry.PostType = Convert.ToInt32(Request.Form["PostTypeSelectList"]);
                    screen.BlogEntry.PreviousEntryInSeries = Convert.ToInt64(Request.Form["PreviousEntryInSeriesSelectList"]);
                    screen.BlogEntry.NextEntryInSeries = Convert.ToInt64(Request.Form["NextEntryInSeriesSelectList"]);

                    screen.CheckAndUpdateEntryTitle();

                    if (screen.IsValid)
                    {
                        screen.SaveEntry();
                        return RedirectToAction("Entries/Index");
                    }
                    ViewData.Model = screen;
                    //Add our validation errors
                    foreach (var item in screen.GetValidationErrors())
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }
                    return View("Entries/Edit");
                    
                }
            }

            ViewData.Model = new EntriesIndexScreen();
            return View("Entries/Index");
        }

        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult Settings(string id, string subSettingName)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (id.ToLower() == "edit")
                {
                    SettingsEditScreen screen = new SettingsEditScreen();
                    ViewData.Model = screen;
                    if (subSettingName == "Feedback")
                    {
                        return View("Settings/FeedbackEdit");
                    }
                    else if (subSettingName == "Spam")
                    {
                        return View("Settings/SpamEdit");
                    }
                    else if (subSettingName == "General")
                    {
                        return View("Settings/GeneralEdit");
                    }
                    else if (subSettingName == "Marketing")
                    {
                        return View("Settings/MarketingEdit");
                    }
                    else if (subSettingName == "SyndicationAndPublishing")
                    {
                        return View("Settings/SyndicationAndPublishingEdit");
                    }
                    else if (subSettingName == "Logging")
                    {
                        return View("Settings/LoggingEdit");
                    }
                    else if (subSettingName == "Security")
                    {
                        return View("Settings/SecurityEdit");
                    }
                    else if (subSettingName == "Email")
                    {
                        return View("Settings/EmailEdit");
                    }
                }
                else if (id.ToLower() == "save")
                {
                    SettingsEditScreen screen = new SettingsEditScreen();
                    TryUpdateModel(screen);
                    //screen.CheckAndUpdateEncodedTitle();

                    if (screen.IsValid)
                    {
                        screen.SaveConfig();
                        return RedirectToAction("Settings/Index");
                    }
                    ViewData.Model = screen;
                    //Add our validation errors
                    foreach (var item in screen.GetValidationErrors())
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }
                    return View("Settings/" + subSettingName + "Edit");

                }
            }

            ViewData.Model = new SettingsIndexScreen();
            return View("Settings/Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Roles(string id, int? roleId)
        {
            //if (!string.IsNullOrEmpty(id))
            //{
            //    if (id.ToLower() == "edit")
            //    {
            //        RolesEditScreen screen = new RolesEditScreen(roleId.HasValue ? roleId.Value : 0);
            //        ViewData.Model = screen;
            //        return View("Roles/Edit");

            //    }
            //}

            ViewData.Model = new RolesIndexScreen();
            return View("Roles/Index");
        }

        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        public ActionResult Users(string id, int? userId)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (id.ToLower() == "edit")
                {
                    UsersEditScreen screen = new UsersEditScreen(userId.HasValue ? userId.Value : 0);
                    ViewData.Model = screen;
                    return View("Users/Edit");

                }
                else if (id.ToLower() == "save")
                {
                    UsersEditScreen screen = new UsersEditScreen(userId.HasValue ? userId.Value : 0);
                    TryUpdateModel(screen);

                    if (screen.IsValid)
                    {
                        screen.SaveUser();
                        return RedirectToAction("Users/Index");
                    }
                    ViewData.Model = screen;
                    //Add our validation errors
                    foreach (var item in screen.GetValidationErrors())
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }
                    return View("Users/Edit");

                }
            }

            ViewData.Model = new UsersIndexScreen();
            return View("Users/Index");
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ResetCache()
        {
            CacheHandler.ResetCache();

            IndexScreen screen = new IndexScreen();
            screen.Message = "The cache has been reset.";

            ViewData.Model = screen;
            return View("Index");

        }


    }
}
