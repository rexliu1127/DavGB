using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SB.Agent.Common;
using SB.Agent.Sites;
using SB.Agent.Users;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SB.DBLibrary;
using SB.Common.Files;
public partial class Bank_InsBank : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Agents.BankMgr)
        {
            return;
        }
        string BankCode = "";
        string BankName = "";
        string Account = "";
        if (string.IsNullOrEmpty(Request["txtBankName"]) || string.IsNullOrEmpty(Request["txtBankAccount"]))
        {
            return;
        }
        BankCode = string.IsNullOrEmpty(Request["txtBankCode"]) ? "" : Request["txtBankCode"];
        BankName = Request["txtBankName"];
        Account = Request["txtBankAccount"];

        try
        {
            DataTable dt_Result = new DataTable();
            string SpName = "";

            SpName = "New_Age_BankIns";


            DBbase connMain = new DBbase(SiteSetting.MainConnString);

            connMain.ExecuteByParameters(SpName, ref dt_Result, new SqlParameter("@BankCode", BankCode)
                                                                , new SqlParameter("@BankName", BankName)
                                                                , new SqlParameter("@BankAccount", Account)

                                                                );
    
            string UserLan = Agents.Language;
            string Result = Skin.GetResValue(UserLan, "lbl_UpdateSuccess");
            Response.Write("<script>alert('" + Result + "')</script>");

        }
        catch (Exception ex)
        {
            Logger.LogException(ex, "New_Age_UserIns Error");
            throw ex;
        }
    }
}