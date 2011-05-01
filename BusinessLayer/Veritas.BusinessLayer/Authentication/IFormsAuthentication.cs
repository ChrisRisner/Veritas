using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veritas.BusinessLayer.Authentication
{
    public interface IFormsAuthentication
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }
}
