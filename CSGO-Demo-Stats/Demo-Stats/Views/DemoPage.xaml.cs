using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DemoInfo;

namespace Demo_Stats.Views
{
    /// <summary>
    /// Interaction logic for DemoPage.xaml
    /// </summary>
    public partial class DemoPage : Page
    {
        MainWindow main;
        Demo demo;
        DemoParser parser;
        string fullpath;

        //Round Variables
        int curr_round = 0;
        int t1_ct = 0;
        int t1_t = 0;
        int t2_ct = 0;
        int t2_t = 0;

        //Workarounds
        bool processRound = false; //TEMPORARY FIX??
        bool roundEndOccurred = false;
        string RoundWonBy = null;

        public DemoPage(MainWindow _main, Demo _demo, string _path)
        {
            InitializeComponent();
            main = _main;
            demo = _demo;
            fullpath = _path + "\\" + demo.name;
        }

        private void BtnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            AnalyzeDemo();
        }

        public void AnalyzeDemo()
        {
            using (FileStream stream = File.OpenRead(fullpath))
            {
                parser = new DemoParser(stream);
                parser.ParseHeader();
                parser.RoundStart += Parser_RoundStart;
                parser.RoundEnd += Parser_RoundEnd;
                parser.RoundOfficiallyEnd += Parser_RoundOfficiallyEnd;
                parser.TeamScoreChange += Parser_TeamScoreChange;
                parser.ParseToEnd();

                //while (parser.ParseNextTick()) //Manual Demo Parse
                //{
                //    lblRoundNumber.Content = parser.CurrentTick;
                //}
            }
        }

        private void Parser_RoundOfficiallyEnd(object sender, RoundOfficiallyEndedEventArgs e)
        {
            //TEMPORARY FIX, WILL FIX WHEN CLASSES ARE FINISHED
            if (!roundEndOccurred)
            {
                if(curr_round < 16)
                {
                    if (RoundWonBy == "CT")
                        t1_ct++;
                    else
                        t2_t++;
                }
                else
                {
                    if (RoundWonBy == "CT")
                        t2_ct++;
                    else
                        t1_t++;
                }
            }
        }

        private void Parser_RoundEnd(object sender, RoundEndedEventArgs e)
        {
            roundEndOccurred = true;

            if (e.Winner != Team.Spectate)
                processRound = true;
        }

        private void Parser_RoundStart(object sender, RoundStartedEventArgs e)
        {
            roundEndOccurred = false;
            lblRoundNumber.Content = "Round: " + curr_round;
        }

        private void Parser_TeamScoreChange(object sender, TeamScoreChangeEventArgs e)
        {
            if (e.Team != Team.Spectate && processRound) //e.Team != Team.Spectate && processRound
            {
                if (curr_round < 16) //First Half
                {
                    if (e.Team == Team.CounterTerrorist)
                    {
                        lblTeam1Score.Content = parser.CTScore;
                        t1_ct++;
                        lblTeam1CT.Content = t1_ct;
                        RoundWonBy = "CT";
                    }
                    else
                    {
                        lblTeam2Score.Content = parser.TScore;
                        t2_t++;
                        lblTeam2T.Content = t2_t;
                        RoundWonBy = "T";
                    }
                }
                else
                {
                    if (e.Team == Team.CounterTerrorist)
                    {
                        lblTeam2Score.Content = parser.CTScore;
                        t2_ct++;
                        lblTeam2CT.Content = t2_ct;
                        RoundWonBy = "CT";
                    }
                    else
                    {
                        lblTeam1Score.Content = parser.TScore;
                        t1_t++;
                        lblTeam1T.Content = t1_t;
                        RoundWonBy = "T";
                    }
                }
            }
            curr_round = parser.CTScore + parser.TScore + 1;
            processRound = false;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new InitialPage(main);
        }
    }
}
