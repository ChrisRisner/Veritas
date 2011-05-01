using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Veritas.BusinessLayer.Authentication
{
    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
