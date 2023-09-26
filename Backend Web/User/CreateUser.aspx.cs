using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SB.Agent.Common;
using SB.Agent.Sites;
using SB.Agent.Users;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SB.DBLibrary;
using SB.Common.Files;

public partial class User_CreateUser : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
     

        String UserLan = Agents.Language;

        if (!Agents.UserMgr)
        {
            return;
        }
        #region Load template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));

        PageParser.SetTemplateFile("CreateUser.html");

        #endregion

        #region Set Label
        //Basic information
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("UserLan", UserLan);
        PageParser.SetVariable("lbl_UserNameErr", Skin.GetResValue(UserLan, "lbl_UserNameErr"));
        PageParser.SetVariable("lbl_enter_pw", Skin.GetResValue(UserLan, "lbl_enter_pw"));
        PageParser.SetVariable("lbl_execPassword", Skin.GetResValue(UserLan, "lbl_execPassword"));
        PageParser.SetVariable("lbl_CreditErr", Skin.GetResValue(UserLan, "lbl_CreditErr"));
        PageParser.SetVariable("lbl_CopyErr", Skin.GetResValue(UserLan, "lbl_CopyErr"));
        PageParser.SetVariable("lbl_close", Skin.GetResValue(UserLan, "lbl_close"));
        PageParser.SetVariable("lbl_submit", Skin.GetResValue(UserLan, "lbl_submit"));
        PageParser.SetVariable("lbl_NewAccount", Skin.GetResValue(UserLan, "lbl_CreateUser"));

        PageParser.SetVariable("lbl_UserName", Skin.GetResValue(UserLan, "lbl_UserName"));

        PageParser.SetVariable("lbl_Password", Skin.GetResValue(UserLan, "lbl_Password"));
        PageParser.SetVariable("lbl_FirstName", Skin.GetResValue(UserLan, "lbl_FName"));
        PageParser.SetVariable("lbl_LastName", Skin.GetResValue(UserLan, "lbl_LName"));
        PageParser.SetVariable("lbl_Phone", Skin.GetResValue(UserLan, "lbl_Phone"));
        PageParser.SetVariable("lbl_MobilePhone", Skin.GetResValue(UserLan, "lbl_MobilePhone"));
  
       
        PageParser.SetVariable("lbl_Permissions", Skin.GetResValue(UserLan, "lbl_Permissions"));
        PageParser.SetVariable("lbl_Member", Skin.GetResValue(UserLan, "lbl_Member"));
        PageParser.SetVariable("lbl_Bank", Skin.GetResValue(UserLan, "lbl_Bank"));
        PageParser.SetVariable("lbl_Reports", Skin.GetResValue(UserLan, "lbl_Reports"));
        PageParser.SetVariable("lbl_execPassword", Skin.GetResValue(UserLan, "lbl_execPassword"));
        PageParser.SetVariable("lbl_TotalBets", Skin.GetResValue(UserLan, "lbl_TotalBets"));
         PageParser.SetVariable("lbl_Setting", Skin.GetResValue(UserLan, "lbl_Setting"));
        
        PageParser.SetVariable("lbl_EMail", Skin.GetResValue(UserLan, "lbl_EMail"));
        
        #endregion
    }
}