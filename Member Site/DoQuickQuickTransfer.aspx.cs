using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using member_site;
using System.Data;
using System.Data.SqlClient;
public partial class DoQuickQuickTransfer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        double AmountIn = 0;
        string errMag = "";
        string ReferenceNo = "";
        string BankAccount="";
        string DepositTime = "";
        int DepositChannel = 0;
       
        string YourBankName = "";
        string BankAccountName = "";
        string BankAccountNo="";
        DBbase conn = new DBbase(member_site.Common.ConnectionString);

        if (!member_site.Auth.ValidateLogin(this.Context)) return;
        if (Session["ServerInfo_key"] == null) return;
        member_site.SelLang selLan = new member_site.SelLang(Auth.GetUserLang(this.Context));
        if(! Double.TryParse(Request["txtAmountIn"], out AmountIn)
            || AmountIn==0)
        {
            errMag = selLan.GetResourceValue("lbl_EnterValidAmount");
        }
        if (string.IsNullOrEmpty(Request["txtReferenceNoATM"]))
        {
            errMag = selLan.GetResourceValue("lbl_enteravalidreferencenumber");
        }
        ReferenceNo = Request["txtReferenceNoATM"];
        BankAccount = Request["selBankAccount"];
        DepositTime = Request["s_date"] + " " + Request["selHour"] + ":" + Request["selMinute"];
        int.TryParse(Request["selDepositChannel"], out DepositChannel);

        if (string.IsNullOrEmpty(Request["txtYourBankName"]))
        {
            errMag = selLan.GetResourceValue("lbl_enterbankname");
        }
        YourBankName = Request["txtYourBankName"];


        if (string.IsNullOrEmpty(Request["txtBankAccountName"]))
        {
            errMag = selLan.GetResourceValue("lbl_enteryourbankaccountname");
        }
        BankAccountName = Request["txtBankAccountName"];

        if (string.IsNullOrEmpty(Request["txtBankAccountNo"]))
        {
            errMag = selLan.GetResourceValue("lbl_enteryourbankaccountNo");
        }
        BankAccountNo = Request["txtBankAccountNo"];

        DataTable dt_Bank = new DataTable();
        dt_Bank = conn.ExecSPbyParamsWithDataTable("dbo.Dot_GetBank", new SqlParameter("@BankID", BankAccount));
        if (dt_Bank.Rows.Count == 0)
        {
            errMag = selLan.GetResourceValue("lbl_enterbankname");
        }

        if (string.IsNullOrEmpty(errMag))
        {
            try
            {
                conn.ExecuteSPbyParameters("dbo.Dot_Cust_Transaction_Transfer_Ins"
                                                , new SqlParameter("@CustID", Auth.GetUserID(HttpContext.Current))
                                                , new SqlParameter("@TransType", 1)
                                                , new SqlParameter("@PaymentType", DepositChannel)
                                                , new SqlParameter("@Amount", AmountIn)
                                                , new SqlParameter("@BankAccount", dt_Bank.Rows[0]["Account"].ToString())
                                                , new SqlParameter("@ReferenceNo", ReferenceNo)
                                                , new SqlParameter("@TransTime", DepositTime)
                                                , new SqlParameter("@CBankName", YourBankName)
                                                , new SqlParameter("@CBankAccountName", BankAccountName)
                                                , new SqlParameter("@CBankAccountNo", BankAccountNo));

            }
            catch (Exception ex)
            {
                errMag = selLan.GetResourceValue("lbl_SystemError");
                Logger.LogException(ex, "Dot_Cust_Transaction_Transfer_Ins Error");
               
            }
        }

        if (!string.IsNullOrEmpty(errMag))
        {
            Response.Write("<script>alert('" + errMag + "')</script>");

        }
        else
        {
            Response.Write("<script>alert('" + selLan.GetResourceValue("lbl_deposit_success") + "')</script>");
        }
        Response.Write("<script>window.top.location.href='DepositMain.aspx'</script>");

    }
}