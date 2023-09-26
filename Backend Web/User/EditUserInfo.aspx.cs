using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using aspxtemplate;
using System.Data;
using System.Data.SqlClient;
using SB.DBLibrary;
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.Agent.Common;
using SB.Common.Files;

public partial class User_EditUserInfo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String UserLan = Agents.Language;

        int ToUserID = 0;


        if (!Agents.UserMgr)
        {
            return;
        }
        #region Get Request Params

        if ( !int.TryParse(Request["ToUserID"], out ToUserID))
        {
            return;
        }

        
        #endregion

        #region Load template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("EditUserInfo.html");
        #endregion



 
        #region Set Label
        //Basic information
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("UserLan", UserLan);

        PageParser.SetVariable("lbl_cancel", Skin.GetResValue(UserLan, "lbl_cancel"));
        PageParser.SetVariable("lbl_submit", Skin.GetResValue(UserLan, "lbl_submit"));

        PageParser.SetVariable("lbl_EditAccount", Skin.GetResValue(UserLan, "lbl_EditUser"));
        PageParser.SetVariable("ToCID", ToUserID.ToString());
        PageParser.SetVariable("lbl_UserName", Skin.GetResValue(UserLan, "lbl_UserName"));


        PageParser.SetVariable("lbl_FirstName", Skin.GetResValue(UserLan, "lbl_FName"));
        PageParser.SetVariable("lbl_LastName", Skin.GetResValue(UserLan, "lbl_LName"));
        PageParser.SetVariable("lbl_Phone", Skin.GetResValue(UserLan, "lbl_Phone"));
        PageParser.SetVariable("lbl_Permissions", Skin.GetResValue(UserLan, "lbl_Permissions"));
        PageParser.SetVariable("lbl_Member", Skin.GetResValue(UserLan, "lbl_Member"));
        PageParser.SetVariable("lbl_Bank", Skin.GetResValue(UserLan, "lbl_Bank"));
        PageParser.SetVariable("lbl_Reports", Skin.GetResValue(UserLan, "lbl_Reports"));
        PageParser.SetVariable("lbl_EMail", Skin.GetResValue(UserLan, "lbl_EMail"));
        PageParser.SetVariable("lbl_TotalBets", Skin.GetResValue(UserLan, "lbl_TotalBets"));
        PageParser.SetVariable("lbl_Setting", Skin.GetResValue(UserLan, "lbl_Setting"));

        #endregion
        DataTable dt_UserInfo = new DataTable();
        DBbase connMain = new DBbase(SiteSetting.MainConnString);
        connMain.ExecuteByParameters("New_Age_GetUserInfo", ref dt_UserInfo, new SqlParameter("@custid", ToUserID));

        PageParser.SetVariable("UserName", dt_UserInfo.Rows[0]["username"].ToString());
        PageParser.SetVariable("Firstname", dt_UserInfo.Rows[0]["firstname"].ToString());
        PageParser.SetVariable("Lastname", dt_UserInfo.Rows[0]["lastname"].ToString());
        PageParser.SetVariable("Phone", dt_UserInfo.Rows[0]["phone"].ToString());
        PageParser.SetVariable("EMail", dt_UserInfo.Rows[0]["email"].ToString());
        string Permission = "";
        if (!Convert.IsDBNull(dt_UserInfo.Rows[0]["Permissions"]))
        {
            string[] PermissionsList = dt_UserInfo.Rows[0]["Permissions"].ToString().Split(',');
            if (PermissionsList.Length != 0)
            {
                if (PermissionsList[0] == "1")
                {
                    PageParser.SetVariable("checkedMember", "checked");

                }
                if (PermissionsList[1] == "1")
                {
                    PageParser.SetVariable("checkedBank", "checked");
                }
                if (PermissionsList[2] == "1")
                {
                    PageParser.SetVariable("checkedTotalBets", "checked");
                }
                if (PermissionsList[3] == "1")
                {
                    PageParser.SetVariable("checkedReports", "checked");
                }
                if (PermissionsList[4] == "1")
                {
                    PageParser.SetVariable("checkedSetting", "checked");
                }
            }
        }
        PageParser.SetVariable("Permissions", Permission);
    }

}