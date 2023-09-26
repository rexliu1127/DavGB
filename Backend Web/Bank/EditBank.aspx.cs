using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using aspxtemplate;
using System.Data;
using System.Data.SqlClient;
using SB.DBLibrary;
using SB.Common.Files;
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.Agent.Common;

public partial class Bank_EditBank : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String UserLan = Agents.Language;

        int  BankID = 0;


        if (!Agents.BankMgr)
        {
            return;
        }
        int.TryParse(Request["ToBankID"], out BankID);
        if (BankID==0)
        {
            return;
        }

        #region Load template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("EditBank.html");
        #endregion

        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("UserLan", UserLan);

        PageParser.SetVariable("lbl_EditBank", Skin.GetResValue(UserLan, "lbl_EditBank"));
        PageParser.SetVariable("lbl_cancel", Skin.GetResValue(UserLan, "lbl_cancel"));
        PageParser.SetVariable("lbl_submit", Skin.GetResValue(UserLan, "lbl_submit"));
        PageParser.SetVariable("lbl_BankCode", Skin.GetResValue(UserLan, "lbl_BankCode"));
        PageParser.SetVariable("lbl_BankName", Skin.GetResValue(UserLan, "lbl_BankName"));
        PageParser.SetVariable("lbl_BankAccount", Skin.GetResValue(UserLan, "lbl_BankAccount"));
        PageParser.SetVariable("lbl_Status", Skin.GetResValue(UserLan, "lbl_Status"));
        PageParser.SetVariable("lbl_Open", Skin.GetResValue(UserLan, "lbl_Open"));
        PageParser.SetVariable("lbl_Closed", Skin.GetResValue(UserLan, "lbl_Closed"));
        PageParser.SetVariable("lbl_PleaseInput", Skin.GetResValue(UserLan, "lbl_PleaseInput"));
        PageParser.SetVariable("ToBankID", BankID.ToString());
        DataTable dt_Bank = new DataTable();
        DBbase conn = new DBbase(SiteSetting.MainConnString);
        conn.ExecuteByParameters("dbo.New_Dot_Age_GetBankByID", ref dt_Bank, new SqlParameter("@BankID", BankID));
        PageParser.SetVariable("BankCode", Convert.IsDBNull(dt_Bank.Rows[0]["BankCode"]) ? "" : dt_Bank.Rows[0]["BankCode"].ToString());
        PageParser.SetVariable("Name", Convert.IsDBNull(dt_Bank.Rows[0]["Name"]) ? "" : dt_Bank.Rows[0]["Name"].ToString());
        PageParser.SetVariable("BankAccount", Convert.IsDBNull(dt_Bank.Rows[0]["Account"]) ? "" : dt_Bank.Rows[0]["Account"].ToString());
        if (dt_Bank.Rows[0]["Enabled"].ToString() == "1")
        {
            PageParser.SetVariable("Open", "checked");
            PageParser.SetVariable("Closed", "");
        }
        else
        {
            PageParser.SetVariable("Open", "");
            PageParser.SetVariable("Closed", "checked");
        }
    }
}