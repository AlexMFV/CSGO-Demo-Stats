using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DemoInfo;
using Newtonsoft.Json;
using Demo_Stats.Properties;
using System.Net;
using System.IO;
using MahApps.Metro.Controls;

namespace Demo_Stats
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Accounts accounts_collection = new Accounts();

        public MainWindow()
        {
            InitializeComponent();
            this.TitleForeground = new SolidColorBrush(Colors.Black);
            this.MainFrame.Content = new Views.InitialPage(this);
        }
    }
}