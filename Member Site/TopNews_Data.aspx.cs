using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using member_site;
using aspxtemplate;


public partial class TopNews_Data : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ServerInfo_key"] == null) return;

        string webname = Common.GetDomainName(Context);

        #region get top new message -----------------------------------------------
        string showmsg = "";
        string strUserLanguage = member_site.Auth.GetUserLang(this.Context);

            showmsg = SelLang.GetResourceValue(HttpContext.Current, "lbl_Advice");
            //---- BEGIN load template file ---------------
            //string SkinRootPath = SelLang.GetSkinRootPath();
            PageParser.SetTemplatesDir("template/public/");
            PageParser.SetTemplateFile("TopNews_Data.html");
            //--- END load template file -----

            PageParser.SetVariable("pubmsg", showmsg);
            PageParser.SetVariable("primsg", "");
            PageParser.SetVariable("lbl_private_message", "");
        

        #endregion --------------------------------------------------------------------------------
    }

 

    private string GetMsg(bool ispublic)
    {


        #region get top new message -----------------------------------------------
        string showmsg = "";
        string strUserLanguage = member_site.Auth.GetUserLang(this.Context);


        #endregion --------------------------------------------------------------------------------
        return showmsg;



    }
}
