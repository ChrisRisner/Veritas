using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Screens.Admin.Roles
{
    public class RolesIndexScreen : ScreenBase
    {
        public BlogRole[] BlogRoles { get; set; }

        public RolesIndexScreen()
        {
            LoadScreen();
        }

        protected override void LoadScreen()
        {
            this.BlogRoles = repo.GetBlogRoles(this.blogConfig.BlogConfigId).ToArray();
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
