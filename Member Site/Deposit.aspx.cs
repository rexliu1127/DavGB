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
public partial class Deposit : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!member_site.Auth.ValidateLogin(this.Context)) return;
        if (Session["ServerInfo_key"] == null) return;
        #region Load Template

        string SkinRootPath = SelLang.GetSkinRootPath();
        PageParser.SetTemplatesDir(SkinRootPath + "public/");
        PageParser.SetTemplateFile("deposit.html");
        #endregion
        member_site.SelLang selLan = new member_site.SelLang(Auth.GetUserLang(this.Context));
        PageParser.SetVariable("SkinRootPath", SkinRootPath);
        PageParser.SetVariable("lbl_DepositFrom", selLan.GetResourceValue("lbl_DepositFrom"));
        PageParser.SetVariable("lbl_QuickTransfer", selLan.GetResourceValue("lbl_QuickTransfer"));
        PageParser.SetVariable("lbl_Alipay", selLan.GetResourceValue("lbl_Alipay"));
        
 
    }
}