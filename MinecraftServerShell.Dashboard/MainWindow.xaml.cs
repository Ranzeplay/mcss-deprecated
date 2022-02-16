using MinecraftServerShell.Core.Managers;
using MinecraftServerShell.Dashboard.Pages;
using Ookii.Dialogs.Wpf;
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

            var pluginLoadStatus = PluginManager.LoadAllPlugins(AppSettingsManager.ReadOrCreateSettings().PluginDirectory);

            if (!pluginLoadStatus)
            {
                var dialog = new TaskDialog
                {
                    WindowTitle = Application.Current.MainWindow.Name,
                    MainInstruction = "Failed to load plugins",
                    Content = "Check your settings. Maybe it's bacause of your directory of plugins is not exist?",
                    MainIcon = TaskDialogIcon.Warning
                };
                dialog.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
                dialog.ShowDialog();
            }
        }

        private void ShowConsoleOutputButton_Click(object sender, RoutedEventArgs e) => FrameView.Content = InternalInstance.ConsoleOutputPage;

        private void ShowAppSettingsButton_Click(object sender, RoutedEventArgs e) => FrameView.Content = new ApplicationSettingsPage();

        private void ShowServerSettingsButton_Click(object sender, RoutedEventArgs e) => FrameView.Content = new MCServerSettings();

        private void ShowPluginsButton_Click(object sender, RoutedEventArgs e) => FrameView.Content = new PluginListPage();

        private void ShowLogsButton_Click(object sender, RoutedEventArgs e) => FrameView.Content = InternalInstance.LogPage;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Make these field modifiable
            InternalInstance.StatusBarText.ValueChange += (s, e) =>
            {
                StatusBar.Text = $"{e} ({DateTime.Now.ToShortTimeString()})";
            };

            InternalInstance.AppStatus.ValueChange += (s, e) =>
            {
                Title = $"MinecraftServerShell - {e}";
            };

            InternalInstance.AppStatus.UpdateValue("Idle");
        }
    }
}
