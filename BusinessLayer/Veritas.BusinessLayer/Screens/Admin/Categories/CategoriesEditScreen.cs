using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;
using Veritas.BusinessLayer.Session;

namespace Veritas.BusinessLayer.Screens.Admin.Categories
{
    public class CategoriesEditScreen : ScreenBase
    {
        public int? BlogCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public CategoriesEditScreen()
        {
        }

        public CategoriesEditScreen(int categoryId)
        {
            if (categoryId > 0)
                LoadScreen(categoryId);
            else
                LoadScreen();
        }

        protected override void LoadScreen()
        {
            
        }

        protected void LoadScreen(int categoryId)
        {
            var category = repo.GetBlogCategoryByCategoryId(this.blogConfig.BlogConfigId, categoryId);
            this.Title = category.Title;
            this.Description = category.Description;
            this.IsActive = category.IsActive.HasValue ? category.IsActive.Value : false;
            this.BlogCategoryId = category.BlogCategoryId;
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(this.Title))
                items.Add("Title", "You must enter a title.");
            if (string.IsNullOrEmpty(this.Description))
                items.Add("Description", "You must enter a description.");

            return items;
        }

        public override bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(this.Title) && !string.IsNullOrEmpty(this.Description));
            }
        }

        public void SaveCategory()
        {
            BlogCategory category = null;

            //Create new audit record
            var newLog = new BlogLog()
            {
                BlogConfigId = this.blogConfig.BlogConfigId,
                CreateDate = DateTime.Now,
                EventLevel = "Audit",
                Logger = "CategoriesEditScreen"
            };

            if (this.BlogCategoryId.HasValue && this.BlogCategoryId > 0)
            {                
                category = repo.GetBlogCategoryByCategoryId(this.blogConfig.BlogConfigId, this.BlogCategoryId.Value);

                newLog.Message = "Changing Category with ID = " + this.BlogCategoryId.Value +
                    "\nOld Title: " + category.Title + " \nNew Title: " + this.Title +
                    "\nOld Description: " + category.Description + " \nNew Desccription: " + this.Description +
                    "\nOld Active: " + category.IsActive + " \nNew Active: " + this.IsActive +
                    "\nBy user: " + SessionHandler.CurrentUser.Username;


                category.Title = this.Title;
                category.Description = this.Description;
                category.IsActive = this.IsActive;
            }
            else
            {
                newLog.Message = "New Category "+
                    "\nNew Title: " + this.Title +
                    "\nNew Desccription: " + this.Description +
                    "\nNew Active: " + this.IsActive;

                category = new BlogCategory()
                {
                    BlogConfigId = this.blogConfig.BlogConfigId,
                    CreateDate = DateTime.Now,
                    CreatedById = SessionHandler.CurrentUserId,
                    Description = this.Description,
                    IsActive = this.IsActive,
                    Title = this.Title                
                };
                repo.Add(category);
            }

            category.LastUpdateDate = DateTime.Now;
            category.LastUpdatedById = SessionHandler.CurrentUserId;
            repo.Add(newLog);
            
            repo.Save();
        }
    }
}
