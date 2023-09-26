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

public partial class NewAccount : ParsedPage
{
    SelLang sl = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        string userlang = "";
        try
        {
            userlang = Auth.GetUserLang(this.Context);
            sl = new SelLang(userlang);
        }
        catch  {
            Response.Write("<script>alert('Page time out.');</script>");
            Response.Write("<script> top.window.location.href='login.aspx'</script>");
        }

        if (Context.Session["ServerInfo_key"] == null)
        {
            Response.Redirect("login.aspx");
            return;
        }

        string skin_path = member_site.SelLang.GetSkinPath(Context);
        string webname = member_site.Common.GetWebName(Context);

        if (webname == "ibet888.net" || webname == "trojanbet.com" || webname == "titanplay.com")
        {
            Response.Redirect("login.aspx");
            return;
        }

        #region Load Template
        PageParser.SetTemplatesDir("template/" + userlang + "/");
        PageParser.SetTemplateFile("NewAccount.html");
        #endregion

        #region Set Label

        PageParser.SetVariable("skinpath", skin_path);
        PageParser.SetVariable("webname", webname);

        PageParser.SetVariable("lbl_firstname", sl.GetResourceValue("lbl_firstname"));

        PageParser.SetVariable("lbl_lastname", sl.GetResourceValue("lbl_lastname"));

        PageParser.SetVariable("lbl_phone", sl.GetResourceValue("lbl_phone"));

        PageParser.SetVariable("lbl_mobile", sl.GetResourceValue("lbl_mobile"));

        PageParser.SetVariable("lbl_fax", sl.GetResourceValue("lbl_fax"));

        PageParser.SetVariable("lbl_email", sl.GetResourceValue("lbl_email"));

        PageParser.SetVariable("lbl_address", sl.GetResourceValue("lbl_address"));

        PageParser.SetVariable("lbl_remark", sl.GetResourceValue("lbl_remark"));

        PageParser.SetVariable("lbl_code", sl.GetResourceValue("lbl_code"));

        PageParser.SetVariable("lbl_submit", sl.GetResourceValue("lbl_submit"));

        PageParser.SetVariableFile("topmenu", "info_topmenu.html");
        if (webname == "trojanbet.com" || webname == "titanplay.com" || webname == "spotico.com")
        {
            PageParser.SetVariable("link_aboutus", "#");
            PageParser.SetVariable("link_openaccount", "#");
            PageParser.SetVariable("link_contact", "#");
            PageParser.SetVariable("link_account", "#");

        }
        else
        {
            PageParser.SetVariable("link_aboutus", "DF_about_us.aspx");
            PageParser.SetVariable("link_openaccount", "NewAccount.aspx");
            PageParser.SetVariable("link_contact", "DF_Contact.aspx");
            PageParser.SetVariable("link_account", "DF_Account.aspx");
        }

        #endregion

        #region Send Mail

        if (Request["hidSubmit"]  !=null && Request["hidSubmit"].ToString()  == "YES" )
        {
            string strAmailBody = "";
            string strFirstname = Request.Params["txtFirstName"];
            string strLastname = Request.Params["txtLastName"];
            string strPhone = Request.Params["txtPhone"];
            string strMobile = Request.Params["txtMobile"];
            string strFax = Request.Params["txtFax"];
            string strMail = Request.Params["txtMail"];
            string strAddress = Request.Params["txtAddress"];
            string strRemark = Request.Params["txtRemark"];
            string strCode = Request.Params["txtCode"];

            Mail Cmail = new Mail(Common.MailServer);
            Mail Amail = new Mail(Common.MailServer);
            Cmail.MailFrom = Common.AdminMail;
            Amail.MailFrom = Common.AdminMail;
            Cmail.MailTo = new string[] { strMail };
            Amail.MailTo = new string[] { Common.OperatorMail };
            Cmail.Subject = sl.GetResourceValue("mail_cust_subject");
            Amail.Subject = sl.GetResourceValue("mail_agent_subject");
            Cmail.Body = sl.GetResourceValue("mail_cust_body");

            strAmailBody = sl.GetResourceValue("mail_agent_body") + "<br>";
            strAmailBody += sl.GetResourceValue("lbl_firstname") + " : " + strFirstname + "<br>";
            strAmailBody += sl.GetResourceValue("lbl_lastname") + " : " + strLastname + "<br>";
            strAmailBody += sl.GetResourceValue("lbl_phone") + " : " + strPhone + "<br>";
            strAmailBody += sl.GetResourceValue("lbl_mobile") + " : " + strMobile + "<br>";
            strAmailBody += sl.GetResourceValue("lbl_fax") + " : " + strFax + "<br>";
            strAmailBody += sl.GetResourceValue("lbl_email") + " : " + strMail + "<br>";
            strAmailBody += sl.GetResourceValue("lbl_address") + " : " + strAddress + "<br>";
            strAmailBody += sl.GetResourceValue("lbl_remark") + " : " + strRemark + "<br>";

            Amail.Body = strAmailBody;
            try
            {
                if (!CheckCode(member_site.Common.FixQuotes(strCode)))
                {

                    throw new Exception("You inputed an invalid code");
                }
                Cmail.Send();
                Amail.Send();
                Response.Write("<script>alert('" + sl.GetResourceValue("mail_success") + "');</script>");
                Response.Write("<script> top.window.location.href='login.aspx'</script>");

            }
            catch (Exception ex)
            {
               
                Response.Write("<script>alert(\"" + ex.Message + "\");</script>");
            }
        }
        #endregion
    }

    private bool CheckCode(string strCode)
    {
        strCode = strCode.ToUpper();
        string strSessionCode = System.Web.HttpContext.Current.Session["Acc_CheckCode"] + "";
        strSessionCode = strSessionCode.ToUpper();
        Session["Acc_CheckCode"] = null;
        return (strSessionCode == strCode);
    }  
}
