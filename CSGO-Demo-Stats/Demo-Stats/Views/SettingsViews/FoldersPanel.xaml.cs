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
        Folders folders;

        public FoldersPanel(Settings _settings)
        {
            InitializeComponent();
            settings = _settings;
            folders = Cache.LoadFolders();
            UpdateFoldersComboBox();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    folders.Add(dialog.SelectedPath);
                    Cache.SaveFolders(folders);
                    UpdateFoldersComboBox();
                }
            }
        }

        public void UpdateFoldersComboBox()
        {
            lstFolders.Items.Clear();

            foreach (string path in folders)
                lstFolders.Items.Add(path);
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (btnRemove.IsEnabled && lstFolders.SelectedIndex != -1)
            {
                folders.RemoveAt(lstFolders.SelectedIndex);
                Cache.SaveFolders(folders);
                UpdateFoldersComboBox();
            }
        }
    }
}
