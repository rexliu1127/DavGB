using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SB.Agent.Common;
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.DBLibrary;
using SB.Common.Files;
public partial class ChangeSubStatus : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserLan = Agents.Language;
        int ToCID = 0;
        string ToCN = "";
        string OldStatus = "";
        int CurrentAgentID = 0;
        int CurrentRoleID = 0;
        int SubAgentID = 0;
        int SubRoleID = 0;


        if (!Agents.UserMgr)
        {
            return;
        }
        #region Get Request dtat
        int.TryParse(Request["ToCID"], out ToCID);
        ToCN = Request["ToCN"];
        OldStatus = Request["OldStatus"];
        if (ToCID == 0 || string.IsNullOrEmpty(ToCN) || string.IsNullOrEmpty(OldStatus))
        {
            return;
        }
        int.TryParse(Request["SubAgentID"], out SubAgentID);
        int.TryParse(Request["SubRoleID"], out SubRoleID);
        #endregion

        #region Load Template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("ChangeSubStatus.html");
        #endregion

            CurrentAgentID = Agents.AgentID;
            CurrentRoleID = Agents.RoleID;
            //set search button param
            PageParser.SetVariable("CurrentAgentID", "");
            PageParser.SetVariable("CurrentRoleID", "");


        #region Set Label

        
        PageParser.SetVariable("lbl_ChangeStatus", Skin.GetResValue(UserLan, "lbl_ChangeStatus"));
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("lbl_UserName", Skin.GetResValue(UserLan, "lbl_UserName"));
        PageParser.SetVariable("UserName", ToCN);
        PageParser.SetVariable("lbl_OldStatus", Skin.GetResValue(UserLan, "lbl_OldStatus"));
        PageParser.SetVariable("OldStatus", OldStatus);
        PageParser.SetVariable("lbl_Open", Skin.GetResValue(UserLan, "lbl_Open"));
        PageParser.SetVariable("lbl_Suspend", Skin.GetResValue(UserLan, "lbl_Suspend"));
        PageParser.SetVariable("lbl_Closed", Skin.GetResValue(UserLan, "lbl_Closed"));
        PageParser.SetVariable("lbl_Disabled", Skin.GetResValue(UserLan, "lbl_Disabled"));
        PageParser.SetVariable("lbl_submit", Skin.GetResValue(UserLan, "lbl_Submit"));
        PageParser.SetVariable("lbl_cancel", Skin.GetResValue(UserLan, "lbl_cancel"));
        PageParser.SetVariable("ToCID", ToCID.ToString());

        #endregion
    }
}