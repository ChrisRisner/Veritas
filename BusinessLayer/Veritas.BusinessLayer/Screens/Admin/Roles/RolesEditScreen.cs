using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Screens.Admin.Roles
{
    public class RolesEditScreen : ScreenBase
    {
        public BlogRole BlogRole { get; set; }

        public RolesEditScreen(int blogRoleId)
        {
            LoadScreen(blogRoleId);
        }

        private void LoadScreen(int blogRoleId)
        {
            this.BlogRole = repo.GetBlogRoleByRoleId(this.blogConfig.BlogConfigId, blogRoleId);
        }

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

        public void SaveRole()
        {
            throw new NotImplementedException();
        }
    }
}
