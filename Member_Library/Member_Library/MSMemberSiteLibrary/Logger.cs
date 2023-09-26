using System;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Windows.Forms;
using SB.Common.Files;
namespace member_site
{

    /// <summary>
    /// static Log Functions Collection for web member site
    /// </summary>
    public static class Logger
    {

        public static void Log2File(string FileName, string LogStr)
        {
           
            SB.Common.Files.Logger.Log(FileName, LogStr);
        }

        public static void Log2File(string LogStr)
        {
            SB.Common.Files.Logger.Log(LogStr);
        }


        public static void Log(string LogStr)
        {
            SB.Common.Files.Logger.Log(LogStr);
        }

        public static void Log(string FileName, string LogStr)
        {
            SB.Common.Files.Logger.Log(FileName, LogStr);
        }

        public static void LogException(Exception e)
        {
            SB.Common.Files.Logger.LogException(e);
        }

        public static void LogException(Exception e, string s)
        {
            SB.Common.Files.Logger.LogException(e, s);
        }

        public static void LogException(string FileName, Exception e)
        {
            SB.Common.Files.Logger.LogException(FileName, e);
        }

        public static void LogException(string FileName, Exception e, string sInfo)
        {
            SB.Common.Files.Logger.LogException(FileName, e, sInfo);
        }

        public static string Exception2LogStr(Exception e, string s)
        {
            string sLog = (s == null) ? "" : s + "\r\n";
            sLog += "ClassName:" + e.GetType().ToString() + "\r\n";
            sLog += "Message:" + e.Message + "\r\n";
            sLog += "StackTrace: \r\n" + e.StackTrace + "\r\n";
            return sLog;
        }

        /// <summary>
        /// this is for member / deposit site after login ONLY
        /// if this class be copied to other application, remove this method
        /// </summary>
        /// <param name="LogStr"></param>
        public static void Log2FileByUser(string LogStr)
        {
                        string sPath = "";
            if (HttpContext.Current == null)
            {
                sPath = Application.StartupPath + "\\";
            }
            else
            {
                try
                {
                    sPath = HttpContext.Current.Server.MapPath("/");
                }
                catch
                {
                    sPath = "\\";
                }
            }
            sPath += "Logs\\" + Auth.GetUserName(HttpContext.Current) + "\\";

            string sFileName = "Log_" + DateTime.Now.ToString("yyyyMMdd") + ".log";

            SB.Common.Files.Logger.Log(sPath + sFileName, LogStr);
        }


        public static void Log2File(HttpContext Context, string LogStr)
        {
            Log2File(LogStr);
        }

    }

}