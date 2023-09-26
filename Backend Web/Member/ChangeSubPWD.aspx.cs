using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SB.Agent.Common;
using SB.Agent.Users;
using SB.Agent.Sites;
public partial class ChangeSubPassword : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserLan = Agents.Language;
        int ToCID = 0;
        string ToCN = "";


        if (!Agents.MemberMgr)
        {
            return;
        }

        #region Get Request dtat
        int.TryParse(Request["ToCID"], out ToCID);
        ToCN = Request["ToCN"];
        if (ToCID == 0 || string.IsNullOrEmpty(ToCN))
        {
            return;
        }

        #endregion
        #region Load Template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("ChangeSubPassword.html");
        #endregion



        

        #region Set Label
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("lbl_UserName", Skin.GetResValue(UserLan, "lbl_UserName"));
        PageParser.SetVariable("UserName", ToCN);
        PageParser.SetVariable("lbl_changepassword", Skin.GetResValue(UserLan, "lbl_changepassword"));
        PageParser.SetVariable("lbl_password", Skin.GetResValue(UserLan, "lbl_password"));
        PageParser.SetVariable("lbl_confirmpassword", Skin.GetResValue(UserLan, "lbl_confirmpassword"));
        PageParser.SetVariable("lbl_submit", Skin.GetResValue(UserLan, "lbl_submit"));
        PageParser.SetVariable("lbl_reset", Skin.GetResValue(UserLan, "lbl_reset"));
        PageParser.SetVariable("lbl_enter_pw", Skin.GetResValue(UserLan, "lbl_enter_pw"));
        PageParser.SetVariable("lbl_confirm_pw", Skin.GetResValue(UserLan, "lbl_confirm_pw"));
        PageParser.SetVariable("lbl_Passworddifferent", Skin.GetResValue(UserLan, "lbl_Passworddifferent"));
        PageParser.SetVariable("lbl_execPassword", Skin.GetResValue(UserLan, "lbl_execPassword"));
        PageParser.SetVariable("lbl_cancel", Skin.GetResValue(UserLan, "lbl_Cancel"));
        PageParser.SetVariable("ToCID", ToCID.ToString());
        #endregion

    }
}