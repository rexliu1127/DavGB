using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions; 
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using aspxtemplate;
using member_site;


public partial class CheckUserName : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool hasError = false; 
        string CheckName ="";
        string UserName_status = ""; 
        SelLang sel = new SelLang(Auth.GetUserLang(this.Context));
          
        CheckName =  Request.QueryString["UName"].ToString();
      
        #region load template file
        String SkinPath = SelLang.GetSkinPath(Context);
        string SkinRootPath = SelLang.GetSkinRootPath();
        PageParser.SetTemplatesDir(SkinRootPath + "public/");
        PageParser.SetTemplateFile("CheckUserName.html");
       
        
        #endregion
        #region Create DB Object 
        SqlDataSource dsr = null;
        DataView dv = null;
        DataTable dt = null;

        member_site.DBbase conn = null;
        try
        {
            
            conn = new member_site.DBbase(Common.ConnectionString);
        }
        catch (SqlException ex)
        {
            hasError = true;
            //out put some logs
        }
        #endregion
        if (!hasError)
        {
            #region Do Store Procude for Check UserName Exists  [deposit_user.dbo.Deposit_CheckUserName]



                UserName_status =  sel.GetResourceValue("lbl_Worngfromat") ;   
            
            if (CheckName.Length < 5)
            {
                UserName_status = sel.GetResourceValue("lbl_limitUserName");   
            }
            else if (CheckName.IndexOf("__") != -1) 
            {
                UserName_status = sel.GetResourceValue("lbl_Worngfromat"); 
            }

            else
            {
                dt = conn.ExecSPbyParamsWithDataTable("Dot_Member_CheckUserName", new SqlParameter("@CheckName", CheckName)); 
                //dt = conn.ExecuteQuery(string.Format("Deposit_CheckUserName '{0}'", CheckName));
                if (dt.Rows.Count != 0)
                {
                    UserName_status = (dt.Rows[0][0].ToString() == "0") ? sel.GetResourceValue("msg_UserName_valid") : sel.GetResourceValue("msg_UserName_invalid");    // User Name valid 

                }
                else
                {
                    // Need log error record
                    hasError = true;
                }
            }

            #endregion
        }
        #region Set Check Result
        PageParser.SetVariable("UserName", CheckName);
        PageParser.SetVariable("Result1", UserName_status);
        PageParser.SetVariable("shin_path", SkinRootPath + "public/");
        #endregion

    }
    
}
