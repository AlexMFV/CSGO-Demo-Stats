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
            using (FileStream stream = File.OpenRead(path))
            {
                parser = new DemoParser(stream);

                //Parses the first few hundred bytes of the demo, with initial and important data
                parser.ParseHeader();

                //Round Events
                parser.RoundStart += Parser_RoundStart;
                parser.RoundEnd += Parser_RoundEnd;
                parser.RoundOfficiallyEnd += Parser_RoundOfficiallyEnd;

                //Score Events
                parser.TeamScoreChange += Parser_TeamScoreChange;

                //Parse till the end of the demo
                parser.ParseToEnd();
            }
        }

        #region Round Methods

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

        #region Score Methods

        private static void Parser_TeamScoreChange(object sender, TeamScoreChangeEventArgs e)
        {
            if (e.Team != Team.Spectate && processRound) //e.Team != Team.Spectate && processRound
            {
                if (demo.rounds < 16) //First Half
                {
                    if (e.Team == Team.CounterTerrorist)
                    {
                        demo.t1_ct++;
                        RoundWonBy = "CT";
                    }
                    else
                    {
                        demo.t2_t++;
                        RoundWonBy = "T";
                    }
                }
                else //Second Half
                {
                    if (e.Team == Team.CounterTerrorist)
                    {
                        demo.t2_ct++;
                        RoundWonBy = "CT";
                    }
                    else
                    {
                        demo.t1_t++;
                        RoundWonBy = "T";
                    }
                }
            }
            demo.rounds = parser.CTScore + parser.TScore + 1;
            processRound = false;
        }

        #endregion
    }
}
