<%@ WebHandler Language="C#" Class="ProcessChangeTransStatus" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.Collections.Generic;
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.Agent.Common;
using SB.DBLibrary;
using SB.Common.Files;
public class ProcessChangeTransStatus : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        int ToTID = 0;
        int NewStatus = 0;
        string Remark = "";

        if (!PageBase.BaseValidate() )
        {
            context.Response.Write("Validate Error !");
            return;
        }

        if (!Agents.ReportMgr)
        {
            return;
        }
        #region Get Request
        int.TryParse(context.Request["ToTID"], out ToTID);
        

        if (ToTID == 0 || !int.TryParse(context.Request["NewStatus"],out NewStatus))
        {
            return;
        }
        Remark =string.IsNullOrEmpty( context.Request["txtMemo"])?"":context.Request["txtMemo"];
        #endregion




        
        #region Update Status
        DBbase connMain = new DBbase(SiteSetting.MainConnString);
        connMain.ExecuteByParameters("New_Age_UpdateTransactionStatus", new SqlParameter("@TransID", ToTID)
                                                                        , new SqlParameter("@TransStatus", NewStatus)
                                                                        , new SqlParameter("@Remark", Remark)
                                                                        , new SqlParameter("@Editor",Agents.AgentID));
        #endregion

        context.Response.Write("ok");
            
    }
 
    public bool IsReusable {
        get {
            return true;
        }
    }

}