using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SB.Agent.Common;
using SB.Agent.Sites;
using SB.Agent.Users;
using SB.Common.Files;
public partial class Menu : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserLan = Agents.Language;

        #region Load template
        PageParser.SetTemplatesDir(Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("Menu.html");
        #endregion

        #region Set Label
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));

        PageParser.SetVariable("lbl_UserMgr", Skin.GetResValue(UserLan, "lbl_UserMgr"));
         PageParser.SetVariable("lbl_CreateUser", Skin.GetResValue(UserLan, "lbl_CreateUser"));
         PageParser.SetVariable("lbl_ListUser", Skin.GetResValue(UserLan, "lbl_ListUser"));
        PageParser.SetVariable("lbl_Reports", Skin.GetResValue(UserLan, "lbl_Reports"));
        
        PageParser.SetVariable("lbl_MemberMgr", Skin.GetResValue(UserLan, "lbl_MemberMgr"));
        PageParser.SetVariable("lbl_ListMember", Skin.GetResValue(UserLan, "lbl_ListMember"));

        PageParser.SetVariable("lbl_ListMember", Skin.GetResValue(UserLan, "lbl_ListMember"));
        PageParser.SetVariable("lbl_MemberSetting", Skin.GetResValue(UserLan, "lbl_MemberSetting"));
        PageParser.SetVariable("lbl_DefaultMinMax", Skin.GetResValue(UserLan, "lbl_DefaultMinMax"));


        PageParser.SetVariable("lbl_Bank", Skin.GetResValue(UserLan, "lbl_Bank"));
        PageParser.SetVariable("lbl_CreateBank", Skin.GetResValue(UserLan, "lbl_CreateBank"));
        PageParser.SetVariable("lbl_ListBank", Skin.GetResValue(UserLan, "lbl_ListBank"));
        PageParser.SetVariable("lbl_Audit", Skin.GetResValue(UserLan, "lbl_Audit"));
        PageParser.SetVariable("lbl_Outstanding", Skin.GetResValue(UserLan, "lbl_Outstanding"));
        PageParser.SetVariable("lbl_WinLoss", Skin.GetResValue(UserLan, "lbl_WinLoss"));
        PageParser.SetVariable("lbl_PaymentFee", Skin.GetResValue(UserLan, "lbl_PaymentFee"));

        
        PageParser.SetVariable("lbl_TotalBets", Skin.GetResValue(UserLan, "lbl_TotalBets"));
        PageParser.SetVariable("lbl_SlotGame", Skin.GetResValue(UserLan, "lbl_SlotGame"));
        PageParser.SetVariable("lbl_LiveCasino", Skin.GetResValue(UserLan, "lbl_LiveCasino"));
        PageParser.SetVariable("lbl_Keno", Skin.GetResValue(UserLan, "lbl_Keno"));

        PageParser.SetVariable("lbl_Setting", Skin.GetResValue(UserLan, "lbl_Setting"));
        PageParser.SetVariable("lbl_PaymentSetting", Skin.GetResValue(UserLan, "lbl_PaymentSetting"));
        PageParser.SetVariable("lbl_GameSetting", Skin.GetResValue(UserLan, "lbl_GameSetting"));
        #endregion


        PageParser.SetVariable("WinLossUrl", "");


    }
}