using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.DataLayer.Models;

namespace Veritas.BusinessLayer.Screens.Admin.Logs
{
    public class LogsIndexScreen : ScreenBase
    {
        public BlogLog[] BlogLogs { get; set; }

        public LogsIndexScreen()
        {
            this.BlogLogs = repo.GetBlogLogs(this.blogConfig.BlogConfigId).OrderByDescending(p => p.BlogLogId).ToArray();
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
    }
}
