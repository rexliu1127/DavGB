<%@ WebHandler Language="C#" Class="DoUpdDefaultMinMax" %>

using System;
using System.Web.SessionState;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SB.DBLibrary;
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.Agent.Common;
using SB.Common.Files;
public class DoUpdDefaultMinMax : IHttpHandler, IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!PageBase.BaseValidate())
        {
            context.Response.Write("Validate Error !");
            return;
        }
        if ( !Agents.MemberMgr)
        {
            return;
        }

        #region Update Min Max
       
        DBbase connMain = new DBbase(SiteSetting.MainConnString);
        for (int i = 0; i < context.Request.Params.Count; i+=2)
        {
            if (!context.Request.Params.Keys[i].StartsWith("DefaultMin"))
            {
                continue;
            }
            string Provider = context.Request.Params.Keys[i].Split('_')[1];
            string GameType = context.Request.Params.Keys[i].Split('_')[2];
            string Min = context.Request.Params[i];
            string Max = context.Request.Params[i+1];
            connMain.ExecuteByParameters("New_Age_InsUpd_DefaultMinMax", new SqlParameter("@Provider", Convert.ToInt32(Provider))
                                                                    , new SqlParameter("@GameType", Convert.ToInt32(GameType))
                                                                    , new SqlParameter("@MinBet", Convert.ToDecimal(Min))
                                                                    , new SqlParameter("@MaxBet", Convert.ToDecimal(Max)));
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