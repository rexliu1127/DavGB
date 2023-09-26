using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using SB.Agent.Common;
using SB.Agent.Sites;
using SB.Agent.Users;
public partial class Head_logon : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserLan=Agents.Language;
        #region Load template
        PageParser.SetTemplatesDir(Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("Head.html");
        #endregion

        #region Set Logo
        string Logo = "Logo";
        if (UserLan.ToLower() != "en")
        {
            Logo = Logo + "_" + UserLan.ToLower();
        }
        PageParser.SetVariable("Logo", Logo);

        #endregion


        #region Set Label
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("lbl_Welcome",Skin.GetResValue(UserLan,"lbl_Welcome"));
        PageParser.SetVariable("UserName", Agents.AgentName);
        PageParser.SetVariable("lbl_Logout", Skin.GetResValue(UserLan, "lbl_Logout"));
        PageParser.SetVariable("lbl_ChangePassword", Skin.GetResValue(UserLan, "lbl_changepassword"));
        #endregion

        PageParser.SetVariable("Now", DateTime.Now.ToString("h:m:s tt MMM d , yyyy", CultureInfo.CreateSpecificCulture("en-US")));

    }
}