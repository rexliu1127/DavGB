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
public partial class Withdrawal : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!member_site.Auth.ValidateLogin(this.Context)) return;
        if (Session["ServerInfo_key"] == null) return;
        #region Load Template

        string SkinRootPath = SelLang.GetSkinRootPath();
        PageParser.SetTemplatesDir(SkinRootPath + "public/");
        PageParser.SetTemplateFile("Withdrawal.html");
        #endregion
                member_site.SelLang selLan = new member_site.SelLang(Auth.GetUserLang(this.Context));
        PageParser.SetVariable("SkinRootPath", SkinRootPath);
        PageParser.SetVariable("lbl_WithdrawalFromLocal", selLan.GetResourceValue("lbl_WithdrawalFromLocal"));
        PageParser.SetVariable("lbl_WQuickTransferNoteBody", selLan.GetResourceValue("lbl_WQuickTransferNoteBody"));
        PageParser.SetVariable("lbl_QuickTransfer", selLan.GetResourceValue("lbl_QuickTransfer"));
        PageParser.SetVariable("lbl_AmountIn", selLan.GetResourceValue("lbl_AmountIn"));
        PageParser.SetVariable("lbl_submit", selLan.GetResourceValue("lbl_submit"));
        PageParser.SetVariable("lbl_BankName", selLan.GetResourceValue("lbl_BankName"));
                PageParser.SetVariable("lbl_WBankAccountName", selLan.GetResourceValue("lbl_WBankAccountName"));
        PageParser.SetVariable("lbl_WBankAccountNo", selLan.GetResourceValue("lbl_WBankAccountNo"));
        PageParser.SetVariable("lbl_BranchName", selLan.GetResourceValue("lbl_BranchName"));
        PageParser.SetVariable("lbl_EnterValidAmount", selLan.GetResourceValue("lbl_EnterValidAmount"));
        PageParser.SetVariable("lbl_enterbankname", selLan.GetResourceValue("lbl_enterbankname"));
        PageParser.SetVariable("lbl_enteryourbankaccountname", selLan.GetResourceValue("lbl_enteryourbankaccountname"));
        PageParser.SetVariable("lbl_enteryourbankaccountNo", selLan.GetResourceValue("lbl_enteryourbankaccountNo"));
        PageParser.SetVariable("lbl_enterbankbranchname", selLan.GetResourceValue("lbl_enterbankbranchname"));
        PageParser.SetVariable("lbl_Confirmsubmitthisrequest", selLan.GetResourceValue("lbl_Confirmsubmitthisrequest"));

        DBbase conn = new DBbase(member_site.Common.ConnectionString);
        string FavBankInfo = "";
        string BankNane = "";
        string BranchName = "";
        string FavAccountName = "";
        string FavAccountNo = "";
        DataTable dt_FavAcc = new DataTable();

        dt_FavAcc = conn.ExecSPbyParamsWithDataTable("New_Dot_Member_GetFavWithdrawAcc", new SqlParameter("@CustId", Auth.GetUserID(HttpContext.Current)));
        FavBankInfo = Convert.IsDBNull(dt_FavAcc.Rows[0]["FavBankName"]) ? "" : dt_FavAcc.Rows[0]["FavBankName"].ToString();
        FavAccountName = Convert.IsDBNull(dt_FavAcc.Rows[0]["FavAccountName"]) ? "" : dt_FavAcc.Rows[0]["FavAccountName"].ToString();
        FavAccountNo = Convert.IsDBNull(dt_FavAcc.Rows[0]["FavAccountNo"]) ? "" : dt_FavAcc.Rows[0]["FavAccountNo"].ToString();
        if (!string.IsNullOrEmpty(FavBankInfo))
        { 
            string[] Bank=FavBankInfo.Split('-');
            BankNane = Bank[0];
            BranchName = Bank[1];
        }
        PageParser.SetVariable("YourBankName", BankNane);
        PageParser.SetVariable("BranchName", BranchName);
        PageParser.SetVariable("BankAccountName", FavAccountName);
        PageParser.SetVariable("BankAccountNo", FavAccountNo);
    }
} 
