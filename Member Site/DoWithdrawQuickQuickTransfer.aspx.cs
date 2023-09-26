using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using member_site;
using System.Data;
using System.Data.SqlClient;
public partial class DoWithdrawQuickQuickTransfer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        double AmountIn = 0;
        string errMag = "";
        string BankName = "";
        string BranchName="";
        string BankAccountName = "";
        string BankAccountNo = "";
        DBbase conn = new DBbase(member_site.Common.ConnectionString);

        if (!member_site.Auth.ValidateLogin(this.Context)) return;
        if (Session["ServerInfo_key"] == null) return;
        member_site.SelLang selLan = new member_site.SelLang(Auth.GetUserLang(this.Context));
        if (!Double.TryParse(Request["txtAmountIn"], out AmountIn)
            || AmountIn == 0)
        {
            errMag = selLan.GetResourceValue("lbl_EnterValidAmount");
        }
        if (string.IsNullOrEmpty(Request["txtBankAccountName"]))
        {
            errMag = selLan.GetResourceValue("lbl_enteryourbankaccountname");
        }
        if (string.IsNullOrEmpty(Request["txtBankAccountNo"]))
        {
            errMag = selLan.GetResourceValue("lbl_enteryourbankaccountNo");
        }
        BankName = Request["txtBankName"];
        BranchName = Request["txtBranchName"];
        BankAccountName = Request["txtBankAccountName"];
        BankAccountNo = Request["txtBankAccountNo"];



        if (string.IsNullOrEmpty(errMag))
        {
            try
            {
                conn.ExecuteSPbyParameters("dbo.Dot_Cust_Withdraw_Transaction_Transfer_Ins"
                                                , new SqlParameter("@CustID", Auth.GetUserID(HttpContext.Current))
                                                , new SqlParameter("@TransType", 2)
                                                , new SqlParameter("@PaymentType", 1)
                                                , new SqlParameter("@Amount", AmountIn)

                                                , new SqlParameter("@TransTime", DateTime.Now.ToString())
                                                , new SqlParameter("@CBankName", BankName+"-"+BranchName)
                                                , new SqlParameter("@CBankAccountName", BankAccountName)
                                                , new SqlParameter("@CBankAccountNo", BankAccountNo));

            }
            catch (Exception ex)
            {
                errMag = selLan.GetResourceValue("lbl_SystemError");
                Logger.LogException(ex, "Dot_Cust_Withdraw_Transaction_Transfer_Ins Error");

            }
        }

        if (!string.IsNullOrEmpty(errMag))
        {
            Response.Write("<script>alert('" + errMag + "')</script>");

        }
        else
        {
            Response.Write("<script>alert('" + selLan.GetResourceValue("lbl_Withdrawal_cdcNote5") + "')</script>");
        }
        Response.Write("<script>parent.window.location.href='Withdrawal.aspx'</script>");
    }
}