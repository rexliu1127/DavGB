using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using SB.Agent.Common;
using SB.Agent.Users;
using SB.Agent.Sites;
public partial class ChangePWD :PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserLan = Agents.Language;

        if (Request["hidSubmit"] != null && Request["hidSubmit"].ToString() == "YES")
        {
            #region Process Change Password


            string strOldPW = Request["txtOldPW"] == null ? "" : Request["txtOldPW"].ToString().Trim();
            string strPW = Request["txtPW"] == null ? "" : Request["txtPW"].ToString().Trim();
            string strConPW = Request["txtConPW"] == null ? "" : Request["txtConPW"].ToString().Trim();
            this.ChangePasswordUpdate(UserLan, strOldPW, strPW, strConPW);


            #endregion
        }
        else
        {
            #region Load Template
            PageParser.SetTemplatesDir(Skin.GetPublicPath(SiteSetting.SiteID));
            PageParser.SetTemplateFile("ChangePassword.html");
            #endregion

            #region Set Label
            PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));

            PageParser.SetVariable("lbl_changepassword", Skin.GetResValue(UserLan, "lbl_changepassword"));

            PageParser.SetVariable("lbl_oldpassword", Skin.GetResValue(UserLan, "lbl_oldpassword"));

            PageParser.SetVariable("lbl_password", Skin.GetResValue(UserLan, "lbl_password"));

            PageParser.SetVariable("lbl_confirmpassword", Skin.GetResValue(UserLan, "lbl_confirmpassword"));

            PageParser.SetVariable("lbl_submit", Skin.GetResValue(UserLan, "lbl_submit"));

            PageParser.SetVariable("lbl_reset", Skin.GetResValue(UserLan, "lbl_reset"));

            PageParser.SetVariable("lbl_enter_old_pw", Skin.GetResValue(UserLan, "lbl_enter_old_pw"));
            PageParser.SetVariable("lbl_enter_pw", Skin.GetResValue(UserLan, "lbl_enter_pw"));
            PageParser.SetVariable("lbl_confirm_pw", Skin.GetResValue(UserLan, "lbl_confirm_pw"));
            PageParser.SetVariable("lbl_Passworddifferent", Skin.GetResValue(UserLan, "lbl_Passworddifferent"));
            PageParser.SetVariable("lbl_execPassword", Skin.GetResValue(UserLan, "lbl_execPassword"));
            #endregion
        }
    }

    private  void ChangePasswordUpdate(string UserLan,string strOldPW, string strPW, string strConPW)
    {
        string ResultMsg = "";
        Regex CheckTest = new Regex("^[0-9a-zA-Z]{1,}$");
        if (strOldPW == null || strOldPW == "")
        {
            ResultMsg = Skin.GetResValue(UserLan, "lbl_enter_old_pw");
        }
        if (strPW == null || strPW == "")
        {
            ResultMsg = Skin.GetResValue(UserLan, "lbl_enter_pw");
        }
        if (strConPW == null || strConPW == "")
        {
            ResultMsg = Skin.GetResValue(UserLan, "lbl_confirm_pw");
        }
        if (!CheckTest.IsMatch(strOldPW))
        {
            ResultMsg = Skin.GetResValue(UserLan, "lbl_oldpassword") + " " + Skin.GetResValue(UserLan, "lbl_execPassword");
        }
        if (!CheckTest.IsMatch(strPW))
        {
            ResultMsg = Skin.GetResValue(UserLan, "lbl_password") + " " + Skin.GetResValue(UserLan, "lbl_execPassword");
        }
        if (!CheckTest.IsMatch(strConPW))
        {
            ResultMsg = Skin.GetResValue(UserLan, "lbl_confirmpassword") + " " + Skin.GetResValue(UserLan, "lbl_execPassword");
        }

        if (strPW != strConPW)
        {
            ResultMsg = Skin.GetResValue(UserLan, "lbl_confirm_pw");
        }


        ResultCode result = new ResultCode();
        result=Agents.ChangePassword(Agents.AgentID, strOldPW, strPW);
        if (result == ResultCode.OldPwdError)
        {
            ResultMsg = Skin.GetResValue(UserLan, "lbl_old_pw"); 
        }

        if (string.IsNullOrEmpty(ResultMsg))
        {
            ResultMsg = Skin.GetResValue(UserLan, "lbl_success_change_pw");
            ResultMsg = ResultMsg.Replace("'", "\\'");
        }

        Response.Write( ResultMsg );
    }

}