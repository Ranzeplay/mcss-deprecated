using Microsoft.Win32;
using MinecraftServerShell.Core.Managers;
using MinecraftServerShell.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MinecraftServerShell.Dashboard.Pages
{
    /// <summary>
    /// Interaction logic for PluginListPage.xaml
    /// </summary>
    public partial class PluginListPage : Page
    {
        private List<PluginIndexModel> plugins;

        public PluginListPage()
        {
            InitializeComponent();

            plugins = Core.InternalInstance.PluginsEnabled.ConvertAll(p => p.Index);
            PluginListPanel.ItemsSource = plugins;
        }

        private void ImportPluginButton_Click(object sender, RoutedEventArgs e)
        {
            var pluginDirectory = AppSettingsManager.ReadOrCreateSettings().PluginDirectory;

            var dialog = new OpenFileDialog();
            dialog.Filter = "MCSS Plugin|*.dll";
            dialog.Title = "Import plugin";
            dialog.InitialDirectory = pluginDirectory;

            if (dialog.ShowDialog() == true)
            {
                File.Copy(dialog.FileName, System.IO.Path.Combine(pluginDirectory, System.IO.Path.GetFileName(dialog.FileName)));
            }
        }
    }
}
