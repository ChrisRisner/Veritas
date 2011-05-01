using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Veritas.BusinessLayer.Caching;

namespace Veritas.BusinessLayer.Email
{
    public class EmailHandler
    {
        public static void SendEmail(string fromAddress, string body, string toAddresses, string subject, bool isHtml)
        {
            SendEmail(fromAddress, body, toAddresses, null, null, subject, isHtml);
        }

        public static void SendEmail(string fromAddress, string body, string toAddresses, string ccAddresses, string bccAddresses, string subject, bool isHtml)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(fromAddress);
            //msg.To = new MailAddressCollection();
            foreach (string address in toAddresses.Split(';'))
                msg.To.Add(new MailAddress(address));
            if (!string.IsNullOrEmpty(ccAddresses))
                foreach (string address in ccAddresses.Split(';'))
                    msg.CC.Add(new MailAddress(address));
            if (!string.IsNullOrEmpty(bccAddresses))
                foreach (string address in bccAddresses.Split(';'))
                    msg.Bcc.Add(new MailAddress(address));
            msg.Body = body;
            msg.Subject = subject;
            msg.IsBodyHtml = isHtml;
            SmtpClient client = GetSmtpClient();
            client.Send(msg);
        }

        private static SmtpClient GetSmtpClient()
        {
            SmtpClient client = new SmtpClient(CacheHandler.GetBlogConfig().SmtpServer);
            if (!string.IsNullOrEmpty(CacheHandler.GetBlogConfig().SmtpUserName) &&
                !string.IsNullOrEmpty(CacheHandler.GetBlogConfig().SmtpPassword))
            {
                client.Credentials = new System.Net.NetworkCredential(CacheHandler.GetBlogConfig().SmtpUserName,
                                                                      CacheHandler.GetBlogConfig().SmtpPassword);
            }
            if (CacheHandler.GetBlogConfig().SmtpUseSsl.HasValue)
                client.EnableSsl = CacheHandler.GetBlogConfig().SmtpUseSsl.Value;
            if (CacheHandler.GetBlogConfig().SmtpPort.HasValue)
                client.Port = CacheHandler.GetBlogConfig().SmtpPort.Value;
            return client;
        }
    }
}



