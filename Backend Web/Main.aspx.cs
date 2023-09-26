using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SB.Agent.Common;
using SB.Agent.Sites;
using SB.Agent.Users;
public partial class Main :PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserLan=Agents.Language;
        #region Load template
        PageParser.SetTemplatesDir(Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("Main.html");
        #endregion

        #region Process Get or Change Lan
        HttpCookie LangCookie = null;
        LangCookie = Request.Cookies["AgentUserLan"];
        //change lan
        if (!string.IsNullOrEmpty(Request["hidSelLang"]))
        {
            Agents.Language = Request["hidSelLang"];
            LangCookie = new HttpCookie("AgentUserLan");
            LangCookie.Expires = DateTime.Now.AddYears(1);
            LangCookie.Value = Agents.Language;
            Response.Cookies.Add(LangCookie);
        }
        else
        {
            if (string.IsNullOrEmpty(Agents.Language))
            {
                //cookie is null and  get default lan
                if (LangCookie == null)
                {
                    Agents.Language = "en";
                    LangCookie = new HttpCookie("AgentUserLan");
                    LangCookie.Value = Agents.Language;
                    LangCookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(LangCookie);
                }
                //get lan from cookie
                else
                {
                    Agents.Language = LangCookie.Value;
                }
            }
        }
        #endregion

        #region Set Label
        PageParser.SetVariable("SkinPublicPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("UserName", Agents.AgentName);
        PageParser.SetVariable("UserLang", Agents.Language);
        PageParser.SetVariable("lbl_UpdateSuccess", Skin.GetResValue(Agents.Language, "lbl_UpdateSuccess"));
        #endregion

        #region Set Head Clock
        #region get server time and set diplay time as GMT +8 
        DateTime gmt8 = DateTime.Now.ToUniversalTime().AddHours(8); //Change current system time to GMT +8
        int hr = gmt8.Hour;
        int min = gmt8.Minute;
        int sec = gmt8.Second;
        int week = Microsoft.VisualBasic.DateAndTime.Weekday(gmt8, Microsoft.VisualBasic.FirstDayOfWeek.Monday);
        int year = gmt8.Year;
        int month = gmt8.Month;
        int day = gmt8.Day;

        //set timer seeds
        PageParser.SetVariable("year", year.ToString());
        PageParser.SetVariable("month", month.ToString());
        PageParser.SetVariable("day", day.ToString());
        PageParser.SetVariable("hrs", hr.ToString());
        PageParser.SetVariable("min", min.ToString());
        PageParser.SetVariable("sec", sec.ToString());
        PageParser.SetVariable("week", week.ToString());

        #endregion

        PageParser.SetVariable("Jan", Skin.GetResValue(UserLan, "lbl_month_1"));
        PageParser.SetVariable("Feb", Skin.GetResValue(UserLan, "lbl_month_2"));
        PageParser.SetVariable("Mar", Skin.GetResValue(UserLan, "lbl_month_3"));
        PageParser.SetVariable("Apr", Skin.GetResValue(UserLan, "lbl_month_4"));
        PageParser.SetVariable("May", Skin.GetResValue(UserLan, "lbl_month_5"));
        PageParser.SetVariable("Jun", Skin.GetResValue(UserLan, "lbl_month_6"));
        PageParser.SetVariable("Jul",Skin.GetResValue(UserLan, "lbl_month_7"));
        PageParser.SetVariable("Aug", Skin.GetResValue(UserLan, "lbl_month_8"));
        PageParser.SetVariable("Sept",Skin.GetResValue(UserLan, "lbl_month_9"));
        PageParser.SetVariable("Oct", Skin.GetResValue(UserLan, "lbl_month_10"));
        PageParser.SetVariable("Nov",  Skin.GetResValue(UserLan, "lbl_month_11"));
        PageParser.SetVariable("Dec", Skin.GetResValue(UserLan, "lbl_month_12"));
        PageParser.SetVariable("PM", Skin.GetResValue(UserLan, "lbl_PM"));
        PageParser.SetVariable("AM", Skin.GetResValue(UserLan, "lbl_AM"));
        #endregion

        PageParser.SetVariable("UserMgr",Agents.UserMgr.ToString().ToLower());
        PageParser.SetVariable("MemberMgr", Agents.MemberMgr.ToString().ToLower());
        PageParser.SetVariable("BankMgr", Agents.BankMgr.ToString().ToLower());
        PageParser.SetVariable("ReportMgr", Agents.ReportMgr.ToString().ToLower());
        Agents.InitActionLineMap();
        Agents.LastCheckinTime = DateTime.Now;


    }
}