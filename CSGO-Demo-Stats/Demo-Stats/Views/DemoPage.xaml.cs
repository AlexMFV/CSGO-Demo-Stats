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
                parser.TeamScoreChange += Parser_TeamScoreChange;
                parser.RoundStart += Parser_RoundStart;
                parser.ParseToEnd();

                //while (parser.ParseNextTick()) //Manual Demo Parse
                //{
                //    lblRoundNumber.Content = parser.CurrentTick;
                //}
            }
        }

        private void Parser_RoundStart(object sender, RoundStartedEventArgs e)
        {
            lblRoundNumber.Content = "Round: " + (parser.CTScore + parser.TScore + 1);
        }

        private void Parser_TeamScoreChange(object sender, TeamScoreChangeEventArgs e)
        {
            lblTeam1Score.Content = parser.CTScore;
            lblTeam2Score.Content = parser.TScore;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new InitialPage(main);
        }
    }
}
