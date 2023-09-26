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
using System.Data.SqlClient;

using member_site;
using aspxtemplate;

public partial class MultiSkin_MultiSkin_ProcessLogin : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool boolIsDowntime = false;
     

        #region load UMPage
        if (boolIsDowntime)
        {
            return;
        }

        #endregion

        if (Request["UserName"] == null || Request["PWD"] == null || Request["VCode"] == null) return;

        string strUserName = Request["UserName"];
        string strPW = Request["PWD"];
        string strCode = Request["VCode"];

        string strServerName = Request.ServerVariables["SERVER_NAME"];


        try
        {
            //get validcode from session
            string strSessionCode = System.Web.HttpContext.Current.Session["CheckCode"] + "";

                // if member site ,it need to check txtCode
                if (!CheckCode(Common.FixQuotes(strCode)))
                {
                    if (UserAction.IsFromBDLogin)
                        throw new Exception(SelLang.GetMsgResourceValue(this.Context, "inputinvalid"));
                    else
                    {
                        Response.Write(SelLang.GetMsgResourceValue(this.Context, "inputinvalid"));
                        return;
                    }
                }
            

            Auth au = new member_site.Auth();
            au.Login(strUserName, strPW, strCode);

            
            Session["RobotLogin"] = false;  //flag to record login user is real robot



            Session["LoginVerified"] = true;


            Response.Write("ok");
            

            UserAction.IsFromBDLogin = false;


         
        }
        catch (Exception ex)
        {

            if (UserAction.IsFromBDLogin)
                Response.Write("<script>alert('" + Server.HtmlDecode(ex.Message) + "');window.top.location='http://" + strServerName + "/';</script>");
            else
                Response.Write(ex.Message);
            //}
        }

    }

    private bool CheckCode(string strCode)
    {
        strCode = strCode.ToUpper();
        string strSessionCode = System.Web.HttpContext.Current.Session["CheckCode"] + "";
        strSessionCode = strSessionCode.ToUpper();
        //Session["CheckCode"] = null;
        return (strSessionCode == strCode);
    }

    protected override void Render(HtmlTextWriter writer)
    {

    }

    private string GetCustCurrencyName()
    {
        SB.DBLibrary.DBbase aConn = new SB.DBLibrary.DBbase(Common.ConnectionString);
        DataTable aTable = aConn.ExecSPbyParamsWithDataTable("dbo.Dot_member_GetCustCurrency", new SqlParameter("@CustID", Auth.GetUserID(Context)));

        if (aTable.Rows.Count == 0)
            return "";
        return aTable.Rows[0]["currency"].ToString();
    }

    private void WriteLogFileToDB(string doing)
    {
        SB.DBLibrary.DBbase conn = new SB.DBLibrary.DBbase(Common.ConnectionString);
        conn.ExecuteSPbyParameters("dot_logfile",
                           new SqlParameter("userid", Auth.GetUserID(Context)),
                           new SqlParameter("doing", doing));
    }

 

}
