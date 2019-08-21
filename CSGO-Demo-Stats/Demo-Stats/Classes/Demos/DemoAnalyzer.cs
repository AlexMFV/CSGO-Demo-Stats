using DemoInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Stats
{
    public static class DemoAnalyzer
    {
        static DemoParser parser;
        static Demo demo;

        //Workarounds
        static bool processRound = false; //TEMPORARY FIX??
        static bool roundEndOccurred = false;
        static string RoundWonBy = null;

        public static void AnalyzeDemo(string path, Demo _demo)
        {
            demo = _demo;
            demo.team1_name = "Team 1";
            demo.team2_name = "Team 2";

            using (FileStream stream = File.OpenRead(path))
            {
                parser = new DemoParser(stream);

                //Parses the first few hundred bytes of the demo, with initial and important data
                parser.ParseHeader();

                //Round Events
                parser.RoundStart += Parser_RoundStart;
                parser.RoundEnd += Parser_RoundEnd;
                parser.RoundOfficiallyEnd += Parser_RoundOfficiallyEnd;
                parser.RankUpdate += Parser_RankUpdate;

                //Player Events
                parser.PlayerTeam += Parser_PlayerTeam;

                //Score Events
                parser.TeamScoreChange += Parser_TeamScoreChange;

                //Parse till the end of the demo
                parser.ParseToEnd();
            }
        }

        #region Round Events

        private static void Parser_RoundStart(object sender, RoundStartedEventArgs e)
        {
            roundEndOccurred = false;
        }

        private static void Parser_RoundEnd(object sender, RoundEndedEventArgs e)
        {
            roundEndOccurred = true;

            if (e.Winner != Team.Spectate)
                processRound = true;
        }

        private static void Parser_RoundOfficiallyEnd(object sender, RoundOfficiallyEndedEventArgs e)
        {
            //TEMPORARY FIX, WILL FIX WHEN CLASSES ARE FINISHED
            if (!roundEndOccurred)
            {
                if (demo.rounds < 16)
                {
                    if (RoundWonBy == "CT")
                        demo.t1_ct++;
                    else
                        demo.t2_t++;
                }
                else
                {
                    if (RoundWonBy == "CT")
                        demo.t2_ct++;
                    else
                        demo.t1_t++;
                }
            }
        }

        #endregion

        #region Player Events

        private static void Parser_PlayerTeam(object sender, PlayerTeamEventArgs e)
        {
            if (!e.IsBot && e.NewTeam != Team.Spectate && !isPlayerInTeam(e.Swapped.SteamID))
                demo.players.Add(new Player(e.NewTeam, e.Swapped.Name, e.Swapped.SteamID, e.Swapped.EntityID));
        }

        private static void Parser_RankUpdate(object sender, RankUpdateEventArgs e)
        {
            foreach (Player p in demo.players)
            {
                if (p.steamID == e.SteamId.ToString())
                {
                    p.rankChange = (int)e.RankChange;
                    p.rank = e.RankOld;
                    p.winCount = e.WinCount;
                }
            }
        }

        #endregion

        #region Score Events

        private static void Parser_TeamScoreChange(object sender, TeamScoreChangeEventArgs e)
        {
            if (e.Team != Team.Spectate && processRound) //e.Team != Team.Spectate && processRound
            {
                if (demo.rounds < 16) //First Half
                {
                    if (e.Team == Team.CounterTerrorist)
                    {
                        demo.score_t1++;
                        demo.t1_ct++;
                        RoundWonBy = "CT";
                    }
                    else
                    {
                        demo.score_t2++;
                        demo.t2_t++;
                        RoundWonBy = "T";
                    }
                }
                else //Second Half
                {
                    if (e.Team == Team.CounterTerrorist)
                    {
                        demo.score_t2++;
                        demo.t2_ct++;
                        RoundWonBy = "CT";
                    }
                    else
                    {
                        demo.score_t1++;
                        demo.t1_t++;
                        RoundWonBy = "T";
                    }
                }
            }
            demo.rounds = parser.CTScore + parser.TScore + 1;
            processRound = false;
        }

        #endregion

        #region Methods

        private static bool isPlayerInTeam(long steamID)
        {
            foreach (Player p in demo.players)
                if (p.steamID == steamID.ToString())
                    return true;
            return false;
        }

        #endregion
    }
}
