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

        public InitialPage(MainWindow _main)
        {
            InitializeComponent();
            main = _main;
        }

        private void BtnOpenSettings_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new Views.Settings(main);
        }
    }
}
