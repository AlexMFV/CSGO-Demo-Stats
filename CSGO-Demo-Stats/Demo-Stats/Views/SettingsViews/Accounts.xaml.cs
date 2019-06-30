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
    public partial class Accounts : Page
    {
        Settings settings;

        public Accounts(Settings _settings)
        {
            InitializeComponent();
            settings = _settings;
            
            //Since all the collections are stored in the MainWindow, we need to access them by the "refs"
            //To access the collections use settings.main.<collection_name>
        }
    }
}
