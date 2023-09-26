<%@ WebHandler Language="C#" Class="ProcessChangeMemberSetting" %>

using System;
using System.Web;
using System.Data;
using System.Web.SessionState;
using System.Data.SqlClient;
using SB.DBLibrary;
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.Agent.Common;
using SB.Common.Files;
public class ProcessChangeMemberSetting : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!PageBase.BaseValidate())
        {
            context.Response.Write("Validate Error !");
            return;
        }
        if (!Agents.MemberMgr)
        {
            return;
        }
        int CustID = 0;
        int ProviderID = 0;
        int GameTypeID = 0;
        int NewStatus = 0;
        int.TryParse( context.Request["ToCID"],out CustID);
        int.TryParse(context.Request["ProviderID"], out ProviderID);
        int.TryParse(context.Request["GameType"], out GameTypeID);
        int.TryParse(context.Request["NewStatus"], out NewStatus);
        #region Update Setting
        DBbase connMain = new DBbase(SiteSetting.MainConnString);
        try
        {
            connMain.ExecuteByParameters("New_Age_Upd_MemberSetting", new SqlParameter("@CustID", CustID)
                                                            , new SqlParameter("@ProviderID", ProviderID)
                                                            , new SqlParameter("@GameTypeID", GameTypeID)
                                                            , new SqlParameter("@Status", NewStatus));
        }
        catch (Exception ex)
        {
            Logger.LogException(ex, "ProcessChangeMemberSetting Error");
            throw ex;
        }

        #endregion
        context.Response.Write("ok");

    }
 
    public bool IsReusable {
        get {
            return true;
        }
    }

}