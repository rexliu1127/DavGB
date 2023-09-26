using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using aspxtemplate;
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.Common.Files;

public partial class index : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        #region Set Server Info Key
        SiteSetting.ServerInfoKey = Request.Url.Host;
        #endregion

        #region Process Get or Change Lan
        HttpCookie LangCookie = null;
        LangCookie = Request.Cookies.Get("AgentUserLan");
        //change lan
        if (!string.IsNullOrEmpty(Request["hidSelLang"]))
        {
            Agents.Language = Request["hidSelLang"];
            LangCookie = new HttpCookie("AgentUserLan");
            LangCookie.Expires = DateTime.Now.AddYears(1);
            LangCookie.Value = Agents.Language;
            Response.Cookies.Add(LangCookie);
        }
        else
        {
            if (string.IsNullOrEmpty(Agents.Language))
            {
                //cookie is null and  get default lan
                if (LangCookie==null)
                {
                    Agents.Language = "en";
                    LangCookie = new HttpCookie("AgentUserLan");
                    LangCookie.Value = Agents.Language;
                    LangCookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(LangCookie);
                }
                //get lan from cookie
                else
                {
                    Agents.Language = LangCookie.Value;
                }
            }
        }
        #endregion



        string UserLan = Agents.Language;


        #region Load template
        PageParser.SetTemplatesDir( Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("Index.html");
        #endregion

        #region Set Logo
        string logoPic = "logoPic";
        if (UserLan.ToLower() != "en")
        {
            logoPic = logoPic + "_" + UserLan.ToLower();
        }
        PageParser.SetVariable("logoPic", logoPic);

        #endregion

        #region Set Label
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("UserLan", UserLan);
        PageParser.SetVariable("lbl_UserName", Skin.GetResValue(UserLan, "lbl_UserName"));
        PageParser.SetVariable("lbl_Password", Skin.GetResValue(UserLan, "lbl_Password"));
        PageParser.SetVariable("lbl_Validation", Skin.GetResValue(UserLan, "lbl_Validation"));
        PageParser.SetVariable("lbl_Language", Skin.GetResValue(UserLan, "lbl_Language"));
        PageParser.SetVariable("lbl_Login", Skin.GetResValue(UserLan, "lbl_Login"));
        PageParser.SetVariable("lbl_Reset", Skin.GetResValue(UserLan, "lbl_Reset"));
        PageParser.SetVariable("lbl_Username_Null", Skin.GetResValue(UserLan, "lbl_Username_Null"));
        PageParser.SetVariable("lbl_PWD_Null", Skin.GetResValue(UserLan, "lbl_PWD_Null"));
        PageParser.SetVariable("lbl_Validation_Null", Skin.GetResValue(UserLan, "lbl_Validation_Null"));
        #endregion
    }
}