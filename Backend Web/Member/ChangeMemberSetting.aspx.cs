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
using System.Data;
using System.Data.SqlClient;
public partial class Member_ChangeMemberSetting : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserLan = Agents.Language;
        int ToCID = 0;
        int ProviderID = 0;
        int GameType = 0; 
        string ToCN = "";
        string OldStatus = "";



        if (!Agents.MemberMgr)
        {
            return;
        }
        #region Get Request dtat
        int.TryParse(Request["ToCID"], out ToCID);
        ToCN = Request["ToCN"];
        OldStatus = Request["OldStatus"];
        int.TryParse(Request["ProviderID"], out ProviderID);
        int.TryParse(Request["GameType"], out GameType);

        if (ToCID == 0 || string.IsNullOrEmpty(ToCN) || string.IsNullOrEmpty(OldStatus)
        || ProviderID == 0 || GameType==0)
        {
            return;
        }

        #endregion

        #region Load Template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("ChangeMemberSetting.html");
        #endregion



        #region Set Label


        PageParser.SetVariable("lbl_ChangeMemberSetting", Skin.GetResValue(UserLan, "lbl_ChangeMemberSetting"));
        PageParser.SetVariable("lbl_Provider", Skin.GetResValue(UserLan, "lbl_Provider"));
        PageParser.SetVariable("lbl_GameType", Skin.GetResValue(UserLan, "lbl_GameType"));
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("lbl_UserName", Skin.GetResValue(UserLan, "lbl_UserName"));
        PageParser.SetVariable("UserName", ToCN);
        PageParser.SetVariable("lbl_OldStatus", Skin.GetResValue(UserLan, "lbl_OldStatus"));
        PageParser.SetVariable("OldStatus", OldStatus);
        PageParser.SetVariable("lbl_Open", Skin.GetResValue(UserLan, "lbl_Open"));
        PageParser.SetVariable("lbl_Closed", Skin.GetResValue(UserLan, "lbl_Closed"));
        PageParser.SetVariable("lbl_submit", Skin.GetResValue(UserLan, "lbl_Submit"));
        PageParser.SetVariable("lbl_cancel", Skin.GetResValue(UserLan, "lbl_cancel"));
        PageParser.SetVariable("lbl_ChangeStatus", Skin.GetResValue(UserLan, "lbl_ChangeStatus"));

        PageParser.SetVariable("ToCID", ToCID.ToString());
        PageParser.SetVariable("ProviderID", ProviderID.ToString());
        PageParser.SetVariable("GameTypeID", GameType.ToString());
        #endregion

        DBbase connMain = new DBbase(SiteSetting.MainConnString);
        DataTable dt_Result = new DataTable();
        connMain.ExecuteByParameters("New_Age_GetProviderAndGameTypeName", ref dt_Result, new SqlParameter("@ProviderID", ProviderID)
                                                            , new SqlParameter("@GameTypeID", GameType)
                                                            );

        PageParser.SetVariable("Provider", Skin.GetResValue(UserLan, "lbl_GameProivder"));
    //    PageParser.SetVariable("Provider", dt_Result.Rows[0]["Provider"].ToString());
        PageParser.SetVariable("GameType", dt_Result.Rows[0]["GameType"].ToString());

    }
}