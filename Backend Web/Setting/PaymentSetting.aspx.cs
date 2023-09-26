using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SB.Agent.Common;
using SB.Agent.Sites;
using SB.Agent.Users;
using SB.DBLibrary;

public partial class PaymentSetting : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string CurrentAgentName = "";
        int CurrentAgentID = 0;
        int CurrentRoleID = 0;
        string UserLan = Agents.Language;


        #region Load Template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("PaymentSetting.html");
        #endregion


        CurrentAgentName = Agents.AgentName;
        CurrentAgentID = Agents.AgentID;
        CurrentRoleID = Agents.RoleID;
        //set search button param
        PageParser.SetVariable("CurrentAgentID", "");
        PageParser.SetVariable("CurrentRoleID", "");

        PageParser.SetVariable("CurrentAgentName", CurrentAgentName);
        PageParser.SetVariable("lbl_PaymentSetting", Skin.GetResValue(UserLan, "lbl_PaymentSetting"));
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));

    }
}