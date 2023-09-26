using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

using System.Text;
using System.Collections;
namespace member_site
{

    public class Function
    {
        private static DateTime datetimeLastUpdateTime_T = DateTime.Now;



        /// <summary>
        /// Author: Eddie Chen
        /// Check whether the data is datetime or not
        /// </summary>
        /// <returns>bool</returns>
        public static bool IsDateTime(object data)
        {
            if (data == null) return false;
            try
            {
                Convert.ToDateTime(data);
                return true;
            }
            catch
            {
                return false;
            }
        }




        /*
         * Author: Victor Chen 
         * Date: 2007/04/19
         * Update Author: Victor Chen
         * Update Date: 2007/04/20
         */
        public static string right(string inputString, int index)
        {
            return inputString.Substring(inputString.Length - index, index);
        }

        /*
         * Author: Victor Chen 
         * Date: 2007/04/19
         * Update Author: Victor Chen
         * Update Date: 2007/04/20
         */
        public static string left(string inputString, int index)
        {
            return inputString.Substring(0, index);
        }




        /*
         * Author: Victor Chen 
         * Date: 2007/04/19
         * Update Author: Victor Chen
         * Update Date: 2007/04/20
         */
        public static double format1000(double number)
        {

            return number - (number % 100);
        }

        /*
         * Author: Victor Chen 
         * Date: 2007/04/19
         * Update Author: Victor Chen
         * Update Date: 2007/04/20
         */
        public static String getDateTimeWithCulTure(String cultureInfo, DateTime dateTime, String pattern)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureInfo);
            return dateTime.ToString(pattern);
        }


        /*
        * Author: Victor Chen 
        * Date: 2007/05/23
        * Update Author: Victor Chen
        * Update Date: 2007/05/23
        */
        public static double Floor(double d, int num)
        {
            return Convert.ToDouble(Regex.Replace(Convert.ToString(d), "^([-+]?[0-9]*.[0-9]{" + num + "})[0-9]*$", "$1"));
        }

        /*
         * Author: Yai
         * Date: 2007/04/23
         * Update Author: David Wu
         * Update Date: 2007/05/11
         */
        public static string FormatN(double fN, int iDig)
        {
            /*string str = Microsoft.VisualBasic.Strings.FormatNumber(fN, iDig, Microsoft.VisualBasic.TriState.True, Microsoft.VisualBasic.TriState.False, Microsoft.VisualBasic.TriState.False);
            str = (Microsoft.VisualBasic.Strings.Right(str, 1) == "0") ? Microsoft.VisualBasic.Strings.Left(str, str.Length - 1) : str;
            return str;*/

            double dblFn = Math.Round(Convert.ToDouble(fN), 5);

            switch (iDig)
            {
                case 0:
                    return Floor(dblFn, iDig).ToString(); 
                case 2:
                    dblFn = dblFn * 100;
                    dblFn = Math.Truncate(Convert.ToDouble(dblFn.ToString()));
                    dblFn = Convert.ToDouble(dblFn.ToString()) / 100;
                    return dblFn.ToString("0.#0");
                case 3:
                    dblFn = dblFn * 1000;
                    dblFn = Math.Truncate(Convert.ToDouble(dblFn.ToString()));
                    dblFn = Convert.ToDouble(dblFn.ToString()) / 1000;
                    return dblFn.ToString("0.##0");
                default:
                    dblFn = dblFn * 1000;
                    dblFn = Math.Truncate(Convert.ToDouble(dblFn.ToString()));
                    dblFn = Convert.ToDouble(dblFn.ToString()) / 1000;
                    return dblFn.ToString("0.##0");
            }

        }

        /*
         * Author: Yai
         * Date: 2007/04/23
         * Update Author: Yai
         * Update Date: 2007/04/25
         */
        public static string FormatDateTime(DateTime aDate)
        {
            return aDate.ToString("MM/dd/yyyy HH:mm:ss");
        }

        /*
         * Author: David Wu
         * Date: 2007/04/26
         * Update Author: David Wu
         * Update Date: 2007/04/26
         */

        public static double Getrefno()
        {
            member_site.DBbase conn = new member_site.DBbase(member_site.Common.ConnectionString);
            DataTable dt = conn.ExecuteQuery("refno");

            return Convert.ToDouble(dt.Rows[0]["refno"]) + 100000000000;
        }

        /*
      * Author: David Shih
      * Date: 2007/04/26
      * Update Author: David Shih
      * Update Date: 2007/04/26
      */

        public static DataTable Load_SiteSetting_Session(HttpContext hc)
        {
            DataTable dt = null;
            if (hc.Cache["ss_data"] != null)
            {
                dt = (DataTable)hc.Cache["ss_data"];

            }
            else
            {

                member_site.DBbase conn = new member_site.DBbase(member_site.Common.ConnectionString);
                dt = conn.ExecuteQuery("Dot_SelectSite");
                hc.Cache.Insert("ss_data", dt, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                //using (SqlDataSource dsr = conn.GetDataSource("Dot_SelectSite"))
                //{

                //    DataView dv = (DataView)dsr.Select(DataSourceSelectArguments.Empty);
                //    datetimeLastUpdateTime_T = DateTime.Now;
                //    dt = dv.Table;
                //    hc.Cache.Insert("ss_data", dt, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                //    dv.Dispose();
                //}

            }
            return dt;
        }

   


    }




}