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


public partial class logout : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string redirectUrl = "";
        string url = string.IsNullOrEmpty(Request["Url"]) ? "" : Request["Url"].ToString();

        redirectUrl = redirectUrl = "http://" + Request.ServerVariables["HTTP_HOST"].ToString();
        if (Session["UserID"] == null)
        {
            Response.Write("<script> top.window.location.href='" + redirectUrl + "'</script>");
            return;
        }

        Auth au = new Auth();

      

     
            au.Logout(this.Context);
            Response.Write("<script> top.window.location.href='" + redirectUrl + "'</script>");
        
    }
}
