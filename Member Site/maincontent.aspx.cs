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

public partial class maincontent : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!member_site.Auth.IsLegalRequest(Context.Session)) return;

  


        string DomainName = member_site.Common.GetDomainName(Context);



        try
        {
            #region Load Template
            string skinroot_path = member_site.SelLang.GetSkinRootPath();
            string sLang = Auth.GetUserLang(this.Context);
            string skin_path = member_site.SelLang.GetSkinPath(Context);
            string strDomain = member_site.Common.GetDomainName(this.Context);
            //---- BEGIN load template file ---------------
            PageParser.SetTemplatesDir(skinroot_path + "public/");
            PageParser.SetTemplateFile("maincontent.html");
            PageParser.SetVariable("skinpath", skin_path);
            PageParser.SetVariable("skinrootpath", skinroot_path);

            PageParser.SetVariable("lbl_WC2010", member_site.SelLang.GetResourceValue(Context, "lbl_WC2010"));
            PageParser.SetVariable("lbl_DP_TodayFeatureGames", member_site.SelLang.GetResourceValue(Context, "lbl_DP_TodayFeatureGames"));
            PageParser.SetVariable("lbl_DP_InRunningOffering", member_site.SelLang.GetResourceValue(Context, "lbl_DP_InRunningOffering"));
            PageParser.SetVariable("lang", sLang);
            PageParser.SetVariable("DomainName", DomainName);
	    PageParser.SetVariable("Msg_CntChangeToEuro", SelLang.GetMsgResourceValue(this.Context, "msg_CntChangeToEurope"));



            if (sLang == "cs")
            {
                PageParser.SetVariable("2009_01bonusDisplay", "");
            }
            else
            {
                PageParser.SetVariable("2009_01bonusDisplay", "style=display:none");
            }

          

 

            //--- END load template file -----
            #endregion
        }
        catch (NullReferenceException ex)
        {
            //Response.Write("<script>window.location.replace('index.aspx');</script>");
            string errlog = ex.Source + "  " + ex.Message;
           
        }
    }
}
