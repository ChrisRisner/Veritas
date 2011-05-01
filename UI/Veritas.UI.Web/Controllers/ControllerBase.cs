using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veritas.DataLayer;
using Veritas.BusinessLayer.Caching;
using Veritas.BusinessLayer.Authentication;
using Veritas.BusinessLayer.Logging;

namespace Veritas.UI.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected VeritasRepository repo = VeritasRepository.GetInstance();

        public IFormsAuthentication FormsAuth
        {
            get;
            set;
        }

        public IMembershipService MembershipService
        {
            get;
            set;
        }

        protected bool ValidateLogOn(string userName, string password)
        {
            bool loginFailed = false;
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
                loginFailed = true;
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
                loginFailed = true;
            }
            
            if (!MembershipService.ValidateUser(userName, password))
            {
                ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
                loginFailed = true;
            }
            if (loginFailed)
            {                
                LoggingHandler.LoginFail(userName);
            }

            return ModelState.IsValid;
        }

        public ControllerBase() : this(new FormsAuthenticationService(), new AccountMembershipService())
        {            
        }

        public ControllerBase(IFormsAuthentication formsAuth, IMembershipService membershipService)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationService();
            MembershipService = membershipService ?? new AccountMembershipService();        
        }

        //protected override void HandleUnknownAction(string actionName)
        //{
        //    Response.StatusCode = 404;
        //    View("~/Views/Home/404.aspx").ExecuteResult(this.ControllerContext);
        //}
    }    
}