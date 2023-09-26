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
using System.Collections;
using SB.DBLibrary;
using SB.Common.Files;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Configuration;
public partial class Member_DefaultMinMax : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String UserLan = Agents.Language;



        if (!Agents.MemberMgr)
        {
            return;
        }


        #region Load template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("DefaultMinMax.html");
        #endregion
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("lbl_DefaultMinMax", Skin.GetResValue(UserLan, "lbl_DefaultMinMax"));
        PageParser.SetVariable("lbl_MinBet", Skin.GetResValue(UserLan, "lbl_MinBet"));
        PageParser.SetVariable("lbl_MaxBet", Skin.GetResValue(UserLan, "lbl_MaxBet"));
        PageParser.SetVariable("lbl_SlotGame", Skin.GetResValue(UserLan, "lbl_SlotGame"));
        PageParser.SetVariable("lbl_LiveCasino", Skin.GetResValue(UserLan, "lbl_LiveCasino"));
        PageParser.SetVariable("lbl_Keno", Skin.GetResValue(UserLan, "lbl_Keno"));

        PageParser.SetVariable("lbl_GameProivder", Skin.GetResValue(UserLan, "lbl_GameProivder"));
        PageParser.SetVariable("lbl_submit", Skin.GetResValue(UserLan, "lbl_submit"));
        
        DBbase connMain = null;
        DataTable dt_Default = new DataTable();
        connMain = new DBbase(SiteSetting.MainConnString);
        connMain.ExecuteByParameters("New_Age_GetGameSetting", ref dt_Default, null);
        if (dt_Default.Rows.Count == 0)
        {
            PageParser.SetVariable("DefaultMin_1_1", "1");
            PageParser.SetVariable("DefaultMax_1_1", "1000");
            PageParser.SetVariable("DefaultMin_2_2", "1");
            PageParser.SetVariable("DefaultMax_2_2", "1000");
            PageParser.SetVariable("DefaultMin_2_1", "1");
            PageParser.SetVariable("DefaultMax_2_1", "1000");
            PageParser.SetVariable("DefaultMin_3_3", "1");
            PageParser.SetVariable("DefaultMax_3_3", "1000");
        }
        else
        {
            DataRow[] dr_Provider = null;
            dr_Provider = dt_Default.Select("ProviderID='1'");
            if (dr_Provider.Length == 0)
            {
                PageParser.SetVariable("DefaultMin_1_1", "1");
                PageParser.SetVariable("DefaultMax_1_1", "1000");

            }
            else
            {
                for (int i = 0; i < dr_Provider.Length; i++)
                {
                    DataRow drSetting = dr_Provider[i];
                    string minKey = "DefaultMin_1_" + drSetting["GameType"].ToString();
                    string maxKey = "DefaultMax_1_" + drSetting["GameType"].ToString();
                    PageParser.SetVariable(minKey,Convert.ToDecimal( drSetting["MinBet"]).ToString("#"));
                    PageParser.SetVariable(maxKey, Convert.ToDecimal(drSetting["MaxBet"]).ToString("#"));
                }
            }

            dr_Provider = dt_Default.Select("ProviderID='2'");
            if (dr_Provider.Length == 0)
            {
                PageParser.SetVariable("DefaultMin_2_1", "1");
                PageParser.SetVariable("DefaultMax_2_1", "1000");
                PageParser.SetVariable("DefaultMin_2_2", "1");
                PageParser.SetVariable("DefaultMax_2_2", "1000");
            }
            else
            {
                for (int i = 0; i < dr_Provider.Length; i++)
                {
                    DataRow drSetting = dr_Provider[i];
                    string minKey = "DefaultMin_2_" + drSetting["GameType"].ToString();
                    string maxKey = "DefaultMax_2_" + drSetting["GameType"].ToString();
                    PageParser.SetVariable(minKey,Convert.ToDecimal( drSetting["MinBet"]).ToString("#"));
                    PageParser.SetVariable(maxKey,Convert.ToDecimal( drSetting["MaxBet"]).ToString("#"));
                }
            }

            dr_Provider = dt_Default.Select("ProviderID='3'");
            if (dr_Provider.Length == 0)
            {
                PageParser.SetVariable("DefaultMin_3_3", "1");
                PageParser.SetVariable("DefaultMax_3_3", "1000");
            }
            else
            {
                for (int i = 0; i < dr_Provider.Length; i++)
                {
                    DataRow drSetting = dr_Provider[i];
                    string minKey = "DefaultMin_3_" + drSetting["GameType"].ToString();
                    string maxKey = "DefaultMax_3_" + drSetting["GameType"].ToString();
                    PageParser.SetVariable(minKey, Convert.ToDecimal(drSetting["MinBet"]).ToString("#"));
                    PageParser.SetVariable(maxKey, Convert.ToDecimal(drSetting["MaxBet"]).ToString("#"));
                }
            }
        }
    }
}