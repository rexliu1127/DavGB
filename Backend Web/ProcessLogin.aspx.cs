using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using aspxtemplate;
using SB.Agent.Common;
using SB.Agent.Sites;
using SB.Agent.Users;
using SB.Common.Files;
public partial class ProcessLogin : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(SiteSetting.ServerInfoKey))
        {
            Response.Write("Sessino timeout , please refresh !");
            return;
        }
        #region Check and Get Request
        if (string.IsNullOrEmpty(Request["UserName"]) || string.IsNullOrEmpty(Request["PWD"])
            || string.IsNullOrEmpty(Request["VCode"]))
        {
            return;
        }
        ResultCode result = new ResultCode();
        string UserName = Request["UserName"];
        string PWD = Common.CfsEncode(Request["PWD"]);
        string ValidateCode = Request["VCode"];
        string UserLan = Agents.Language;
        #endregion

        if (ValidateCode != SiteSetting.ValidationCode)
        {
            Response.Write(Skin.GetResValue(UserLan, "lbl_ValidationCodeError"));
            return;
        }

        #region Process Login
        Logger.Log("PWD=" + PWD);
        result = Agents.Login(UserName, PWD, Agents.IPAddress);

        #endregion

        #region Check Result
        switch (result)
        {
            case ResultCode.Success:
                Response.Write("ok");
                break;
            case ResultCode.AccClosed:
                Response.Write(Skin.GetResValue(UserLan, "lbl_AC_Closed"));
                break;
            case ResultCode.IdPwdError:
                Response.Write(Skin.GetResValue(UserLan, "lbl_Incorrect_id_pw"));
                break;

        }
        #endregion




    }
}