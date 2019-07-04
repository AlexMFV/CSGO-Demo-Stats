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
    /// Interação lógica para FoldersPanel.xam
    /// </summary>
    public partial class FoldersPanel : Page
    {
        Settings settings;

        public FoldersPanel(Settings _settings)
        {
            InitializeComponent();
            settings = _settings;
            this.lstFolders.Items.Add("C://steamid//sdasdk/askjdasds/1");
            this.lstFolders.Items.Add("C://steamid//sdasdk/askjdasds/2");
            this.lstFolders.Items.Add("C://steamid//sdasdk/askjdasds/3");
            this.lstFolders.Items.Add("C://steamid//sdasdk/askjdasds/4");
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
