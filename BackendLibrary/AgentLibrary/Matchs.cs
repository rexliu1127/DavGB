using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SB.Common.Files;
using SB.DBLibrary;
using SB.Agent.Sites;
/// <summary>
/// Matchs 的摘要描述
/// </summary>
namespace SB.Agent.Common
{
    public class Matchs
    {
        public Matchs()
        {

        }

        public static DataTable GetRunningLeague(string Market,int SportType)
        {
            DataTable dt_League = new DataTable();

            Market = Market.ToLower();
            try
            {
                DBbase connMain = new DBbase(SiteSetting.MainConnString);
                connMain.ExecuteByParameters("New_Get_RunningLeagues", ref dt_League, new SqlParameter("@RequestType", Market)
                                                                                    , new SqlParameter("@sporttype", SportType));
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "GetRunningLeague Error");
                throw ex;
            }
            return dt_League;
        }

        public static DataTable GetRunningMatch(int LeagueID,string Market,int SportType)
        {
            DataTable dt_Match = new DataTable();
            Market = Market.ToLower();
            try
            {
                DBbase connMain = new DBbase(SiteSetting.MainConnString);
                connMain.ExecuteByParameters("dbo.New_Get_RunningEvents", ref dt_Match, new SqlParameter("@leagueid", LeagueID)
                                                                                    , new SqlParameter("@RequestType", Market)
                                                                                    , new SqlParameter("@sporttype", SportType));
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "GetRunningMatch Error");
                throw ex;
            }
            return dt_Match;
        }
    }
}