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
        Accounts collection = new Accounts();

        public MainWindow()
        {
            InitializeComponent();
            collection = Cache.LoadAccounts();
            UpdateComboBox();
        }

        private void BtnRefresh_MouseUp1(object sender, MouseButtonEventArgs e)
        {
            if (cbbAccounts.HasItems)
                if (cbbAccounts.SelectedIndex != -1)
                    FillDetails(collection[cbbAccounts.SelectedIndex]);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                collection.Add(Parser.ParseAccount(txtSteamID.Text));

                Cache.SaveAccounts(collection);

                UpdateComboBox();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateComboBox()
        {
            cbbAccounts.Items.Clear();

            foreach(Account acc in collection)
            {
                cbbAccounts.Items.Add(acc._personaName);
            }
        }

        public void FillDetails(Account acc)
        {
            lblNick.Content = acc._personaName;
            lblSteamID1.Content = acc._steamID;
            lblLocID.Content = acc._locCountryCode;
            lblLocation.Content = acc._locCityID;
            lblLastLogoff.Content = acc._lastlogoff;
            lblDateCreated.Content = acc._timeCreated;
            imgAvatar.Source = GetImageFromURL(acc._avatarFull);
        }

        public BitmapImage GetImageFromURL(string url)
        {
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(url, UriKind.Absolute);
                bitmap.EndInit();

                return bitmap;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}