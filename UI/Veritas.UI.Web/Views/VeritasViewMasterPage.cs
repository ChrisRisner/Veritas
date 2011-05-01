using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Veritas.UI.Web.Views
{
    public class VeritasViewMasterPage<T> : ViewMasterPage<T> where T : class
    {
        public VeritasForm VeritasForm
        {
            get
            {
                return VeritasForm.GetInstance(Html, Url);
            }
        }
    }

    public class VeritasViewMasterPage : ViewMasterPage
    {
        public VeritasForm VeritasForm
        {
            get
            {
                return VeritasForm.GetInstance(Html, Url);
            }
        }
    }
}