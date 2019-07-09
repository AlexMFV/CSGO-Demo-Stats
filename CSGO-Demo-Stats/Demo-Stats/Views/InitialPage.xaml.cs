using System;
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
            InitializeComponent();
            main = _main;
            acc_collection = Cache.LoadAccountsBasic();
            settings = Cache.LoadSettings();

            if (!Cache.CheckCSGOPath())
                MessageBox.Show(new SteamPathNotFound().Message, "Not Found!", MessageBoxButton.OK);
            else
            {
                paths = Cache.LoadFolders();
                FillFoldersComboBox();
            }
            FillAccountsComboBox();
            
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
                cbbSelectedAccount.Items.Add("<--- Please create account first --->");
                cbbSelectedAccount.SelectedIndex = 0;
            }
        }

        private void CbbSelectedAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbbSelectedAccount.SelectedIndex >= 0 && acc_collection.Count > 0)
            {
                settings.selectedSteamID = acc_collection[cbbSelectedAccount.SelectedIndex].steamID;
                Cache.SaveAppSettings(settings);
            }
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
                cbbSelectedAccount.Items.Add("<--- Please add a demos path first --->");
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
            lstDemos.Items.Clear();
            foreach (Demo demo in demos)
            {
                lstDemos.Items.Add(demo);
            }
            //lstDemos.DataContext = demos;
            UpdateDemoCount();
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
    }
}
