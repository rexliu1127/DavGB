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
public partial class Report_Audit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool IsDo = false;
        if (!Agents.ReportMgr)
        {
            return;
        }
        #region Load template
        PageParser.SetTemplatesDir("../" + Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetTemplateFile("Audit.html");
        #endregion
        if (!string.IsNullOrEmpty(Request["hidDo"]) && Request["hidDo"].ToUpper() == "Y")
        {
            IsDo = true;
        }
        string UserLan = Agents.Language;
        PageParser.SetVariable("SkinRootPath", Skin.GetPublicPath(SiteSetting.SiteID));
        PageParser.SetVariable("lbl_UserName", Skin.GetResValue(UserLan, "lbl_UserName"));
        PageParser.SetVariable("lbl_Status", Skin.GetResValue(UserLan, "lbl_Status"));
        PageParser.SetVariable("lbl_from", Skin.GetResValue(UserLan, "lbl_from"));
        PageParser.SetVariable("lbl_to", Skin.GetResValue(UserLan, "lbl_to"));
        PageParser.SetVariable("lbl_TransactionType", Skin.GetResValue(UserLan, "lbl_TransactionType"));
        PageParser.SetVariable("lbl_TransactionId", Skin.GetResValue(UserLan, "lbl_TransactionId"));
        PageParser.SetVariable("lbl_Way", Skin.GetResValue(UserLan, "lbl_Way"));
         PageParser.SetVariable("lbl_BankAccount", Skin.GetResValue(UserLan, "lbl_BankAccount"));
         PageParser.SetVariable("lbl_CBankName", Skin.GetResValue(UserLan, "lbl_CBankName"));
         PageParser.SetVariable("lbl_CBankAccountName", Skin.GetResValue(UserLan, "lbl_CBankAccountName"));
         PageParser.SetVariable("lbl_CBankAccountNo", Skin.GetResValue(UserLan, "lbl_CBankAccountNo"));
         PageParser.SetVariable("lbl_date", Skin.GetResValue(UserLan, "lbl_date"));
         PageParser.SetVariable("lbl_amount", Skin.GetResValue(UserLan, "lbl_Amt"));
         PageParser.SetVariable("lbl_Submit", Skin.GetResValue(UserLan, "lbl_submit"));
         PageParser.SetVariable("lbl_Deposit", Skin.GetResValue(UserLan, "lbl_Deposit"));
         PageParser.SetVariable("lbl_Withdrawal", Skin.GetResValue(UserLan, "lbl_Withdrawal"));
         PageParser.SetVariable("Status_0", Skin.GetResValue(UserLan,"Status_0"));
         PageParser.SetVariable("Status_1", Skin.GetResValue(UserLan,"Status_1"));
         PageParser.SetVariable("Status_2", Skin.GetResValue(UserLan,"Status_2"));
         PageParser.SetVariable("Status_3", Skin.GetResValue(UserLan,"Status_3"));
         PageParser.SetVariable("Status_4", Skin.GetResValue(UserLan,"Status_4"));
         PageParser.SetVariable("Status_5", Skin.GetResValue(UserLan,"Status_5"));
         PageParser.SetVariable("lbl_Audit", Skin.GetResValue(UserLan, "lbl_Audit"));
         PageParser.SetVariable("lbl_ReferenceNo", Skin.GetResValue(UserLan, "lbl_ReferenceNoATM"));
         PageParser.SetVariable("lbl_remark", Skin.GetResValue(UserLan, "lbl_remark"));

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
         DBbase connMain = new DBbase(SiteSetting.MainConnString);
         Logger.Log("fromdate=" + Sdate);
         Logger.Log("todate=" + Edate);
         Logger.Log("transtype=" + Transtype.ToString());
         Logger.Log("transstatus=" + TransStatus.ToString());
         dt_Result = connMain.ExecSPbyParamsWithDataTable("dbo.New_Age_Get_TransData"
                                     , new SqlParameter("@fromdate", Sdate)
                                     , new SqlParameter("@todate", Edate)
                                     , new SqlParameter("@transtype", Transtype)
                                     , new SqlParameter("@transstatus", TransStatus));
         if (dt_Result.Rows.Count != 0)
         {
             for (int i = 0; i < dt_Result.Rows.Count; i++)
             {
                 int num = i+1;
                 PageParser.SetVariable("Number", num.ToString());
                 PageParser.SetVariable("UserName", dt_Result.Rows[i]["username"].ToString());
                 PageParser.SetVariable("Status", Skin.GetResValue(UserLan, "Status_" + TransStatus.ToString()));
                 PageParser.SetVariable("ReferenceNo",Convert.IsDBNull( dt_Result.Rows[i]["ReferenceNo"])?"":dt_Result.Rows[i]["ReferenceNo"].ToString());
                 PageParser.SetVariable("BankAccount",Convert.IsDBNull( dt_Result.Rows[i]["BankAccount"])?"":dt_Result.Rows[i]["BankAccount"].ToString());
                 PageParser.SetVariable("CBankName", Convert.IsDBNull(dt_Result.Rows[i]["CBankName"]) ? "" : dt_Result.Rows[i]["CBankName"].ToString());
                 PageParser.SetVariable("CBankAccountName", Convert.IsDBNull(dt_Result.Rows[i]["CBankAccountName"]) ? "" : dt_Result.Rows[i]["CBankAccountName"].ToString());
                 PageParser.SetVariable("CBankAccountNo", Convert.IsDBNull(dt_Result.Rows[i]["CBankAccountNo"]) ? "" : dt_Result.Rows[i]["CBankAccountNo"].ToString());
                 PageParser.SetVariable("TransactionId", dt_Result.Rows[i]["TransID"].ToString());
                 int PaymentType = Convert.ToInt32(dt_Result.Rows[i]["PaymentType"].ToString());
                 PageParser.SetVariable("Way", Skin.GetResValue(UserLan,"lbl_PaymentType" + PaymentType.ToString()));
                 PageParser.SetVariable("date", dt_Result.Rows[i]["TransTime"].ToString());
                 PageParser.SetVariable("amount", Convert.ToDecimal(dt_Result.Rows[i]["amount"]).ToString("##.#0"));
                 PageParser.SetVariable("remark", dt_Result.Rows[i]["memo"].ToString());
                 PageParser.ParseBlock("TransData");
             }
         }
    }
}