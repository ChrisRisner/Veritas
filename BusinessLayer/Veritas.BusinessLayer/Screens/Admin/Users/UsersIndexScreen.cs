using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Screens.Admin.Users
{
    public class UsersIndexScreen : ScreenBase
    {
        public BlogUser[] BlogUsers { get; set; }

        public UsersIndexScreen()
        {
            LoadScreen();
        }

        protected override void LoadScreen()
        {
            this.BlogUsers = repo.GetBlogUsers(this.blogConfig.BlogConfigId).ToArray();
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
