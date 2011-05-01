using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Veritas.BusinessLayer
{
    public static class GravatarHandler
    {
        public static string GetGravatarURL(string emailAddress)
        {
            string imageUrl = "http://www.gravatar.com/avatar.php?";
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            byte[] hashedBytes = md5.ComputeHash(encoder.GetBytes(emailAddress));

            StringBuilder sb = new StringBuilder(hashedBytes.Length * 2);
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                sb.Append(hashedBytes[i].ToString("X2").ToLower());
            }

            imageUrl += "gravatar_id=" + sb.ToString();
            imageUrl += "&size=50";
            return imageUrl;
        }
    }
}
