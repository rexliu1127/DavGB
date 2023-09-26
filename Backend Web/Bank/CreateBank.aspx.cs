using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SB.Agent.Common;
using SB.Agent.Sites;
using SB.Agent.Users;
using SB.DBLibrary;
using SB.Common.Files;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class Bank_CreateBank : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {


        String UserLan = Agents.Language;

        if (!Agents.BankMgr)
        {
            return;
        }
        #region Load template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));

        PageParser.SetTemplateFile("CreateBank.html");

        #endregion
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("lbl_CreateBank", Skin.GetResValue(UserLan, "lbl_CreateBank"));
        PageParser.SetVariable("lbl_BankCode", Skin.GetResValue(UserLan, "lbl_BankCode"));
        PageParser.SetVariable("lbl_BankName", Skin.GetResValue(UserLan, "lbl_BankName"));
        PageParser.SetVariable("lbl_BankAccount", Skin.GetResValue(UserLan, "lbl_BankAccount"));
        PageParser.SetVariable("lbl_submit", Skin.GetResValue(UserLan, "lbl_submit"));
        PageParser.SetVariable("lbl_PleaseInput", Skin.GetResValue(UserLan, "lbl_PleaseInput"));
    }
}