using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using aspxtemplate;
using member_site;
public partial class DepositMain : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!member_site.Auth.ValidateLogin(this.Context)) return;
        if (Session["ServerInfo_key"] == null) return;
        #region Load Template

        string SkinRootPath = SelLang.GetSkinRootPath();
        PageParser.SetTemplatesDir(SkinRootPath + "public/");
        PageParser.SetTemplateFile("DepositMain.html");
        #endregion
    }
}