using aspxtemplate;
using member_site;

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class errorpage_ErrorReport : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Auth.IsLegalRequest(Session)) return;
        string ErrorPageName = Request["aspxerrorpath"].Replace("/", "//");  
        Exception objErr = (Exception)Application["LastException"];

        bool isUM = false;



        //checn is SqlException or UM Time
        if ((objErr is System.Data.SqlClient.SqlException || objErr is System.InvalidOperationException || objErr is System.Net.Sockets.SocketException) 
            && isUM)
        {            
            string LoginMode = System.Web.Configuration.WebConfigurationManager.AppSettings["LoginMode"];
            string redirectUrl = "";
            if (LoginMode == "local")
            {
                redirectUrl = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + "/index.aspx";
            }
            else
            {
                redirectUrl = "http://www." + Common.GetDomainName(HttpContext.Current).ToLower().Trim() + "/index.aspx";
            }
            Response.Write("<script> top.window.location.href='" + redirectUrl + "'</script>");
            Session.Abandon();
            return;
        }

 

        #region Load Template
        PageParser.SetTemplatesDir("/errorpage/");
        PageParser.SetTemplateFile("notes.html");
        #endregion
    }

}
