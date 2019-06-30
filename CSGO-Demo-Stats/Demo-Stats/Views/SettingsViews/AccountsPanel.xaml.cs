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

namespace Demo_Stats.Views.SettingsViews
{
    /// <summary>
    /// Interação lógica para Accounts.xam
    /// </summary>
    public partial class AccountsPanel : Page
    {
        Settings settings;
        Accounts acc_collection;

        public AccountsPanel(Settings _settings)
        {
            InitializeComponent();
            settings = _settings;
            FillListBox();
        }

        private void FillListBox()
        {
            acc_collection = Cache.LoadAccountsBasic();

            foreach (Account acc in acc_collection)
                lstAccounts.Items.Add(new AccountListItem(acc.personaName, acc.steamID));
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshListBox();
        }

        private void RefreshListBox()
        {
            lstAccounts.Items.Clear();
            FillListBox();
            lstAccounts.InvalidateArrange();
            lstAccounts.UpdateLayout();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string SteamID = null;

            //Open DialogBox
            Dialogs.NewAccountDialog prompt = new Dialogs.NewAccountDialog();
            if (prompt.ShowDialog() == true)
            {
                SteamID = prompt.Answer;

                //Parse returned ID / Add to collection
                acc_collection.Add(Parser.ParseAccountBasic(SteamID));

                //Save Collection
                Cache.SaveAccounts(acc_collection);

                //Reload ListBox
                RefreshListBox();
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (btnRemove.IsEnabled && lstAccounts.SelectedIndex != -1)
            {
                acc_collection.RemoveAt(lstAccounts.SelectedIndex);
                Cache.SaveAccounts(acc_collection);
                RefreshListBox();
            }
        }
    }
}
