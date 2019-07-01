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

        public InitialPage(MainWindow _main)
        {
            InitializeComponent();
            main = _main;
            acc_collection = Cache.LoadAccountsBasic();
            settings = Cache.LoadSettings();
            FillAccountsComboBox();
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
    }
}
