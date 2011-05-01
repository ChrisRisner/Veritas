using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer;
using Veritas.DataLayer.Models;
using Veritas.BusinessLayer.Caching;

namespace Veritas.BusinessLayer.Screens
{
    public abstract class ScreenBase
    {
        protected VeritasRepository repo = VeritasRepository.GetInstance();
        public string Message { get; set; }
        

        public ScreenBase()  {  }

        public BlogConfig blogConfig
        {
            get { return CacheHandler.GetBlogConfig(); }
        }

        public BlogMenuItem[] blogMenuItems
        {
            get { return CacheHandler.GetBlogMenuItems(); }
        }

        public BlogCategoryTag[] blogCategoryTags
        {
            get { return CacheHandler.GetBlogCategoryTags(); }
        }

        /// <summary>
        /// Determines if the entites associated with the view 
        /// are valid or not.
        /// </summary>
        public abstract bool IsValid { get; }

        /// <summary>
        /// Loads up whatever entities this screen may need.
        /// </summary>
        protected abstract void LoadScreen();

        /// <summary>
        /// When overridden by child classes, will return a dictionary of validation errors where the key is the 
        /// control ID and the value is the error message.
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, string> GetValidationErrors();
    }
}
