﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Demo_Stats.Views
{
    /// <summary>
    /// Interação lógica para InitialPage.xam
    /// </summary>
    public partial class InitialPage : Page
    {
        MainWindow main;
        Accounts acc_collection;
        AppSettings settings;
        Folders paths;
        Demos demos;

        public InitialPage(MainWindow _main)
        {
            main = _main;
            acc_collection = Cache.LoadAccountsBasic();
            settings = Cache.LoadSettings();
            InitializeComponent();

            if (!Cache.CheckCSGOPath())
                MessageBox.Show(new SteamPathNotFound().Message, "Not Found!", MessageBoxButton.OK);
            else
            {
                paths = Cache.LoadFolders();
                FillFoldersComboBox();
            }
            FillAccountsComboBox();

            //Sort the DataGrid
            SetInitialColumnSort();

            //Load the demos from Cache first
        }

        private void BtnOpenSettings_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new Views.Settings(main);
        }

        public void FillAccountsComboBox()
        {
            cbbSelectedAccount.Items.Clear();

            if (acc_collection.Count > 0)
            {
                foreach (Account acc in acc_collection)
                    cbbSelectedAccount.Items.Add(acc.personaName);

                cbbSelectedAccount.SelectedIndex = AppDataMethods.GetIndexFromSteamID(settings.selectedSteamID, acc_collection);
            }
            else
            {
                cbbSelectedAccount.Items.Add("<--- Add an account first --->");
                cbbSelectedAccount.SelectedIndex = 0;
            }
        }

        private void CbbSelectedAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbbSelectedAccount.SelectedIndex >= 0 && acc_collection.Count > 0)
            {
                columnStatus.Visibility = Visibility.Visible;
                settings.selectedSteamID = acc_collection[cbbSelectedAccount.SelectedIndex].steamID;
                Cache.SaveAppSettings(settings);
            }
            else
                columnStatus.Visibility = Visibility.Hidden;
        }

        public void FillFoldersComboBox()
        {
            cbbSelectedAccount.Items.Clear();

            if (paths.Count > 0)
            {
                foreach (string path in paths)
                    cbbFolders.Items.Add(path);

                if (!settings.isShowAllActive)
                    cbbFolders.SelectedIndex = AppDataMethods.GetIndexFromSteamPath(settings.selectedSteamPath, paths);
                else
                    chkAllFolders.IsChecked = true;
            }
            else
            {
                cbbSelectedAccount.Items.Add("<--- Please add a \"demos\" path first --->");
                cbbSelectedAccount.SelectedIndex = 0;
            }
        }

        private void CbbFolders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbFolders.SelectedIndex >= 0 && paths.Count > 0)
            {
                settings.selectedSteamPath = paths[cbbFolders.SelectedIndex];
                Cache.SaveAppSettings(settings);
                demos = Cache.LoadDemos();
                demos = DemoSearch.SearchNewDemos(cbbFolders.SelectedIndex, paths, demos);
                FillDemoList();
            }
        }

        public void UpdateDemoCount()
        {
            lblDemoCount.Content = lstDemos.Items.Count + " Demo(s)";
        }

        public void FillDemoList()
        {
            lstDemos.DataContext = demos;
        }

        public void ShowAllDemos()
        {
            demos = new Demos();
            demos = DemoSearch.SearchNewDemos(paths, demos);
            FillDemoList();
        }

        private void ChkAllFolders_Checked(object sender, RoutedEventArgs e)
        {
            cbbFolders.IsEnabled = false;
            cbbFolders.SelectedIndex = -1;
            settings.isShowAllActive = true;
            Cache.SaveAppSettings(settings);
            ShowAllDemos();
        }

        private void ChkAllFolders_Unchecked(object sender, RoutedEventArgs e)
        {
            cbbFolders.IsEnabled = true;
            settings.isShowAllActive = false;
            Cache.SaveAppSettings(settings);
            cbbFolders.SelectedIndex = AppDataMethods.GetIndexFromSteamPath(settings.selectedSteamPath, paths);
        }

        private void BtnShowAll_Click(object sender, RoutedEventArgs e)
        {
            ShowAllDemos();
        }

        private void Unfocus(object sender, EventArgs e)
        {
            lstDemos.Focus();
        }

        private void SetInitialColumnSort()
        {
            DataGridColumn column = lstDemos.Columns[1]; //lstDemos.Columns[settings.selectedColumn];
            column.SortDirection = ListSortDirection.Descending; //if(1) Ascending else Descending
            lstDemos.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, (ListSortDirection)column.SortDirection));
        }

        private void LstDemos_LayoutUpdated(object sender, EventArgs e)
        {
            UpdateDemoCount();
        }

        private void LstDemos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstDemos.SelectedItems.Count > 0)
                main.Content = new DemoPage(main, (Demo)lstDemos.SelectedItem, settings.selectedSteamPath);
        }

        //private void BtnTestValues_Click(object sender, RoutedEventArgs e) //Async method
        //{
        //    //await RunAddAsync();
        //}

        //private async Task RunAddAsync()
        //{
        //    await Task.Run(() => RefreshListBox());
        //}

        //private void RefreshListBox()
        //{
        //    //Dispatcher.Invoke(() =>
        //    //{
        //        while (demos[0].demo_ticks < 10000000)
        //        {
        //            demos[0].demo_ticks++;
        //        Application.Current.Dispatcher.Invoke(() =>
        //        {
        //            lstDemos.Items.Refresh();
        //        });
        //            //lstDemos.Items.Refresh();
        //        }
        //    //});
        //}
    }
}
