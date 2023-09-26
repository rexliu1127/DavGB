using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aspxtemplate;
using SB.Agent.Sites;
using SB.Agent.Users;
/// <summary>
/// PageBase 的摘要描述
/// </summary>
namespace SB.Agent.Common
{
    public class PageBase : ParsedPage
    {


        public PageBase()
            : base()
        {

        }

        protected override void OnLoad(EventArgs e)
        {

            BaseValidate();
            base.OnLoad(e);
        }

        public static bool BaseValidate()
        {
            if (!ValidateBaseSession())
            {
                return false;
            }

            if (!Agents.IsLogin)
            {
                HttpContext.Current.Response.Redirect("index.aspx");
                return false;
            }

            if (CheckUM())
            {
                return false;
            }
            return true;
        }

        public static bool  ValidateBaseSession()
        { 
            if (string.IsNullOrEmpty(SiteSetting.ServerInfoKey))
            {
                HttpContext.Current.Response.Redirect("index.aspx");
                
                return false;
            }
            return true;
        }

        public static bool CheckUM()
        {
            bool IsUM = false;
            if (IsUM)
            {
                //HttpContext.Current.Response.Redirect("");
                //return;
            }
            return false;
        }

    }
}