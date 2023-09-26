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
public partial class Bank_UpdBank : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int BankID = 0;
        string BankCode = "";
        string BankName = "";
        string BankAccount = "";
        string selStatus = "";



        if (!Agents.BankMgr)
        {
            return;
        }
        Int32.TryParse(Request["ToBankID"], out BankID);
        if (BankID == 0 || string.IsNullOrEmpty(Request["txtBankName"]) || string.IsNullOrEmpty(Request["txtBankAccount"]))
        {
            return;
        }

        BankCode = string.IsNullOrEmpty(Request["txtBankCode"]) ? "" : Request["txtBankCode"];
        BankName = string.IsNullOrEmpty(Request["txtBankName"]) ? "" : Request["txtBankName"];

        BankAccount = string.IsNullOrEmpty(Request["txtBankAccount"]) ? "" : Request["txtBankAccount"];
        selStatus = string.IsNullOrEmpty(Request["selStatus"]) ? "0" : Request["selStatus"];


        try
        {
            DataTable dt_Result = new DataTable();
            string SpName = "";

            SpName = "New_Age_BankUpd";


            DBbase connMain = new DBbase(SiteSetting.MainConnString);

            connMain.ExecuteByParameters(SpName, ref dt_Result, new SqlParameter("@bankid", BankID)
                                                                , new SqlParameter("@BankCode", BankCode)
                                                                , new SqlParameter("@BankName", BankName)
                                                                , new SqlParameter("@BankAccount", BankAccount)
                                                                , new SqlParameter("@enabled", Convert.ToInt32(selStatus))
                                                             
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