using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SB.Common.Files;

/// <summary>
/// Skin 的摘要描述
/// </summary>
namespace SB.Agent.Sites
{
    public class Skin
    {
        public Skin()
        {

        }

        public static string GetRootPath(int SiteID)
        {
            string strSkinPath = "";
            try
            {
                strSkinPath = (string)HttpContext.GetGlobalResourceObject("SkinPath", SiteID.ToString());
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "GetRootPath Error!");
                throw ex;
            }
            strSkinPath = "template/" + strSkinPath + "/";
            
            return strSkinPath;
        }

        public static string GetLanPath(int SiteID, string UserLan)
        {
            string strSkinPath = "";
            try
            {
                strSkinPath = (string)HttpContext.GetGlobalResourceObject("SkinPath", SiteID.ToString());
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "GetLanPath Error!");
                throw ex;
            }

            UserLan = UserLan.ToLower();
            strSkinPath = "template/" + strSkinPath + "/" + UserLan+"/";
            return strSkinPath;

        }

        public static string GetPublicPath(int SiteID)
        {
            string strSkinPath = (string)HttpContext.GetGlobalResourceObject("SkinPath", SiteID.ToString());
            if (strSkinPath == null)
            {
                return "";
            }
            else
            {
                strSkinPath = "template/" + strSkinPath + "/public/";

            }
            return strSkinPath;

        }

        public static string GetResValue(string Language, string Key)
        {
            string value = "";
            string ResPath = "";
            ResPath = "Lan_" + Language;
            try
            {
                value = HttpContext.GetGlobalResourceObject(ResPath, Key) == null ? "" :
                    (string)HttpContext.GetGlobalResourceObject(ResPath, Key);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "GetResourceValue Error!");
                throw ex;
            }
            return value;
        }
    }
}