using System.Windows.Media;
using MahApps.Metro.Controls;

namespace Demo_Stats
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Accounts accounts_collection = new Accounts();

        public MainWindow()
        {
            InitializeComponent();
            this.TitleForeground = new SolidColorBrush(Colors.Black);
            this.MainFrame.Content = new Views.InitialPage(this);
        }
    }
}