using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Joel.Net;
using Veritas.BusinessLayer.Caching;

namespace Veritas.BusinessLayer.Spam
{
    internal class SpamHandler
    {
        internal static bool CheckCommentForSpamUsingAkismet(HttpRequest request, string name, 
                        string email, string webSite, string feedbackText)
        {
            //Test spam username: viagra-test-123
            Akismet akismetAPI = new Akismet(CacheHandler.GetBlogConfig().AkismetApiKey, 
                "http://www." + CacheHandler.GetBlogConfig().Host, "Test/1.0");
            if (!akismetAPI.VerifyKey())
            {
                throw new Exception("Akismet key could not be verified.");
            }
            AkismetComment comment = new AkismetComment();
            comment.Blog = "http://www." + CacheHandler.GetBlogConfig().Host;
            comment.UserIp = request.UserHostAddress;
            comment.UserAgent = request.UserAgent;
            comment.CommentContent = feedbackText;
            comment.CommentType = "comment";
            comment.CommentAuthor = name;
            comment.CommentAuthorEmail = email;
            comment.CommentAuthorUrl = webSite;

            bool spamCheck = akismetAPI.CommentCheck(comment);
            return spamCheck;
        }
    }
}
