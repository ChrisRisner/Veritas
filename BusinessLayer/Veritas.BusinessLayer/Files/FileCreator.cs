using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.BusinessLayer.Media;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using Veritas.DataLayer.Models;
using Veritas.DataLayer;
using Veritas.BusinessLayer.Caching;

namespace Veritas.BusinessLayer.Files
{
    public class FileCreator
    {
        public static MediaObjectInfo Create(MediaObject media, string username)
        {
            MediaObjectInfo info = new MediaObjectInfo();

            if (!Directory.Exists(HttpContext.Current.Server.MapPath("..") + "/files/media/image/WindowsLiveWriter"))
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("..") + "/files"))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("..") + "/files");
                }
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("..") + "/files/media"))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("..") + "/files/media");
                }
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("..") + "/files/media/image"))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("..") + "/files/media/image");
                }
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("..") + "/files/media/image/WindowsLiveWriter"))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("..") + "/files/media/image/WindowsLiveWriter");
                }
            }
            string[] pathParts = Regex.Split(media.name, "/");
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("..") + "/files/media/image/WindowsLiveWriter/" + pathParts[1]))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("..") + "/files/media/image/WindowsLiveWriter/" + pathParts[1]);
            }
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("..") + "/files/media/image/" + media.name, FileMode.Create, FileAccess.Write);
            fs.Write(media.bits, 0, media.bits.Length);
            fs.Close();

            string host = HttpContext.Current.Request.Url.Host;
            int port = HttpContext.Current.Request.Url.Port;
            if (port != 80)
            {
                host += ":" + port;
            }
            info.url = "http://" + host + "/files/media/image/" + media.name;

            //Save a media record for this file
            BlogMedia bMedia = new BlogMedia();
            bMedia.BlogConfigId = CacheHandler.BlogConfigId;
            VeritasRepository repo = VeritasRepository.GetInstance();
            bMedia.CreatedById = repo.GetBlogUserByUserName(CacheHandler.BlogConfigId, username).BlogUserId;
            bMedia.FileName = media.name;
            bMedia.FilePath = HttpContext.Current.Server.MapPath("..") + "/files/media/image/" + media.name;
            bMedia.ServerPath = info.url;
            repo.Add(bMedia);
            repo.Save();



            return info;
        }
    }
}
