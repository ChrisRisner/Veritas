using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Screens.Home
{
    public class UploadScreen : ScreenBase
    {
        public string Question1 { get; set; }
        public string Question2 { get; set; }
        public string Question3 { get; set; }

        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }

        public string SourceAnswer1 { get; set; }
        public string SourceAnswer2 { get; set; }
        public string SourceAnswer3 { get; set; }

        public UploadScreen()
        {
            LoadScreen();
        }

        protected override void LoadScreen()
        {
            this.Question1 = this.blogConfig.SecurityQuestionOne;
            this.Question2 = this.blogConfig.SecurityQuestionTwo;
            this.Question3 = this.blogConfig.SecurityQuestionThree;
            this.SourceAnswer1 = this.blogConfig.SecurityQuestionAnswerOne;
            this.SourceAnswer2 = this.blogConfig.SecurityQuestionAnswerTwo;
            this.SourceAnswer3 = this.blogConfig.SecurityQuestionAnswerThree;
        }

        public override bool IsValid
        {
            get { return true; }
        }

        public override Dictionary<string, string> GetValidationErrors()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves our upload file, returns the web path
        /// </summary>
        /// <param name="httpPostedFileBase"></param>
        /// <returns></returns>
        public string SaveFile(System.Web.HttpPostedFileBase httpPostedFileBase)
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
            repo.Add(media);
            repo.Save();

            return media.ServerPath;
        }

        public bool AnswersAreValid
        {
            get
            {
                return ((this.Answer1 == this.SourceAnswer1) &&
                        (this.Answer2 == this.SourceAnswer2) &&
                        (this.Answer3 == this.SourceAnswer3));
            }
        }

        public Dictionary<string,string> GetAnswerValidationErrors()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();

            if ((this.Answer1 != this.SourceAnswer1) ||
                (this.Answer2 != this.SourceAnswer2) ||
                (this.Answer3 != this.SourceAnswer3))
                items.Add("Answers", "Your answers are incorrect.  You fail.");

            return items;
                
        }
    }
}
