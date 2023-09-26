<%@ WebHandler Language="C#" Class="ProcessChangeSubStatus" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.Collections.Generic;
using SB.DBLibrary;
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.Agent.Common;
using SB.Common.Files;
public class ProcessChangeSubStatus : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        int ToCID = 0;
        int NewStatus = 0;
        int CurrentAgentID = 0;
        int CurrentRoleID = 0;
        int SubAgentID = 0;
        int SubRoleID = 0;
        string  Enabled=null;
        string  Closed=null;
        string Suspend=null;

        if (!PageBase.BaseValidate() )
        {
            context.Response.Write("Validate Error !");
            return;
        }

        if (!Agents.UserMgr && !Agents.MemberMgr)
        {
            return;
        }
        #region Get Request
        int.TryParse(context.Request["ToCID"], out ToCID);
        int.TryParse(context.Request["NewStatus"],out NewStatus);
        int.TryParse(context.Request["SubAgentID"], out SubAgentID);
        int.TryParse(context.Request["SubRoleID"], out SubRoleID);
        if (ToCID == 0 ||NewStatus==0)
        {
            return;
        }
        

        #endregion

        //Get Default Data or Sub Level Data
        if (SubAgentID == 0 || SubRoleID == 0)
        {
            CurrentAgentID = Agents.AgentID;
            CurrentRoleID = Agents.RoleID;
        }
        else
        {
            CurrentAgentID = SubAgentID;
            CurrentRoleID = SubRoleID;
        }
        
        switch (NewStatus)
        {
            case (int)AccStatus.Closed:
                Closed = "true";
                Suspend = "false";
                Enabled = "true";
                break;
            case (int)AccStatus.Suspend:
                Suspend = "true";
                Closed = "false";
                Enabled = "true";
                break;
            case (int)AccStatus.Disabled:
                Enabled="false";
                Suspend = "false";
                Closed = "false";
                break;
            case (int)AccStatus.Open:
                Enabled = "true";
                Suspend = "false";
                Closed = "false";
                break;
        }

        List<SqlParameter> SqlParameterList = new List<SqlParameter>();
        SqlParameterList.Add(new SqlParameter("@ToUserID", ToCID));
        if (!string.IsNullOrEmpty(Enabled))
        {
            SqlParameterList.Add(new SqlParameter("@Enabled", Convert.ToBoolean(Enabled)));
        }
        if (!string.IsNullOrEmpty(Closed))
        {
            SqlParameterList.Add(new SqlParameter("@Closed", Convert.ToBoolean(Closed)));
        }
        if (!string.IsNullOrEmpty(Suspend))
        {
            SqlParameterList.Add(new SqlParameter("@Suspended", Convert.ToBoolean(Suspend)));
        }
        
        #region Update Status
        DBbase connMain = new DBbase(SiteSetting.MainConnString);
        connMain.ExecuteByParameters("New_Age_UpdateAccountStatus", SqlParameterList.ToArray());
        #endregion

        context.Response.Write("ok");
            
    }
 
    public bool IsReusable {
        get {
            return true;
        }
    }

}