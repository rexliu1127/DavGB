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
using aspxtemplate;

public partial class registration_thankyou : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Load Template 
        string skin_path =member_site.SelLang.GetSkinPath(Context);
        //---- BEGIN load template file ---------------
        PageParser.SetTemplatesDir(skin_path);
        PageParser.SetTemplateFile("registration_thankyou.html");
        PageParser.SetVariable("RegMail", Session["reg_Email"].ToString());
        #endregion
        #region Clear Register info from Session 
        Session["reg_FirstName"] = "";
        Session["reg_LastName"] = "";
        Session["reg_Gender"] = "";
        Session["reg_Email"] = "";
        Session["reg_UserName"] = "";
        Session["reg_PassWord"] = "";
        Session["reg_SecurityCode"] = "";
        Session["reg_Birth_Mon"] = "";
        Session["reg_Birth_Day"] = "";
        Session["reg_Birht_Year"] = "";
        Session["reg_Country"] = "";
        Session["reg_address"] = "";
        Session["reg_Citytown"] = "";
        Session["reg_State"] = "";
        Session["reg_ZipCode"] = "";
        Session["reg_Primary_phone"] = "";
        Session["reg_Pext"] = "";
        Session["reg_Ptime"] = "";
        Session["reg_Other_phone"] = "";
        Session["reg_Oext"] = "";
        Session["reg_Otime"] = "";
        Session["reg_Currency"] = "";
        Session["reg_Odds"] = "";
        #endregion


    }
}
