using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using member_site;
using aspxtemplate;

public partial class ChangeLanguage : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Auth.IsLegalRequest(Session)) return;
        string selLang = "";
        string refreshPage = "";
        string LoginMode = System.Web.Configuration.WebConfigurationManager.AppSettings["LoginMode"];
        if (!string.IsNullOrEmpty(Request["hidSelLang"]))
        {
            selLang = Request["hidSelLang"];

            
            member_site.Auth.SetUserLang(this.Context, selLang);
            
            // Store Language selection into cookie
            HttpCookie LangCookie = new HttpCookie("LangKey");
            LangCookie.Value = selLang;
            LangCookie.Expires = DateTime.Now.AddYears(2);
         //   LangCookie.Domain = Session["ServerInfo_key"].ToString();
            Response.Cookies.Add(LangCookie);
        }
        else
        {
            selLang = SelLang.GetDefaultLan(Context);
            member_site.Auth.SetUserLang(this.Context, selLang);
        }

        if (!string.IsNullOrEmpty(Request["hidIsLogin"]))
        {
            #region After Login
            if (Convert.ToBoolean ( Request["hidIsLogin"].ToString().ToLower()))
            {

                if (Request["DefaultMainPage"] != null)
                {
                    Session["CurrMainPage_logon"] = Request["DefaultMainPage"].ToString();
                    Session["CurrLeftPage_logon"] = Request["DefaultLeftPage"].ToString();


                        if (Session["CurrMainPage_logon"].ToString().Contains("TopOddsData2"))
                        {
                            Session["CurrMainPage_logon"] = "maincontent.aspx";
                        }
                    
                }
                else
                    if (Session["CurrMainPage_logon"] != null)
                    {

                            if (Session["CurrMainPage_logon"].ToString().Contains("TopOddsData2"))
                            {
                                Session["CurrMainPage_logon"] = "maincontent.aspx";
                            }
                        

                        if (Session["CurrMainPage_logon"].ToString().Contains("home.aspx"))
                        {
                            Session["CurrLeftPage_logon"] = "";
                        }


                    }
                    else
                    {
                        Session["CurrMainPage_logon"] = "home.aspx?Ig=yes";
                        Session["CurrLeftPage_logon"] = "LeftAllInOne.aspx";
                    }

                    refreshPage = "main.aspx"; //user already login
                
            }
            #endregion
            #region Before Login
            else
            {


                        if (Session["CurrMainPage_nolog"] != null)
                        {

                                if (Session["CurrMainPage_nolog"].ToString().Contains("TopOddsData2"))
                                {
                                    Session["CurrMainPage_nolog"] = "maincontent.aspx";
                                }
                            

                            if (Session["CurrMainPage_nolog"].ToString().Contains("home.aspx"))
                            {
                                Session["CurrLeftPage_nolog"] = "";
                            }
                        }
                        else
                        {
                            Session["CurrMainPage_nolog"] = "home.aspx?Ig=no";
                            Session["CurrLeftPage_nolog"] = "leftallinone.aspx";
                        }



                            refreshPage = "index.aspx";
                        
                

            }
            #endregion
        }
        else
        {

            Session["CurrMainPage_nolog"] = "maincontent.aspx";
            Session["CurrLeftPage_nolog"] = "leftallinone.aspx";
            

                refreshPage = "index.aspx";
            
        }
        Response.Redirect ( refreshPage);
    }

    protected override void Render(HtmlTextWriter writer)
    {

    }
}
