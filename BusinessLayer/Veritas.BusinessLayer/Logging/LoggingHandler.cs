using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veritas.BusinessLayer.Caching;
using Veritas.DataLayer.Models;
using System.Web;
using Veritas.DataLayer;
using System.IO;
using Veritas.BusinessLayer.Email;

namespace Veritas.BusinessLayer.Logging
{
    public static class LoggingHandler
    {

        public static bool IsEmailLoggingEnabled 
        {
            get
            {
                return CacheHandler.GetBlogConfig().LogToEmail;
            }
        }
        public static bool IsFileLoggingEnabled 
        {
            get
            {
                return CacheHandler.GetBlogConfig().LogToFile;
            }
        }
        public static bool IsDbLoggingEnabled
        {
            get
            {
                return CacheHandler.GetBlogConfig().LogToDb;
            }
        }

        public static void Log(string message, string details, string level, string logger)
        {
            if (LoggingHandler.IsDbLoggingEnabled)
                LogToDb(message, details, level, logger);
            if (LoggingHandler.IsEmailLoggingEnabled)
                LogToEmail(message, details, level, logger);
            if (LoggingHandler.IsFileLoggingEnabled)
                LogToFile(message, details, level, logger);
        }

        public static void Log(Exception ex, string logger)
        {
            if (LoggingHandler.IsDbLoggingEnabled)
                LogToDb(ex, logger);
            if (LoggingHandler.IsEmailLoggingEnabled)
                LogToEmail(ex, logger);
            if (LoggingHandler.IsFileLoggingEnabled)
                LogToFile(ex, logger);
        }

        public static void Log(Exception ex, string logger, string level)
        {
            if (LoggingHandler.IsDbLoggingEnabled)
                LogToDb(ex, logger, level);
            if (LoggingHandler.IsEmailLoggingEnabled)
                LogToEmail(ex, logger, level);
            if (LoggingHandler.IsFileLoggingEnabled)
                LogToFile(ex, logger, level);
        }


        public static void LogToEmail(string message, string details, string level, string logger)
        {
            if (LoggingHandler.IsEmailLoggingEnabled)
            {
                BlogLog log = LoggingHandler.GetBlogLog(message, details, level, logger);

                EmailHandler.SendEmail("logging@" + HttpContext.Current.Request.Url.Host,
                    log.ToString(), CacheHandler.GetBlogConfig().LogEmailAddress, HttpContext.Current.Request.Url.Host + " " + level,
                    false);
            }
        }

        public static void LogToEmail(Exception ex, string logger)
        {
            LogToEmail(ex, logger, "Error");
        }

        public static void LogToEmail(Exception ex, string logger, string level)
        {
            if (LoggingHandler.IsEmailLoggingEnabled)
            {
                BlogLog log = LoggingHandler.GetBlogLog(ex, logger, level);

                EmailHandler.SendEmail("logging@" + HttpContext.Current.Request.Url.Host,
                    log.ToString(), CacheHandler.GetBlogConfig().LogEmailAddress, HttpContext.Current.Request.Url.Host + " " + level,
                    false);
            }
        }

        public static void LogToFile(string message, string details, string level, string logger)
        {
            if (LoggingHandler.IsFileLoggingEnabled)
            {
                BlogLog log = LoggingHandler.GetBlogLog(message, details, level, logger);

                File.AppendAllText(CacheHandler.GetBlogConfig().LogFilePath, log.ToString());
            }
        }

        public static void LogToFile(Exception ex, string logger)
        {
            LogToFile(ex, logger, "Error");
        }

        public static void LogToFile(Exception ex, string logger, string level)
        {
            if (LoggingHandler.IsFileLoggingEnabled)
            {
                BlogLog log = LoggingHandler.GetBlogLog(ex, logger, level);

                File.AppendAllText(CacheHandler.GetBlogConfig().LogFilePath, log.ToString());
            }
        }

        public static void LogToDb(string message, string details, string level, string logger)
        {
            if (LoggingHandler.IsDbLoggingEnabled)
            {
                BlogLog log = LoggingHandler.GetBlogLog(message, details, level, logger);

                var repo = VeritasRepository.GetInstance();
                repo.Add(log);
                repo.Save();
            }
        }

        public static void LogToDb(Exception ex, string logger)
        {
            LogToDb(ex, logger, "Error");
        }

        public static void LogToDb(Exception ex, string logger, string level)
        {
            if (LoggingHandler.IsDbLoggingEnabled)
            {
                BlogLog log = LoggingHandler.GetBlogLog(ex, logger, level);

                var repo = VeritasRepository.GetInstance();
                repo.Add(log);
                repo.Save();
            }
        }


        private static string GetInfoFromException(Exception ex)
        {
            string stackTrace = ex.StackTrace + "\n\n";
            if (ex.InnerException != null)
                stackTrace += LoggingHandler.GetInfoFromException(ex.InnerException);
            return stackTrace;
        }

        private static BlogLog GetBlogLog(string message, string details, string level, string logger)
        {
            BlogLog log = new BlogLog()
            {
                BlogConfigId = CacheHandler.BlogConfigId,
                CreateDate = DateTime.Now,
                EventLevel = level,
                Exception = details,
                Message = message,
                Url = HttpContext.Current.Request.Url.ToString(),
                Logger = logger
            };
            return log;
        }

        private static BlogLog GetBlogLog(Exception ex, string logger)
        {
            return GetBlogLog(ex, logger, "Error");
        }

        private static BlogLog GetBlogLog(Exception ex, string logger, string level)
        {
            BlogLog log = new BlogLog()
            {
                BlogConfigId = CacheHandler.BlogConfigId,
                CreateDate = DateTime.Now,
                EventLevel = level,
                Exception = GetInfoFromException(ex),
                Message = ex.Message,
                Url = HttpContext.Current.Request.Url.ToString(),
                Logger = logger
            };
            return log;
        }

        public static void LoginFail(string username)
        {
            Log("Login failed", "Login failed for " + username, "AuthFail", "AuthFail");
        }
    }
}
