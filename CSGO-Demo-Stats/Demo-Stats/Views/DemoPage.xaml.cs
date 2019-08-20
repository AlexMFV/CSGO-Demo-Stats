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
            DemoAnalyzer.AnalyzeDemo(fullpath, demo);
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
    }
}
