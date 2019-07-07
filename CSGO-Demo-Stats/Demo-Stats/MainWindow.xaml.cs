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
using System.Drawing;
using System.Net;
using System.IO;

namespace Demo_Stats
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public Accounts accounts_collection = new Accounts();

        public MainWindow()
        {
            InitializeComponent();
            this.Content = new Views.InitialPage(this);
        }

        private void BtnRefresh_MouseUp1(object sender, MouseButtonEventArgs e)
        {
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cache.SaveAccounts(accounts_collection);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void OpenSettings()
        {
            this.Content = new Settings();
        }
    }
}