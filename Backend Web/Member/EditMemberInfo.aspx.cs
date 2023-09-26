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
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.Agent.Common;
using SB.DBLibrary;
using SB.Common.Files;
public partial class User_EditMemberInfo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String UserLan = Agents.Language;

        int ToUserID = 0;


        if (!Agents.MemberMgr)
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
        PageParser.SetTemplateFile("EditMemberInfo.html");
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

        PageParser.SetVariable("lbl_EMail", Skin.GetResValue(UserLan, "lbl_EMail"));
        PageParser.SetVariable("lbl_Birthday", Skin.GetResValue(UserLan, "lbl_Birthday"));
        PageParser.SetVariable("lbl_Country", Skin.GetResValue(UserLan, "lbl_Country"));
        PageParser.SetVariable("lbl_address", Skin.GetResValue(UserLan, "lbl_address"));
        PageParser.SetVariable("lbl_Citytown", Skin.GetResValue(UserLan, "lbl_Citytown"));
        PageParser.SetVariable("lbl_State", Skin.GetResValue(UserLan, "lbl_State"));
         PageParser.SetVariable("lbl_Zipcode", Skin.GetResValue(UserLan, "lbl_Zipcode"));
        PageParser.SetVariable("lbl_Primaryphone", Skin.GetResValue(UserLan, "lbl_Primaryphone"));
        PageParser.SetVariable("lbl_Gender", Skin.GetResValue(UserLan, "lbl_Gender"));
        PageParser.SetVariable("lbl_Male", Skin.GetResValue(UserLan, "lbl_Male"));
        PageParser.SetVariable("lbl_Female", Skin.GetResValue(UserLan, "lbl_Female"));
        PageParser.UpdateBlock("MONTH");
        for (int i = 1; i <= 12; i++)
        {
            PageParser.SetVariable("MonthID", i.ToString());
            PageParser.SetVariable("MonthName", i.ToString());
            PageParser.SetVariable("Selected", "");
            PageParser.ParseBlock("MONTH");
        }

        int EndYear = DateTime.Now.Year - 18;
        PageParser.UpdateBlock("SELYEAR");
        for (int cntyeat = 0; cntyeat < 100; cntyeat++)
        {
            string DispYear = Convert.ToString((EndYear - cntyeat));
            PageParser.SetVariable("YearCode", DispYear);
            PageParser.SetVariable("YearName", DispYear);
            PageParser.SetVariable("Selected", "");
            PageParser.ParseBlock("SELYEAR");
        }

        DataRow[] drList;
        drList = GetListfromXML(Server.MapPath("") + "\\..\\Setting\\Country.xml", "LangID", (Agents.Language));
        PageParser.UpdateBlock("SELCOUNTRY");
        if (drList.Length > 0)
        {


            PageParser.SetVariable("Selected", "");
            PageParser.ParseBlock("SELCOUNTRY");
            for (int i = 0; i < drList.Length; i++)
            {
                PageParser.SetVariable("CountryCode", drList[i]["CountryID"].ToString());
                PageParser.SetVariable("CountryName", drList[i]["CountryName"].ToString());

                PageParser.SetVariable("Selected", "");

                PageParser.ParseBlock("SELCOUNTRY");
            }
        }
        #endregion

        DataTable dt_UserInfo = new DataTable();
        DBbase connMain = new DBbase(SiteSetting.MainConnString);
        connMain.ExecuteByParameters("New_Age_GetMemberInfo", ref dt_UserInfo, new SqlParameter("@custid", ToUserID));

        PageParser.SetVariable("UserName", dt_UserInfo.Rows[0]["username"].ToString());
        PageParser.SetVariable("Firstname", dt_UserInfo.Rows[0]["firstname"].ToString());
        PageParser.SetVariable("Lastname", dt_UserInfo.Rows[0]["lastname"].ToString());
        PageParser.SetVariable("Phone", dt_UserInfo.Rows[0]["phone"].ToString());
        PageParser.SetVariable("EMail", dt_UserInfo.Rows[0]["email"].ToString());
        if (!Convert.IsDBNull(dt_UserInfo.Rows[0]["birthday"]))
        {
            DateTime Birthday = Convert.ToDateTime(dt_UserInfo.Rows[0]["birthday"]);
            PageParser.SetVariable("bYear", Birthday.Year.ToString());
            PageParser.SetVariable("bMonth", Birthday.Month.ToString());
            PageParser.SetVariable("bDay", Birthday.Day.ToString());
        }
        PageParser.SetVariable("Country", dt_UserInfo.Rows[0]["country"].ToString());

        if (dt_UserInfo.Rows[0]["gender"].ToString() == "0")
        {
            PageParser.SetVariable("MaleChecked", "checked");
            PageParser.SetVariable("FemaleChecked", "");
        }
        else
        {

                PageParser.SetVariable("MaleChecked", "");
                PageParser.SetVariable("FemaleChecked", "checked");
            
        }
        PageParser.SetVariable("Country", dt_UserInfo.Rows[0]["country"].ToString());
        PageParser.SetVariable("address", dt_UserInfo.Rows[0]["address"].ToString());
        PageParser.SetVariable("Citytown", dt_UserInfo.Rows[0]["city"].ToString());
        PageParser.SetVariable("State", dt_UserInfo.Rows[0]["state"].ToString());
        PageParser.SetVariable("Zipcode", dt_UserInfo.Rows[0]["ZipCode"].ToString());
        PageParser.SetVariable("mobile", dt_UserInfo.Rows[0]["phone"].ToString());
        
    }
    public static DataRow[] GetListfromXML(String XmlPath, String SearchKey, String SelLang)
    {
        DataSet ds = new DataSet();
        DataRow[] dr;
        //Hashtable hs = new Hashtable();
        //string strWebName = Common.GetWebName(hc);
        ds.ReadXml(XmlPath);
        dr = ds.Tables[0].Select(SearchKey + "='" + SelLang + "'");
        //for (int i = 0; i < dr.Length; i++)
        //{
        //    string strLanID = dr[i]["LanID"].ToString();
        //    hs.Add(strLanID, (string)HttpContext.GetGlobalResourceObject("Lan_Info", strLanID));
        //}
        return dr;

    }
}

