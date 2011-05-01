using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Screens.Admin.Categories
{
    public class CategoriesIndexScreen : ScreenBase
    {
        public BlogCategory[] BlogCategories;

        //New Category
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public CategoriesIndexScreen()
        {
            this.LoadScreen();
        }

        protected override void LoadScreen()
        {
            this.BlogCategories = repo.GetBlogCategories(this.blogConfig.BlogConfigId).ToArray();
            this.IsActive = true;
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            throw new NotImplementedException();
        }

        public override bool IsValid
        {
            get { return true; }
        }
    }
}
