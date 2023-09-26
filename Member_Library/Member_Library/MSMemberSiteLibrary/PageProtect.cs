using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace member_site
{
    public class PageProtect
    {
        private HttpContext oPage = HttpContext.Current;
        private string _pageNameStr;

        public string PageName
        {
            get { return _pageNameStr; }
        }

        public PageProtect()
        {
            _pageNameStr = oPage.Request.AppRelativeCurrentExecutionFilePath.ToLower();

            if (_pageNameStr.LastIndexOf("/") > -1)
            {
                _pageNameStr = _pageNameStr.Substring(_pageNameStr.LastIndexOf("/") + 1, _pageNameStr.Length - _pageNameStr.LastIndexOf("/") - 1);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DurationInMilliSecords">Duration In Millisecords</param>
        /// <returns></returns>
        public bool isReachRapidLimitation(double DurationInMilliSecords)
        {
            return isReachRapidLimitation(DurationInMilliSecords, _pageNameStr);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DurationInMilliSecords">Duration In Millisecords</param>
        /// <param name="SessionNamePrefix"></param>
        /// <returns></returns>
        public bool isReachRapidLimitation(double DurationInMilliSecords, string SessionNamePrefix)
        {
            bool isRobotUser = false;

            string oLastTimeSessionName = SessionNamePrefix + "LastTime";
            try
            {
                if (oPage.Session[oLastTimeSessionName] != null)
                {
                    TimeSpan DurationBetweenTwoBet = DateTime.Now - Convert.ToDateTime(oPage.Session[oLastTimeSessionName]);
                    if (DurationBetweenTwoBet.TotalMilliseconds < DurationInMilliSecords)
                    {
                        isRobotUser = true;
                    }
                }

                oPage.Session[oLastTimeSessionName] = DateTime.Now;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "PageProtect");
            }

            return isRobotUser;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DurationInMilliSecords">Duration In Millisecords</param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public bool isReachTotalCountLimitation(double DurationInMilliSecords, int TotalCount)
        {
            return isReachTotalCountLimitation(DurationInMilliSecords, TotalCount, _pageNameStr);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DurationInMilliSecords">Duration In Millisecords</param>
        /// <param name="TotalCount">implement in the page Total Count it is to check in duration time</param>
        /// <param name="SessionNamePrefix"></param>
        /// <returns></returns>
        public bool isReachTotalCountLimitation(double DurationInMilliSecords, int TotalCount, string SessionNamePrefix)
        {
            bool isRobotUser = false;

            string oCountOneTimeSessionName = SessionNamePrefix + "CountOneTime";
            string oCounterSessionName = SessionNamePrefix + "Counter";
            try
            {
                if (oPage.Session[oCounterSessionName] == null)
                {
                    oPage.Session[oCounterSessionName] = 1;
                    oPage.Session[oCountOneTimeSessionName] = DateTime.Now;
                }
                else
                {
                    oPage.Session[oCounterSessionName] = Convert.ToInt32(oPage.Session[oCounterSessionName]) + 1;
                    if (Convert.ToInt32(oPage.Session[oCounterSessionName]) > TotalCount)
                    {
                        TimeSpan DurationBetweenTwoBet = DateTime.Now - Convert.ToDateTime(oPage.Session[oCountOneTimeSessionName]);
                        if (DurationBetweenTwoBet.TotalMilliseconds < DurationInMilliSecords)
                        {
                            isRobotUser = true;
                        }

                        oPage.Session.Remove(oCounterSessionName);
                        oPage.Session.Remove(oCountOneTimeSessionName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "PageProtect");
            }

            return isRobotUser;
        }
    }
}
