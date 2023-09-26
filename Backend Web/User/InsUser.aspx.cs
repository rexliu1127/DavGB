using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.Agent.Common;
using SB.DBLibrary;
using SB.Common.Files;
public partial class User_InsUser : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserName = "";
        string PWD = "";
        string FirstName = "";
        string LastName = "";
        string Phone = "";
        string EMail="";
        string chkMember = "";
        string chkBank = "";
        string chkReport = "";
        string chkTotalBets = "";
        string chkSetting = "";
        string Permissions = "";


        if (!Agents.UserMgr)
        {
            return;
        }
        #region Get Request Params

        if (string.IsNullOrEmpty(Request["txtUserName"]) || string.IsNullOrEmpty(Request["txthidUserpwd"]))
        {
                return;
        }
        UserName = Request["txtUserName"];
        PWD = Request["txthidUserpwd"];
        FirstName = string.IsNullOrEmpty(Request["txtFirstname"]) ? "" : Request["txtFirstname"];
        LastName = string.IsNullOrEmpty(Request["txtLastname"]) ? "" : Request["txtLastname"];
        Phone = string.IsNullOrEmpty(Request["txtPhone"]) ? "" : Request["txtPhone"];
        EMail = string.IsNullOrEmpty(Request["txtMail"]) ? "" : Request["txtMail"];
        chkMember = string.IsNullOrEmpty(Request["chkMember"]) ? "0" : "1";
        chkBank = string.IsNullOrEmpty(Request["chkBank"]) ? "0" : "1";
        chkTotalBets = string.IsNullOrEmpty(Request["chkTotalBets"]) ? "0" : "1";
        chkReport = string.IsNullOrEmpty(Request["chkReports"]) ? "0" : "1";
        chkSetting = string.IsNullOrEmpty(Request["chkSetting"]) ? "0" : "1";
        Permissions = chkMember + "," + chkBank + "," + chkTotalBets + "," + chkReport + "," + chkSetting;
        #endregion

        try
        {
            DataTable dt_Result = new DataTable();
            string SpName = "";
            
            SpName = "New_Age_UserIns";

           
            DBbase connMain = new DBbase(SiteSetting.MainConnString);

            connMain.ExecuteByParameters(SpName, ref dt_Result, new SqlParameter("@firstname", FirstName)
                                                                , new SqlParameter("@lastname", LastName)
                                                                , new SqlParameter("@phone", Phone)
                                                                , new SqlParameter("@email", EMail)
                                                                , new SqlParameter("@username", UserName)
                                                                  , new SqlParameter("@userpwd", PWD)
                                                                  , new SqlParameter("@permissions", Permissions)
                                                                );
            int ResultCode = Convert.ToInt32(dt_Result.Rows[0]["Result"]);
            string UserLan = Agents.Language;
            string Result = Skin.GetResValue(UserLan, "lbl_UpdateSuccess");
            if (ResultCode == 1001)
            {
                Result = Skin.GetResValue(UserLan, "lbl_UserName_Exist");
            }
            Response.Write("<script>alert('" + Result + "')</script>");

        }
        catch (Exception ex)
        {
            Logger.LogException(ex, "New_Age_UserIns Error");
            throw ex;
        }

    }
}