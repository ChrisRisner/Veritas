using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Veritas.DataLayer.Models;
using System.Web;
using Veritas.BusinessLayer.Session;

namespace Veritas.BusinessLayer.Screens.Admin.Uploads
{
	public class UploadsIndexScreen : ScreenBase
	{
        public BlogMedia[] BlogMedias;

        public UploadsIndexScreen()
        {
            LoadScreen();
        }

        protected override void LoadScreen()
        {
            this.BlogMedias = repo.GetBlogMedias(this.blogConfig.BlogConfigId).ToArray();
        }

        public override bool IsValid
        {
            get { return true; }
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            throw new NotImplementedException();
        }

        public void SaveFile(System.Web.HttpPostedFileBase httpPostedFileBase)
        {
            int lastPeriod = httpPostedFileBase.FileName.LastIndexOf(".");
            string fileName = httpPostedFileBase.FileName;
            httpPostedFileBase.SaveAs(HttpContext.Current.Server.MapPath("~/Upload/") + fileName);

            BlogMedia media = new BlogMedia();
            media.BlogConfigId = this.blogConfig.BlogConfigId;
            media.FileName = fileName;
            media.FilePath = HttpContext.Current.Server.MapPath("/Upload/") + fileName;
            //info.url = "http://" + host + "/files/media/image/" + media.name;
            media.ServerPath = "http://" + this.blogConfig.Host + "/upload/" + fileName;
            media.CreateDate = DateTime.Now;
            media.CreatedById = SessionHandler.CurrentUserId;
            repo.Add(media);
            repo.Save();
        }
    }
}
