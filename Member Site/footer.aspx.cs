using aspxtemplate;

using member_site;

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

public partial class footer : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Auth.IsLegalRequest(Session)) return;
        #region Load template
        string skin_path = SelLang.GetSkinPath(this.Context);
        string SkinRootPath = SelLang.GetSkinRootPath();
        member_site.SelLang sel = new member_site.SelLang(Auth.GetUserLang(this.Context));




            PageParser.SetVariable("lbl_CareersPath", sel.GetResourceValue("lbl_CareersPath"));
        

        //SkinRootPath = "template/deposit/";
        PageParser.SetTemplatesDir(SkinRootPath + "public/");
        PageParser.SetTemplateFile("footer.html");
        PageParser.SetVariable("skinpath", skin_path);
        PageParser.SetVariable("SkinRootPath", SkinRootPath);

        PageParser.SetVariable("DomainName", Common.GetDomainName(this.Context).ToLower().Trim());
        PageParser.SetVariable("lbl_PrivacyPolicy", sel.GetResourceValue("lbl_PrivacyPolicy"));
         PageParser.SetVariable("lbl_MsgCopyright", sel.GetResourceValue("lbl_MsgCopyright"));
        
        PageParser.SetVariable("lbl_Termsofuse", sel.GetResourceValue("lbl_Termsofuse"));        
        PageParser.SetVariable("lbl_Termandconditions", sel.GetResourceValue("lbl_Termandconditions"));
        PageParser.SetVariable("lbl_ResponsibleGaming", sel.GetResourceValue("lbl_ResponsibleGaming"));
        PageParser.SetVariable("lbl_Sitemap", sel.GetResourceValue("lbl_Sitemap"));
        PageParser.SetVariable("lbl_Recommended_Browsers_Screen Resolution", sel.GetResourceValue("lbl_Recommended_Browsers_Screen Resolution"));
        PageParser.SetVariable("HowtoBet", sel.GetResourceValue("lbl_howToBet"));
        PageParser.SetVariable("AboutUs", sel.GetResourceValue("lbl_AboutUs"));
        PageParser.SetVariable("Help", sel.GetResourceValue("lbl_help"));
        PageParser.SetVariable("FAQ", sel.GetResourceValue("lbl_FAQ"));
        PageParser.SetVariable("Rules_Regulations", sel.GetResourceValue("lbl_RR"));
        PageParser.SetVariable("ContactUs", sel.GetResourceValue("lbl_ContactUs"));
        PageParser.SetVariable("lbl_advice", sel.GetResourceValue("lbl_Advice"));

            PageParser.SetVariable("lbl_twitterurl", "#");
			PageParser.SetVariable("twitter_target", @"_self");
        
        PageParser.SetVariable("lbl_facebookurl", sel.GetResourceValue("lbl_facebookurl"));

        #endregion
    }
}
