using MinecraftServerShell.Dashboard.Managers;
using MinecraftServerShell.Dashboard.Pages;
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

namespace MinecraftServerShell.Dashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PluginManager.LoadAllPlugins(AppSettingsManager.ReadOrCreateSettings().PluginDirectory);
        }

        private void ShowConsoleOutputButton_Click(object sender, RoutedEventArgs e) => FrameView.Content = InternalInstance.ConsoleOutputPage;

        private void ShowAppSettingsButton_Click(object sender, RoutedEventArgs e) => FrameView.Content = new ApplicationSettingsPage();

        private void ShowServerSettingsButton_Click(object sender, RoutedEventArgs e) => FrameView.Content = new MCServerSettings();

        private void ShowPluginsButton_Click(object sender, RoutedEventArgs e) => FrameView.Content = new PluginListPage();
    }
}
