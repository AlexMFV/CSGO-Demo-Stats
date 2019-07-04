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
    /// Interação lógica para Settings.xam
    /// </summary>
    public partial class Settings : Page
    {
        public MainWindow main;
        int _selectedPanel = -1;

        public Settings(MainWindow _main)
        {
            InitializeComponent();
            main = _main;
            ChangePanel((int)SettingsPanels.Account);
        }

        #region ColorManagers

        #region Panel Change

        public void ChangePanel(int panel)
        {
            switch (panel)
            {
                case 0: this.frame.Content = new SettingsViews.AccountsPanel(this); ChangeHighlight(panel); break;
                case 1: this.frame.Content = new SettingsViews.FoldersPanel(this); ChangeHighlight(panel); break;
            }
        }

        public void ChangeHighlight(int panel)
        {
            switch (_selectedPanel)
            {
                case 0:
                    txtAccounts.Foreground = GetDefaultColor();
                    txtAccounts.BorderThickness = new Thickness(0); break;

                case 1:
                    txtFolders.Foreground = GetDefaultColor();
                    txtFolders.BorderThickness = new Thickness(0); break;
            }

            switch (panel)
            {
                case 0:
                    txtAccounts.Foreground = GetHighlightColor(); _selectedPanel = 0;
                    txtAccounts.BorderThickness = new Thickness(0, 0, 2, 0);
                    txtAccounts.BorderBrush = GetHighlightColor(); break;

                case 1:
                    txtFolders.Foreground = GetHighlightColor(); _selectedPanel = 1;
                    txtFolders.BorderThickness = new Thickness(0, 0, 2, 0);
                    txtFolders.BorderBrush = GetHighlightColor(); break;
            }
        }

        #endregion

        #region Accounts

        private void TxtAccounts_MouseEnter(object sender, MouseEventArgs e)
        {
            txtAccounts.Foreground = GetHoverColor();
            txtAccounts.BorderBrush = GetHoverColor();
            txtAccounts.BorderThickness = new Thickness(0, 0, 2, 0);
        }

        private void TxtAccounts_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_selectedPanel != (int)SettingsPanels.Account)
            {
                txtAccounts.Foreground = GetDefaultColor();
                txtAccounts.BorderThickness = new Thickness(0);
            }
            else
            {
                txtAccounts.Foreground = GetHighlightColor();
                txtAccounts.BorderBrush = GetHighlightColor();
                txtAccounts.BorderThickness = new Thickness(0, 0, 2, 0);
            }
        }

        //Fires when the user presses the Label
        private void TxtAccounts_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_selectedPanel != (int)SettingsPanels.Account)
                ChangePanel((int)SettingsPanels.Account);
        }

        #endregion

        #region Folders

        private void TxtFolders_MouseEnter(object sender, MouseEventArgs e)
        {
            txtFolders.Foreground = GetHoverColor();
            txtFolders.BorderBrush = GetHoverColor();
            txtFolders.BorderThickness = new Thickness(0, 0, 2, 0);
        }

        private void TxtFolders_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_selectedPanel != (int)SettingsPanels.Folders)
            {
                txtFolders.Foreground = GetDefaultColor();
                txtFolders.BorderThickness = new Thickness(0);
            }
            else
            {
                txtFolders.Foreground = GetHighlightColor();
                txtFolders.BorderBrush = GetHighlightColor();
                txtFolders.BorderThickness = new Thickness(0, 0, 2, 0);
            }
        }

        private void TxtFolders_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_selectedPanel != (int)SettingsPanels.Folders)
                ChangePanel((int)SettingsPanels.Folders);
        }

        #endregion

        #region Other Buttons

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new InitialPage(main);
        }

        #endregion

        #region Get Colors

        public SolidColorBrush GetHighlightColor()
        {
            return (SolidColorBrush)this.FindResource("HighlightColor");
        }

        public SolidColorBrush GetMainColor()
        {
            return (SolidColorBrush)this.FindResource("MainColor");
        }

        public SolidColorBrush GetHoverColor()
        {
            return (SolidColorBrush)this.FindResource("HoverColor");
        }

        public SolidColorBrush GetDefaultColor()
        {
            return (SolidColorBrush)this.FindResource("DefaultColor");
        }

        #endregion

        #endregion
    }
}
