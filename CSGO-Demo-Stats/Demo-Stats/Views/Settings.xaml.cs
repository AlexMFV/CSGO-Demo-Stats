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

        public Settings(MainWindow _main)
        {
            InitializeComponent();
            main = _main;
            this.frame.Content = new SettingsViews.AccountsPanel(this);
        }

        private void TxtAccounts_MouseEnter(object sender, MouseEventArgs e)
        {
            txtAccounts.Foreground = (SolidColorBrush)this.FindResource("HoverColor");
        }

        private void TxtAccounts_MouseLeave(object sender, MouseEventArgs e)
        {
            txtAccounts.Foreground = (SolidColorBrush)this.FindResource("DefaultColor");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new InitialPage(main);
        }
    }
}
