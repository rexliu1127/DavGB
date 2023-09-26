using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Globalization;



using aspxtemplate;

using System.Collections.Specialized;

using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;

using System.Data.SqlClient;
using member_site;
using System.Data;
using SB.DBLibrary;
using System.Web.Configuration;

//Status_0 Pending
//Status_1 Approved 00939. Deposit
//Status_2 Failed 00938. Deposit
//Status_3 Rejected 00940. Deposit
//Status_4 Cancelled 00941. Deposit
//Status_5 Processing 00942. Deposit

public partial class DepositResult :ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {



        #region Load Template

        //測試
        //string SkinRootPath = "template/Standard/public/";
        //PageParser.SetTemplatesDir(SkinRootPath);        
        //PageParser.SetTemplateFile("aopDepositResult.html");
        //IBCDBLibrary.DBbase conn = new IBCDBLibrary.DBbase(WebConfigurationManager.AppSettings["MainConnectionString"]);


        //正式
        string SkinRootPath = SelLang.GetSkinRootPath();
        PageParser.SetTemplatesDir(SkinRootPath + "/public/");
        PageParser.SetVariable("SkinRootPath", SkinRootPath);
        PageParser.SetTemplateFile("aopDepositResult.html");
        SB.DBLibrary.DBbase conn = new SB.DBLibrary.DBbase(Common.ConnectionString);

        #endregion


        //Alipay parameter
        string payment_gateway = "";
        string app_id = "";
        string method = "";
        string format = "";
        string return_url = "";
        string notify_url = "";
        string charset = "";
        string sign_type = "";
        string version = "";

        string timeout_express = "";
        string product_code = "";

        string alipay_public_key = "";
        string merchant_private_key = "";


        payment_gateway = WebConfigurationManager.AppSettings["payment_gateway"];
        app_id = WebConfigurationManager.AppSettings["app_id"];
        method = WebConfigurationManager.AppSettings["method"];
        format = WebConfigurationManager.AppSettings["format"];
        return_url = WebConfigurationManager.AppSettings["return_url"];
        notify_url = WebConfigurationManager.AppSettings["notify_url"];
        charset = WebConfigurationManager.AppSettings["charset"];
        sign_type = WebConfigurationManager.AppSettings["sign_type"];
        version = WebConfigurationManager.AppSettings["version"];
        timeout_express = WebConfigurationManager.AppSettings["timeout_express"];
        product_code = WebConfigurationManager.AppSettings["product_code"];


        alipay_public_key = WebConfigurationManager.AppSettings["alipay_public_key"];
        merchant_private_key = WebConfigurationManager.AppSettings["merchant_private_key"];




        member_site.SelLang selLan = new member_site.SelLang(Auth.GetUserLang(this.Context));


        
        int status = 1;

        string out_trade_no = "";
        string trade_no = "";

        //成功條件:out_trade_no!=null,trade_no!=null

        if (Request.QueryString["out_trade_no"] != null && Request.QueryString["trade_no"] != null)
        {

            out_trade_no = Request.QueryString["out_trade_no"];
            trade_no = Request.QueryString["trade_no"];


            DefaultAopClient client = new DefaultAopClient(payment_gateway, app_id, merchant_private_key, format, version, sign_type, alipay_public_key, charset);


            

            AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
            request.BizContent = "{" +
            "    \"out_trade_no\":\""+out_trade_no+"\"," +
            "    \"trade_no\":\""+ trade_no + "\"" +
            "  }";
            AlipayTradeQueryResponse response = client.Execute(request);

            if (response.TradeNo == trade_no && response.OutTradeNo == out_trade_no && Request.QueryString["total_amount"] == response.TotalAmount)
            {
                //反查成功
                status = 1;
            }
            else
            {
                //反查失敗
                status = 2;
            }

            try
            {
                //更新交易狀態及回應碼,並更新Customer ActiveCode(SP會判斷status=2才更新ActiveCode)
                conn.ExecuteSPbyParameters("dbo.Update_Aop_Transaction_TransStatus_ResponseLog_Customer_ActiveCode"
                                        , new SqlParameter("@TransID", out_trade_no) //支付寶回跳參數-商家訂單序號
                                        , new SqlParameter("@TransStatus", status) //交易狀態
                                        , new SqlParameter("@ReferenceNo", trade_no) //支付寶回跳參數-支付寶交易流水號
                                        , new SqlParameter("@ResponseLog", Request.Url.Query)
                                        );


                if (status == 1)
                {


                    PageParser.SetVariable("lbl_Msg", selLan.GetResourceValue("lbl_successful"));

                    //內部加值點數流程
                    #region RechargePoint



                    #endregion
                }
                else
                {
                    PageParser.SetVariable("lbl_Msg", selLan.GetResourceValue("lbl_failed"));
                }
            }
            catch (Exception ex)
            {

                Logger.LogException(ex, "Update_Aop_Transaction_TransStatus_ResponseLog_Customer_ActiveCode Error");
            }

        }
        


        


    }

    #region ClassUtility Code

    public static decimal getNumericDecimalDefault(string strText, int defaultValue)
    {
        try
        {
            if (String.IsNullOrEmpty(strText))
            {
                return defaultValue;
            }
            else
            {
                if (isNumeric(strText))
                {
                    return decimal.Parse(strText);
                }
                else
                {
                    return defaultValue;
                }
            }
        }
        catch (Exception)
        {
            return defaultValue;
        }

    }

    public static bool isNumeric(object Expression)
    {

        bool isNum;

        double retNum;

        isNum = double.TryParse(Convert.ToString(Expression), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }

}


#endregion