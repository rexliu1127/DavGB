using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SB.Agent.Common;
using SB.Agent.Users;
using SB.Agent.Sites;
public partial class Report_ChangTransStatus : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserLan = Agents.Language;
        int ToTID = 0;
        string ToCN = "";
        string Status = "";
        string Remark = "";
        if (!Agents.ReportMgr)
        {
            return;
        }
        int.TryParse(Request["ToTID"], out ToTID);
        ToCN = Request["ToCN"];
        if (ToTID == 0 || string.IsNullOrEmpty(ToCN))
        {
            return;
        }
        Status = Request["Status"];
        Remark = Request["Remark"];
        #region Load Template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("ChangTransStatus.html");
        #endregion
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("lbl_UserName", Skin.GetResValue(UserLan, "lbl_UserName"));
        PageParser.SetVariable("UserName", ToCN);
        PageParser.SetVariable("lbl_OldStatus", Skin.GetResValue(UserLan, "lbl_OldStatus"));
        PageParser.SetVariable("lbl_submit", Skin.GetResValue(UserLan, "lbl_Submit"));
        PageParser.SetVariable("lbl_cancel", Skin.GetResValue(UserLan, "lbl_cancel"));
        PageParser.SetVariable("lbl_ChangeStatus", Skin.GetResValue(UserLan, "lbl_ChangeStatus"));
        PageParser.SetVariable("Status_0", Skin.GetResValue(UserLan, "Status_0"));
        PageParser.SetVariable("Status_1", Skin.GetResValue(UserLan, "Status_1"));
        PageParser.SetVariable("Status_2", Skin.GetResValue(UserLan, "Status_2"));
        PageParser.SetVariable("Status_3", Skin.GetResValue(UserLan, "Status_3"));
        PageParser.SetVariable("Status_4", Skin.GetResValue(UserLan, "Status_4"));
        PageParser.SetVariable("Status_5", Skin.GetResValue(UserLan, "Status_5"));
        PageParser.SetVariable("OldStatus", Status);
        PageParser.SetVariable("ToTID", ToTID.ToString());
        PageParser.SetVariable("lbl_Memo", Skin.GetResValue(UserLan, "lbl_remark"));
        PageParser.SetVariable("Memo", Remark);
            
    }
}