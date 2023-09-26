using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using aspxtemplate;
using System.Data;
using System.Data.SqlClient;
using SB.DBLibrary;
using SB.Common.Files;
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.Agent.Common;
using System.Text;
public partial class Bank_ListBank : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String UserLan = Agents.Language;
        string OddClolr = "tdbg1";
        string EvenColor = "tdbg2";
        int ToUserID = 0;


        if (!Agents.BankMgr)
        {
            return;
        }


        #region Load template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("ListBank.html");
        #endregion
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("lbl_ListBank", Skin.GetResValue(UserLan, "lbl_ListBank"));
       
        
        PageParser.SetVariable("lbl_Edit", Skin.GetResValue(UserLan, "lbl_Edit"));
        PageParser.SetVariable("lbl_Status", Skin.GetResValue(UserLan, "lbl_Status"));
        PageParser.SetVariable("lbl_BankCode", Skin.GetResValue(UserLan, "lbl_BankCode"));
        PageParser.SetVariable("lbl_BankName", Skin.GetResValue(UserLan, "lbl_BankName"));
        PageParser.SetVariable("lbl_BankAccount", Skin.GetResValue(UserLan, "lbl_BankAccount"));
        string BankSP = "";
        string OpenEditUrl = "";
        BankSP = "New_Dot_Age_GetBank";
        OpenEditUrl = "Bank/EditBank.aspx";
        PageParser.SetVariable("OpenEditUrl", OpenEditUrl);
        DataTable dt_Bank = new DataTable();
        DBbase conn = new DBbase(SiteSetting.MainConnString);
        conn.ExecuteByParameters("dbo.New_Dot_Age_GetBank", ref dt_Bank, null);
        PageParser.UpdateBlock("BankBlock");
        if (dt_Bank.Rows.Count == 0)
        {
            PageParser.SetVariable("SubAccDataOutput", "");
            return;
        }

        StringBuilder sbOutput = new StringBuilder();
        for (int i = 1; i <= dt_Bank.Rows.Count; i++)
        {
            DataRow dr = dt_Bank.Rows[i - 1];

            #region Set Tr Interval Color
            if (i % 2 == 0)
            {
                PageParser.SetVariable("IntervalColor", EvenColor);
            }
            else
            {
                PageParser.SetVariable("IntervalColor", OddClolr);
            }
            #endregion

            PageParser.SetVariable("Number", i.ToString());
            if (dr["Enabled"].ToString() == "1")
            {
                PageParser.SetVariable("Status", Skin.GetResValue(UserLan, "lbl_Open"));
            }
            else
            {
                PageParser.SetVariable("Status", Skin.GetResValue(UserLan, "lbl_Closed"));
            }
            PageParser.SetVariable("BankCode", Convert.IsDBNull(dr["BankCode"]) ? "" : dr["BankCode"].ToString());
            PageParser.SetVariable("BankName", Convert.IsDBNull(dr["Name"]) ? "" : dr["Name"].ToString());
            PageParser.SetVariable("BankAccount", Convert.IsDBNull(dr["Account"]) ? "" : dr["Account"].ToString());
            PageParser.SetVariable("ToBankID", Convert.IsDBNull(dr["BankID"]) ? "" : dr["BankID"].ToString());
            PageParser.SetVariable("CurrentAgentID",Agents.AgentID.ToString());
            sbOutput.Append(PageParser.getblock("BankBlock"));
        }
        PageParser.SetVariable("BankDataOutput", sbOutput.ToString());
    }
}