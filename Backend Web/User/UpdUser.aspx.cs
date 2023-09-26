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
public partial class User_UpdUser : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int CustID = 0;
        string Firstname = "";
        string Lastname = "";
        string Phone="";
        string EMail = "";
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
        Int32.TryParse(Request["ToCID"], out CustID);
        if (CustID == 0)
        {
            return;
        }
        Firstname = string.IsNullOrEmpty(Request["txtFirstname"]) ? "" : Request["txtFirstname"];
        Lastname = string.IsNullOrEmpty(Request["txtLastname"]) ? "" : Request["txtLastname"];
        
        Phone = string.IsNullOrEmpty(Request["txtPhone"]) ? "" : Request["txtPhone"];
        EMail = string.IsNullOrEmpty(Request["txtEMail"]) ? "" : Request["txtEMail"];
        chkMember = string.IsNullOrEmpty(Request["chkMember"]) ? "0" : "1";
        chkBank = string.IsNullOrEmpty(Request["chkBank"]) ? "0" : "1";
        chkTotalBets = string.IsNullOrEmpty(Request["chkTotalBets"]) ? "0" : "1";
        chkReport = string.IsNullOrEmpty(Request["chkReports"]) ? "0" : "1";
        chkSetting = string.IsNullOrEmpty(Request["chkSetting"]) ? "0" : "1";
        Permissions = chkMember + "," + chkBank + "," + chkTotalBets + "," + chkReport + "," + chkSetting;

        try
        {
            DataTable dt_Result = new DataTable();
            string SpName = "";

            SpName = "New_Age_UserUpd";


            DBbase connMain = new DBbase(SiteSetting.MainConnString);

            connMain.ExecuteByParameters(SpName, ref dt_Result, new SqlParameter("@custid", CustID)
                                                                , new SqlParameter("@firstname", Firstname)
                                                                , new SqlParameter("@lastname", Lastname)
                                                                , new SqlParameter("@phone", Phone)
                                                                , new SqlParameter("@email", EMail)
                                                                  , new SqlParameter("@permissions", Permissions)
                                                                );
            string UserLan = Agents.Language;
            string Result = Skin.GetResValue(UserLan, "lbl_UpdateSuccess");
            Response.Write("ok");

        }
        catch (Exception ex)
        {
            Logger.LogException(ex, "New_Age_UserUpd Error");
            throw ex;
        }

    }
}