using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using member_site;
using aspxtemplate;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class Change_Password : ParsedPage
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!member_site.Auth.ValidateLogin(this.Context)) return;

        string SkinRootPath = SelLang.GetSkinRootPath();
        //member_site.Auth au = new member_site.Auth();
        string strExpiry = Request["expiry"] == null ? "" : Request["expiry"].ToString();

        SelLang sl = new SelLang(Auth.GetUserLang(this.Context));

        #region Load Template
        PageParser.SetTemplatesDir(SkinRootPath+"public/");
        PageParser.SetTemplateFile("Change_Password.html");
        #endregion
        PageParser.SetVariable("skinrootpath", SkinRootPath);


        #region Set Label
        string webname = member_site.Common.GetWebName(Context);
        PageParser.SetVariable("webname", webname);

        string skin_path = member_site.SelLang.GetSkinPath(Context);

        //PageParser.SetVariable("skinpath", skin_path);

        PageParser.SetVariable("lbl_changepassword", sl.GetResourceValue("lbl_changepassword"));

        PageParser.SetVariable("lbl_oldpassword", sl.GetResourceValue("lbl_oldpassword"));

        PageParser.SetVariable("lbl_password", sl.GetResourceValue("lbl_password"));

        PageParser.SetVariable("lbl_confirmpassword", sl.GetResourceValue("lbl_confirmpassword"));

        PageParser.SetVariable("btnChangePW", sl.GetResourceValue("lbl_submit"));

        PageParser.SetVariable("btnReset", sl.GetResourceValue("lbl_reset"));

        PageParser.SetVariable("hiddenOldPW", SelLang.GetMsgResourceValue(this.Context, "errlogin_enter_old_pw"));
        PageParser.SetVariable("hiddenPW", SelLang.GetMsgResourceValue(this.Context, "errlogin_enter_pw"));
        PageParser.SetVariable("hiddenConPW", SelLang.GetMsgResourceValue(this.Context, "errlogin_confirm_pw"));
        PageParser.SetVariable("hiddenPWdiff", SelLang.GetResourceValue(this.Context, "lbl_Passworddifferent"));
        PageParser.SetVariable("hiddenExecPW", SelLang.GetResourceValue(this.Context, "lbl_execPassword"));
        PageParser.SetVariable("lbl_execPassword", SelLang.GetResourceValue(this.Context, "lbl_execPassword"));

        PageParser.UpdateBlock("NextDateExpiry");
        if (strExpiry != "" || Convert.ToString(Request["hidExDate"] == null ? "0" : Request["hidExDate"]) == "1")
        {
            PageParser.SetVariable("hiddenExDate", "1");
            PageParser.SetVariable("btnExRemind", SelLang.GetResourceValue(this.Context, "lbl_remindnextmon"));
            PageParser.ParseBlock("NextDateExpiry");
        }
        else
            PageParser.SetVariable("hiddenExDate", "0");

        #endregion

        #region Process Change Password

        try
        {
            if (Convert.ToString(Request["hidExDate"]) != "0")
            {
                if (Request["hidSubmit"] != null && Request["hidSubmit"].ToString() == "YES")
                {
                    string strOldPW = Request["txtOldPW"] == null ? "" : Request["txtOldPW"].ToString().Trim();
                    string strOldLowerCasePW = Request["hidLowerCaseOldPW"] == null ? "" : Request["hidLowerCaseOldPW"].ToString().Trim();
                    string strPW = Request["txtPW"] == null ? "" : Request["txtPW"].ToString().Trim();
                    string strConPW = Request["txtConPW"] == null ? "" : Request["txtConPW"].ToString().Trim();
     
                    this.ChangePasswordUpdate(strOldPW,strOldLowerCasePW, strPW, strConPW);

                    Response.Write("<script charset='utf-8'>window.top.frames['leftFrame'].refreshAccountInfo('full')</script>");
                    Response.Write("<script charset='utf-8'>window.top.frames['mainFrame'].location = 'UnderOver.aspx'</script>");
                }
                else
                {
                    if (Request["hidRemindSubmit"] != null && Request["hidRemindSubmit"].ToString() == "next")
                    {
                        member_site.Auth au = new member_site.Auth();

                        au.ChangePassword(this.Context,"", "", "", "", true);
                        Response.Write("<script charset='utf-8'>alert('" + SelLang.GetMsgResourceValue(this.Context, "success_remind_nextmonth") + "');</script>");
                        Response.Write("<script charset='utf-8'>window.top.frames['leftFrame'].refreshAccountInfo('full')</script>");
                        Response.Write("<script charset='utf-8'>window.top.frames['mainFrame'].location = 'UnderOver.aspx'</script>");
                    }
                }
            }
            else
            {
                if (Request["hidSubmit"] != null && Request["hidSubmit"].ToString() == "YES")
                {
                    string strOldPW = Request["txtOldPW"] == null ? "" : Request["txtOldPW"].ToString().Trim();
                    string strOldLowerCasePW = Request["hidLowerCaseOldPW"] == null ? "" : Request["hidLowerCaseOldPW"].ToString().Trim();
                    string strPW = Request["txtPW"] == null ? "" : Request["txtPW"].ToString().Trim();
                    string strConPW = Request["txtConPW"] == null ? "" : Request["txtConPW"].ToString().Trim();

                    this.ChangePasswordUpdate(strOldPW,strOldLowerCasePW, strPW, strConPW);

                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script charset='utf-8'>alert('" + ex.Message + "')</script>");
        }
        #endregion
    }

    private void ChangePasswordUpdate(string strOldPW,string strOldLowerCasePW, string strPW, string strConPW)
    {
        member_site.Auth au = new member_site.Auth();
        //Logger.Log2File("New PW : " + strPW + "; ConPW : " + strConPW);
        string strSuccess = SelLang.GetMsgResourceValue(this.Context, "success_change_pw");
        strSuccess = strSuccess.Replace("'", "\\'");
        Regex CheckTest = new Regex("^[0-9a-zA-Z]{1,}$");
        if (strOldPW == null || strOldPW == "")
        {
            throw new Exception(SelLang.GetMsgResourceValue(this.Context, "errlogin_enter_old_pw"));
        }
        if (strPW == null || strPW == "")
        {
            throw new Exception(SelLang.GetMsgResourceValue(this.Context, "errlogin_enter_pw"));
        }
        if (strConPW == null || strConPW == "")
        {
            throw new Exception(SelLang.GetMsgResourceValue(this.Context, "errlogin_confirm_pw"));
        }
        if (!CheckTest.IsMatch(strOldPW))
        {
            throw new Exception("oldpwd1  " + SelLang.GetResourceValue(this.Context, "lbl_oldpassword") + " " + SelLang.GetResourceValue(this.Context, "lbl_execPassword"));
        }
        if (!CheckTest.IsMatch(strPW))
        {
            throw new Exception("pwd1  " + SelLang.GetResourceValue(this.Context, "lbl_password") + " " + SelLang.GetResourceValue(this.Context, "lbl_execPassword"));
        }
        if (!CheckTest.IsMatch(strConPW))
        {
            throw new Exception("conpwd1  " + SelLang.GetResourceValue(this.Context, "lbl_confirmpassword") + " " + SelLang.GetResourceValue(this.Context, "lbl_execPassword"));
        }

        if (strPW != strConPW)
        {
            throw new Exception(SelLang.GetMsgResourceValue(this.Context, "errchange_confirm_pw"));
        }
        //if (strOldPW.Length < 6)
        //{
        //    throw new Exception(SelLang.GetResourceValue(this.Context, "lbl_oldpassword") + " " + SelLang.GetResourceValue(this.Context, "lbl_execPassword"));
        //}
        //if (strPW.Length < 6)
        //{
        //    throw new Exception("pwd2  " + SelLang.GetResourceValue(this.Context, "lbl_password") + " " + SelLang.GetResourceValue(this.Context, "lbl_execPassword"));
        //}
        //if (strConPW.Length < 6)
        //{
        //    throw new Exception("conpwd2  " + SelLang.GetResourceValue(this.Context, "lbl_confirmpassword") + " " + SelLang.GetResourceValue(this.Context, "lbl_execPassword"));
        //}     

        au.ChangePassword(this.Context, strOldPW,strOldLowerCasePW, strPW, strConPW, false);

        Response.Write("<script charset='utf-8'>alert('" + strSuccess + "');window.top.leftFrame.SwitchMenuType(0,'',true);window.top.mainFrame.location.href ='UnderOver.aspx?Market=t';</script>");
    }
}
