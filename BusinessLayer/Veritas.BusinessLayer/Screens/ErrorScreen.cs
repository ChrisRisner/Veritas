using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veritas.BusinessLayer.Screens
{
    public class ErrorScreen : ScreenBase
    {
        protected override void LoadScreen()
        {
            throw new NotImplementedException();
        }

        public override bool IsValid
        {
            get {throw new NotImplementedException(); }
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            throw new NotImplementedException();
        }
    }
}
