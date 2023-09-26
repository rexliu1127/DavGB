using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SB.Agent.Common;
using SB.Agent.Sites;
using SB.Agent.Users;
using System.Text;
using SB.DBLibrary;
using SB.Common.Files;
public partial class Report_Outstanding : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBbase connMain = null;
        DataTable dt_Outstanding = new DataTable();
        string OutstandingSP = "";
        string inputUserName = "";

        string UserLan = Agents.Language;
        string OddClolr = "tdbg1";
        string EvenColor = "tdbg2";

        #region Get Request
        inputUserName = Request["txtUserName"];
        #endregion

        #region Load Template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("Outstanding.html");
        #endregion


        #region Set Label
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        
        PageParser.SetVariable("lbl_Outstanding", Skin.GetResValue(UserLan, "lbl_Outstanding"));

        PageParser.SetVariable("lbl_Submit", Skin.GetResValue(UserLan, "lbl_Submit"));
        PageParser.SetVariable("lbl_UserName", Skin.GetResValue(UserLan, "lbl_UserName"));
        PageParser.SetVariable("lbl_TransID", Skin.GetResValue(UserLan, "lbl_TransID"));
        PageParser.SetVariable("lbl_GameType", Skin.GetResValue(UserLan, "lbl_GameType"));
        PageParser.SetVariable("lbl_stake", Skin.GetResValue(UserLan, "lbl_stake"));
        PageParser.SetVariable("lbl_TransTime", Skin.GetResValue(UserLan, "lbl_TransTime"));

        #endregion


        #region Get Outstanding Dara

        connMain = new DBbase(SiteSetting.MainConnString);
        connMain.ExecuteByParameters("New_Age_Rpt_Outstanding", ref dt_Outstanding,  new SqlParameter("@Username",inputUserName));
        #endregion

        #region Output Data
        PageParser.UpdateBlock("OutstandingListBlock");
        if (dt_Outstanding.Rows.Count == 0)
        {
            PageParser.SetVariable("OutstandingDataOutputLeft", "");
            PageParser.SetVariable("OutstandingDataOutputRight", "");

            return;
        }

        StringBuilder sbOutputLeft = new StringBuilder();
        StringBuilder sbOutputRight = new StringBuilder();
        int LeftCount = 1;
        int RightCount = 1;


        for (int i = 1; i <= dt_Outstanding.Rows.Count; i++)
        {
            DataRow dr = dt_Outstanding.Rows[i - 1];



            PageParser.SetVariable("Number", i.ToString());
            PageParser.SetVariable("UserName", dr["username"].ToString());
            PageParser.SetVariable("TransID", dr["transid"].ToString());
            PageParser.SetVariable("GameType", dr["GameType"].ToString());
            PageParser.SetVariable("Stake",Common.FormatNumber( Convert.ToDouble( dr["stake"])));
            DateTime TransTime = Convert.ToDateTime(dr["transdate"]);
            PageParser.SetVariable("TransTime",TransTime.ToString("yyyy/MM/dd HH:mm:ss"));



            #region Set Tr Interval Color
            if (i % 2 == 0)
            {
                if (RightCount % 2 == 0)
                {
                    PageParser.SetVariable("IntervalColor", EvenColor);
                }
                else
                {
                    PageParser.SetVariable("IntervalColor", OddClolr);
                }
                sbOutputRight.Append(PageParser.getblock("OutstandingListBlock"));
                RightCount++;
            }
            else
            {
                if (LeftCount % 2 == 0)
                {
                    PageParser.SetVariable("IntervalColor", EvenColor);
                }
                else
                {
                    PageParser.SetVariable("IntervalColor", OddClolr);
                }
                sbOutputLeft.Append(PageParser.getblock("OutstandingListBlock"));
                LeftCount++;
            }
            #endregion

        }
        PageParser.SetVariable("OutstandingDataOutputLeft", sbOutputLeft.ToString());
        PageParser.SetVariable("OutstandingDataOutputRight", sbOutputRight.ToString());

        #endregion

    }
}