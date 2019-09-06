using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        string fullpath;
        bool isOpened = true;

        public DemoPage(MainWindow _main, Demo _demo, string _path)
        {
            InitializeComponent();
            main = _main;
            demo = _demo;
            fullpath = _path + "\\" + demo.name;
            FillMapImage();
        }

        public void FillMapImage()
        {
            BitmapImage bi = new BitmapImage();
            string path = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Images\\Maps\\" + demo.map + ".jpg";
            if (File.Exists(path))
            {
                bi.BeginInit();
                bi.UriSource = new Uri(("../Images/Maps/" + demo.map + ".jpg"), UriKind.Relative);
                bi.EndInit();
            }
            else
            {
                bi.BeginInit();
                bi.UriSource = new Uri(@"../Images/Maps/other.jpg", UriKind.Relative);
                bi.EndInit();
            }

            imgMap.Source = bi;
        }

        private void BtnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            switch (demo.source)
            {
                case Source.Valve: DemoAnalyzer.AnalyzeDemo(fullpath, demo); break;
                case Source.Cevo: break;
                case Source.Esea: break;
                case Source.Esportal: break;
                case Source.Faceit: break;
                case Source.PopFlash: break;
                case Source.Unknown: break;
                default: break;
            }
            FilterTeams();
            FillWithDemo();
            FillPlayerAvatars();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new InitialPage(main);
        }

        private void BtnLeftBar_Click(object sender, RoutedEventArgs e)
        {
            if(isOpened)
            {
                panelLeft.Visibility = Visibility.Hidden;
                panelRight.Margin = new Thickness(10, panelRight.Margin.Top, panelRight.Margin.Right, panelRight.Margin.Bottom);
                isOpened = false;
            }
            else
            {
                panelLeft.Visibility = Visibility.Visible;
                panelRight.Margin = new Thickness(440, panelRight.Margin.Top, panelRight.Margin.Right, panelRight.Margin.Bottom);
                isOpened = true;
            }
        }

        private void FilterTeams()
        {
            CollectionViewSource source1 = new CollectionViewSource() { Source = demo.players };
            CollectionViewSource source2 = new CollectionViewSource() { Source = demo.players };

            ICollectionView PlayersTeam1 = source1.View;
            ICollectionView PlayersTeam2 = source2.View;

            var filter1 = new Predicate<object>(item => ((Player)item).teamID.Equals(Team.CounterTerrorist));
            var filter2 = new Predicate<object>(item => ((Player)item).teamID.Equals(Team.Terrorist));

            PlayersTeam1.Filter = filter1;
            PlayersTeam2.Filter = filter2;

            lstTeam1Players.ItemsSource = PlayersTeam1;
            lstTeam2Players.ItemsSource = PlayersTeam2;
        }

        private void FillWithDemo()
        {
            lblTeam1.Content = demo.team1_name;
            lblTeam2.Content = demo.team2_name;

            lblTeam1Score.Content = demo.score_t1;
            lblTeam2Score.Content = demo.score_t2;

            lblTeam1CT.Content = demo.t1_ct;
            lblTeam1T.Content = demo.t1_t;

            lblTeam2CT.Content = demo.t2_ct;
            lblTeam2T.Content = demo.t2_t;
        }

        private void FillPlayerAvatars()
        {
            //List<BitmapImage> imgs = WebAccess.GetPlayerImages(demo.players);
            //int idx = 0;
            //foreach (Player p in demo.players)
            //{
            //    p.avatarUrl = "https://images.pexels.com/photos/67636/rose-blue-flower-rose-blooms-67636.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500";
            //    //idx++;
            //}
        }
    }
}
