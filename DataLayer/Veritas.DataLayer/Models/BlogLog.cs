using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veritas.DataLayer.Models
{
    public partial class BlogLog
    {
        /// <summary>
        /// Overriddent to print out our Log details
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Event Level:  ");
            sb.AppendLine(this.EventLevel);
            sb.Append("Logger:  ");
            sb.AppendLine(this.Logger);
            sb.Append("Url:  ");
            sb.AppendLine(this.Url);
            sb.Append("Date:  ");
            sb.AppendLine(this.CreateDate.ToString());
            sb.Append("Message:  ");
            sb.AppendLine(this.Message);
            sb.Append("Exception Details:  ");
            sb.AppendLine(this.Exception);
            return sb.ToString();
        }
    }
}
