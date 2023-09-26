using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
/// <summary>
/// Site 的摘要描述
/// </summary>
namespace SB.Agent.Sites
{
    public class SiteSetting
    {
        public static string MainConnString
        {
            get
            {
                return WebConfigurationManager.AppSettings["MainConnectionString"] == null ? "" :
                    WebConfigurationManager.AppSettings["MainConnectionString"].ToString();
            }
        }
        public static string OnUserConnString
        {
            get
            {
                return WebConfigurationManager.AppSettings["OnUserConnectionString"] == null ? "" :
                    WebConfigurationManager.AppSettings["OnUserConnectionString"].ToString();
            }
        }
        public static int SiteID
        {
            get
            {
                return WebConfigurationManager.AppSettings["SiteID"] == null ? 3000001 :
                    Convert.ToInt32 (WebConfigurationManager.AppSettings["SiteID"]);
            }
        }
        public static bool IsTestSite
        {
            get
            {
                return WebConfigurationManager.AppSettings["IsTest"] == null ? true :
                    Convert.ToBoolean(WebConfigurationManager.AppSettings["IsTest"]);
            }
        }
        public static bool UseAutoTransfer
        {
            get
            {
                return WebConfigurationManager.AppSettings["UseAutoTransfer"] == null ? false :
                    Convert.ToBoolean(WebConfigurationManager.AppSettings["UseAutoTransfer"]);
            }
        }

        public static string ServerInfoKey
        {
            get
            {
                return HttpContext.Current.Session["ServerInfoKey"] == null?"":
                    HttpContext.Current.Session["ServerInfoKey"].ToString();
            }
            set 
            {
                HttpContext.Current.Session["ServerInfoKey"] = value;
            }
        }

        public static string ValidationCode
        {
            get
            {
                return HttpContext.Current.Session["ValidationCode"] == null ? "" :
                    HttpContext.Current.Session["ValidationCode"].ToString();
            }
            set
            {
                HttpContext.Current.Session["ValidationCode"] = value;
            }
        }


        public SiteSetting()
        {

        }
    }
}