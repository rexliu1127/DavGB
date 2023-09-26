using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SB.Agent.Users;
using SB.Agent.Sites;
using SB.Agent.Common;
using SB.DBLibrary;
using SB.Common.Files;
public partial class User_UpdMember : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int CustID = 0;
        string Firstname = "";
        string Lastname = "";
        string EMail = "";
        int Gendar = 0;
        string Birthday = "";
        string Country = "";
        string Address = "";
        string Citytown = "";
        string State = "";
        string Zipcode = "";
        string Mobile = "";
        if (!Agents.MemberMgr)
        {
            return;
        }
        Int32.TryParse(Request["ToCID"], out CustID);
        if (CustID == 0)
        {
            return;
        }
        Firstname = string.IsNullOrEmpty(Request["txtFirstname"]) ? "" : Request["txtFirstname"];
        Lastname = string.IsNullOrEmpty(Request["txtLastname"]) ? "" : Request["txtLastname"];
        int.TryParse(Request["selGendar"], out Gendar);
        Birthday = Request["Selmon"] + "/" + Request["txtday"] + "/" + Request["txtyear"];
        Country = string.IsNullOrEmpty(Request["selCountry"]) ? "" : Request["selCountry"];
        Address = string.IsNullOrEmpty(Request["txtaddress"]) ? "" : Request["txtaddress"];
        Citytown = string.IsNullOrEmpty(Request["txtCitytown"]) ? "" : Request["txtCitytown"];
        State = string.IsNullOrEmpty(Request["txtState"]) ? "" : Request["txtState"];
        Zipcode = string.IsNullOrEmpty(Request["txtZip_code"]) ? "" : Request["txtZip_code"];
        Mobile = string.IsNullOrEmpty(Request["txtPrimary_phone"]) ? "" : Request["txtPrimary_phone"];
        EMail = string.IsNullOrEmpty(Request["txtEMail"]) ? "" : Request["txtEMail"];

        try
        {
            DataTable dt_Result = new DataTable();
            string SpName = "";

            SpName = "New_Age_MemberUpd";


            DBbase connMain = new DBbase(SiteSetting.MainConnString);

            connMain.ExecuteByParameters(SpName, ref dt_Result, new SqlParameter("@custid", CustID)
                                                                , new SqlParameter("@firstname", Firstname)
                                                                , new SqlParameter("@lastname", Lastname)
                                                                , new SqlParameter("@address", Address)
                                                                , new SqlParameter("@City", Citytown)
                                                                , new SqlParameter("@state", State)
                                                                , new SqlParameter("@country", Country)
                                                                , new SqlParameter("@phone", Mobile)
                                                                , new SqlParameter("@email", EMail)
                                                                , new SqlParameter("@birthday", Birthday)
                                                                , new SqlParameter("@gender", Gendar)
                                                                 , new SqlParameter("@ZipCode", Zipcode)

                                                                );
            string UserLan = Agents.Language;
            string Result = Skin.GetResValue(UserLan, "lbl_UpdateSuccess");
            Response.Write("ok");

        }
        catch (Exception ex)
        {
            Logger.LogException(ex, "New_Age_UserUpd Error");
            throw ex;
        }

    }
}