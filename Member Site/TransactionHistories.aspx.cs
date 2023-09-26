using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using aspxtemplate;
using member_site;
public partial class TransactionHistories : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool IsDo = false;
        if (!member_site.Auth.ValidateLogin(this.Context)) return;
        if (Session["ServerInfo_key"] == null) return;
        #region Load Template

        string SkinRootPath = SelLang.GetSkinRootPath();
        PageParser.SetTemplatesDir(SkinRootPath + "public/");
        PageParser.SetTemplateFile("TransactionHistories.html");
        #endregion

        if (!string.IsNullOrEmpty(Request["hidDo"]) && Request["hidDo"].ToUpper() == "Y")
        {
            IsDo = true;
        }

        member_site.SelLang selLan = new member_site.SelLang(Auth.GetUserLang(this.Context));
        PageParser.SetVariable("SkinRootPath", SkinRootPath);
        PageParser.SetVariable("lbl_TransactionHistories", selLan.GetResourceValue("lbl_TransactionHistories"));
        PageParser.SetVariable("lbl_DepositWithdrawalHistory", selLan.GetResourceValue("lbl_DepositWithdrawalHistory"));
        PageParser.SetVariable("lbl_from", selLan.GetResourceValue("lbl_from"));
        PageParser.SetVariable("lbl_to", selLan.GetResourceValue("lbl_to"));
        PageParser.SetVariable("lbl_TransactionType", selLan.GetResourceValue("lbl_TransactionType"));
        PageParser.SetVariable("lbl_DepositStatus", selLan.GetResourceValue("lbl_DepositStatus"));
        PageParser.SetVariable("lbl_submit", selLan.GetResourceValue("lbl_submit"));

        PageParser.SetVariable("lbl_all", selLan.GetResourceValue("lbl_all"));
        PageParser.SetVariable("lbl_Deposit", selLan.GetResourceValue("lbl_Deposit"));
        PageParser.SetVariable("lbl_Withdrawal", selLan.GetResourceValue("lbl_Withdrawal"));



        PageParser.SetVariable("Status_0", selLan.GetResourceValue("Status_0"));
        PageParser.SetVariable("Status_1", selLan.GetResourceValue("Status_1"));
        PageParser.SetVariable("Status_2", selLan.GetResourceValue("Status_2"));
        PageParser.SetVariable("Status_3", selLan.GetResourceValue("Status_3"));
        PageParser.SetVariable("Status_4", selLan.GetResourceValue("Status_4"));
        PageParser.SetVariable("Status_5", selLan.GetResourceValue("Status_5"));

        PageParser.SetVariable("lbl_TransactionId", selLan.GetResourceValue("lbl_TransactionId"));
        PageParser.SetVariable("lbl_Way", selLan.GetResourceValue("lbl_Way"));
        PageParser.SetVariable("lbl_date", selLan.GetResourceValue("lbl_date"));
        PageParser.SetVariable("lbl_amount", selLan.GetResourceValue("lbl_amount"));

        string Sdate = DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy");
        string Edate = DateTime.Today.ToString("MM/dd/yyyy");

        int Transtype = 1;
        int TransStatus = 0;

        if (IsDo)
        {
            Sdate = Request["s_date"];
            Edate = Request["e_date"];
            Transtype = Convert.ToInt32(Request["selTransType"]);
            TransStatus = Convert.ToInt32(Request["selTransStatus"]);
        }
        PageParser.SetVariable("FromDate", Sdate);
        PageParser.SetVariable("ToDate", Edate);
        PageParser.SetVariable("defTranstype", Transtype.ToString());
        PageParser.SetVariable("defTransStatus", TransStatus.ToString());
        

        PageParser.UpdateBlock("TransData");

        DataTable dt_Result = new DataTable();
        DBbase conn = new DBbase(member_site.Common.ConnectionString);
        dt_Result = conn.ExecSPbyParamsWithDataTable("dbo.Dot_Cust_Get_TransData", 
                                    new SqlParameter("@custid", Auth.GetUserID(HttpContext.Current))
                                    , new SqlParameter("@fromdate", Sdate)
                                    , new SqlParameter("@todate", Edate)
                                    , new SqlParameter("@transtype", Transtype)
                                    , new SqlParameter("@transstatus", TransStatus));
        if (dt_Result.Rows.Count != 0)
        {
            for(int i=0;i<dt_Result.Rows.Count;i++)
            {
                PageParser.SetVariable("TransactionId", dt_Result.Rows[i]["TransID"].ToString());
                int PaymentType = Convert.ToInt32(dt_Result.Rows[i]["PaymentType"].ToString());
                PageParser.SetVariable("Way",  selLan.GetResourceValue("lbl_PaymentType" + PaymentType.ToString()));
                PageParser.SetVariable("date", dt_Result.Rows[i]["TransTime"].ToString());
                PageParser.SetVariable("amount", Convert.ToDecimal( dt_Result.Rows[i]["amount"]).ToString("##.#0"));
                PageParser.ParseBlock("TransData");
            }
        }
    }
}