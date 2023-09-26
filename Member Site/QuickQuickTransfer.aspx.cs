using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using aspxtemplate;
using member_site;

public partial class QuickQuickTransfer : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
               if (!member_site.Auth.ValidateLogin(this.Context)) return;
        if (Session["ServerInfo_key"] == null) return;
        #region Load Template

        string SkinRootPath = SelLang.GetSkinRootPath();
        PageParser.SetTemplatesDir(SkinRootPath + "public/");
        PageParser.SetTemplateFile("QuickQuickTransfer.html");
        #endregion
        member_site.SelLang selLan = new member_site.SelLang(Auth.GetUserLang(this.Context));
        PageParser.SetVariable("SkinRootPath", SkinRootPath);
        PageParser.SetVariable("lbl_QuickTransfer", selLan.GetResourceValue("lbl_QuickTransfer"));
        
        PageParser.SetVariable("lbl_QuickTransferNoteTitle", selLan.GetResourceValue("lbl_QuickTransferNoteTitle"));
        PageParser.SetVariable("lbl_QuickTransferNoteBody", selLan.GetResourceValue("lbl_QuickTransferNoteBody"));
        PageParser.SetVariable("lbl_QuickTransferNoteBody", selLan.GetResourceValue("lbl_QuickTransferNoteBody"));
        PageParser.SetVariable("lbl_QuickTransferType1", selLan.GetResourceValue("lbl_PaymentType1"));
        PageParser.SetVariable("lbl_QuickTransferType2", selLan.GetResourceValue("lbl_PaymentType2"));
        PageParser.SetVariable("lbl_QuickTransferType3", selLan.GetResourceValue("lbl_PaymentType3"));
        PageParser.SetVariable("lbl_QuickTransferType4", selLan.GetResourceValue("lbl_PaymentType4"));
        PageParser.SetVariable("lbl_AmountIn", selLan.GetResourceValue("lbl_AmountIn"));
        PageParser.SetVariable("lbl_ReferenceNoATM", selLan.GetResourceValue("lbl_ReferenceNoATM"));
        PageParser.SetVariable("lbl_BankAccount", selLan.GetResourceValue("lbl_BankAccount"));
        PageParser.SetVariable("lbl_DepositChannel", selLan.GetResourceValue("lbl_DepositChannel"));
        PageParser.SetVariable("lbl_DateTime", selLan.GetResourceValue("lbl_DateTime"));
        PageParser.SetVariable("lbl_YourBankName", selLan.GetResourceValue("lbl_YourBankName"));
        PageParser.SetVariable("lbl_BankAccountName", selLan.GetResourceValue("lbl_BankAccountName"));
        PageParser.SetVariable("lbl_BankAccountNo", selLan.GetResourceValue("lbl_BankAccountNo"));
        PageParser.SetVariable("lbl_submit", selLan.GetResourceValue("lbl_submit"));

        PageParser.SetVariable("lbl_EnterValidAmount", selLan.GetResourceValue("lbl_EnterValidAmount"));
        PageParser.SetVariable("lbl_enterbankname", selLan.GetResourceValue("lbl_enterbankname"));
        PageParser.SetVariable("lbl_enteryourbankaccountname", selLan.GetResourceValue("lbl_enteryourbankaccountname"));
        PageParser.SetVariable("lbl_enteryourbankaccountNo", selLan.GetResourceValue("lbl_enteryourbankaccountNo"));
       PageParser.SetVariable("lbl_enteravalidreferencenumber", selLan.GetResourceValue("lbl_enteravalidreferencenumber"));
       PageParser.SetVariable("lbl_Confirmsubmitthisrequest", selLan.GetResourceValue("lbl_Confirmsubmitthisrequest"));
       PageParser.SetVariable("valToday", DateTime.Today.ToString("MM/dd/yyyy"));
        
        PageParser.UpdateBlock("SelHour");
        string Hour="";
        for (int i = 0; i <= 23; i++)
        {
            if (i < 10)
            {
                Hour = "0" + i.ToString();
            }
            else
            {
                Hour = i.ToString();
            }
            PageParser.SetVariable("valHour", Hour);
            PageParser.SetVariable("txtHour", Hour);
            PageParser.ParseBlock("SelHour");
        }

        PageParser.UpdateBlock("SelMinute");
        string Minute = "";
        for (int i = 0; i <= 59; i++)
        {
            if (i < 10)
            {
                Minute = "0" + i.ToString();
            }
            else
            {
                Minute = i.ToString();
            }
            PageParser.SetVariable("valMinute", Minute);
            PageParser.SetVariable("txtMinute", Minute);
            PageParser.ParseBlock("SelMinute");
        }
        DataTable dt_Bank = new DataTable();
        DBbase conn = new DBbase(member_site.Common.ConnectionString);
        dt_Bank = conn.ExecuteQuery("dbo.Dot_GetBank");
      
        PageParser.UpdateBlock("SelBankAccount");
        for (int i = 0; i < dt_Bank.Rows.Count; i++)
        {

            PageParser.SetVariable("valBankAccount", dt_Bank.Rows[i]["BankId"].ToString());
            PageParser.SetVariable("txtBankAccount", dt_Bank.Rows[i]["Name"].ToString() + " - " + dt_Bank.Rows[i]["Account"].ToString());
            PageParser.ParseBlock("SelBankAccount");
        }
        string FavBankName = "";
        string FavAccountName = "";
        string FavAccountNo = "";
        DataTable dt_FavAcc = new DataTable();

        dt_FavAcc = conn.ExecSPbyParamsWithDataTable("New_Dot_Member_GetFavDepositAcc",new SqlParameter("@CustId",Auth.GetUserID(HttpContext.Current)));
        FavBankName = Convert.IsDBNull(dt_FavAcc.Rows[0]["FavBankName"]) ? "" : dt_FavAcc.Rows[0]["FavBankName"].ToString();
        FavAccountName = Convert.IsDBNull(dt_FavAcc.Rows[0]["FavAccountName"]) ? "" : dt_FavAcc.Rows[0]["FavAccountName"].ToString();
        FavAccountNo = Convert.IsDBNull(dt_FavAcc.Rows[0]["FavAccountNo"]) ? "" : dt_FavAcc.Rows[0]["FavAccountNo"].ToString();
        PageParser.SetVariable("YourBankName", FavBankName);
        PageParser.SetVariable("BankAccountName", FavAccountName);
        PageParser.SetVariable("BankAccountNo", FavAccountNo);
    }
}