<%@ WebHandler Language="C#" Class="ProcessCHangeSubPWD" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using SB.DBLibrary;
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.Agent.Common;
using SB.Common.Files;

public class ProcessCHangeSubPWD : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string UserLan = Agents.Language;
        int ToCID = 0;
        string NewPWD = "";
        string ConPWD = "";
        int CurrentAgentID = 0;
        int CurrentRoleID = 0;
        int SubAgentID = 0;
        int SubRoleID = 0;

        if (!Agents.UserMgr && !Agents.MemberMgr)
        {
            return;
        }
        if (!PageBase.BaseValidate())
        {
            context.Response.Write("Validate Error !");
            return;
        }

        #region Get Request
        int.TryParse(context.Request["ToCID"], out ToCID);
        NewPWD = context.Request["NewPWD"];
        ConPWD = context.Request["ConPWD"];
        int.TryParse(context.Request["SubAgentID"], out SubAgentID);
        int.TryParse(context.Request["SubRoleID"], out SubRoleID);
        if (ToCID == 0 || string.IsNullOrEmpty(NewPWD) || string.IsNullOrEmpty(ConPWD))
        {
            return;
        }


        #endregion


            CurrentAgentID = Agents.AgentID;
            CurrentRoleID = Agents.RoleID;



        string resultMsg= CheckPassword(UserLan, NewPWD, ConPWD);
        if (!string.IsNullOrEmpty(resultMsg))
        {
            context.Response.Write(resultMsg);
            return;
        }
        
        #region Update PWD
        DBbase connMain = new DBbase(SiteSetting.MainConnString);
        connMain.ExecuteByParameters("New_Age_ChangeSubPassword", new SqlParameter("@custid", ToCID)
                                , new SqlParameter("@new_pw", NewPWD)
                                , new SqlParameter("@pw_expiry", DateTime.Now.AddDays(30).ToString("yyyy/MM/dd HH:mm:ss")));

        #endregion

        context.Response.Write("ok");
    }

    private string CheckPassword(string UserLan,  string strPW, string strConPW)
    {
        string ResultMsg = "";
        Regex CheckTest = new Regex("^[0-9a-zA-Z]{1,}$");

        if (strPW == null || strPW == "")
        {
            ResultMsg = Skin.GetResValue(UserLan, "lbl_enter_pw");
        }
        if (strConPW == null || strConPW == "")
        {
            ResultMsg = Skin.GetResValue(UserLan, "lbl_confirm_pw");
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
            ResultMsg = Skin.GetResValue(UserLan, "lbl_Passworddifferent");
        }


        //ResultCode result = new ResultCode();
        //result = Agents.ChangePassword(Agents.AgentID, strOldPW, strPW);

        return ResultMsg;
    }
    
    public bool IsReusable {
        get {
            return true;
        }
    }

}