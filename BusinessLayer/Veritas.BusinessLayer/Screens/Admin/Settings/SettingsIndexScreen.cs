﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veritas.BusinessLayer.Screens.Admin.Settings
{
    public class SettingsIndexScreen : ScreenBase
    {
        protected override void LoadScreen()
        {
            throw new NotImplementedException();
        }

        public override bool IsValid
        {
            get { return true; }
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            throw new NotImplementedException();
        }
    }
}
