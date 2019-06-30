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
using System.Windows.Shapes;

namespace Demo_Stats.Dialogs
{
    /// <summary>
    /// Lógica interna para NewAccountDialog.xaml
    /// </summary>
    public partial class NewAccountDialog : Window
    {
        public NewAccountDialog()
        {
            InitializeComponent();
            txtAnswer.Text = "Enter SteamID64 here...";
            txtAnswer.GotFocus += RemoveText;
            txtAnswer.LostFocus += AddText;
        }       

        public void RemoveText(object sender, EventArgs e)
        {
            if (txtAnswer.Text == "Enter SteamID64 here...")
            {
                txtAnswer.Text = "";
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAnswer.Text))
                txtAnswer.Text = "Enter SteamID64 here...";
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
        }

        public string Answer
        {
            get { return txtAnswer.Text; }
        }
    }
}
