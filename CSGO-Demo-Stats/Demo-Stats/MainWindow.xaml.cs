﻿using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;
using Demo_Stats.Properties;

namespace Demo_Stats
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnRefresh_MouseUp1(object sender, MouseButtonEventArgs e)
        {
            string[] accs = { "76561198061445771", "76561198202125464", "76561198039056787", "76561198030622192", "76561198031669452" };
            Accounts collection = new Accounts();

            foreach(string account in accs)
            {
                collection.Add(Parser.ParseAccount(account));
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSteamID.Text.Length > 0)
                btnRefresh.IsEnabled = true;
            else
                btnRefresh.IsEnabled = false;
        }
    }
}