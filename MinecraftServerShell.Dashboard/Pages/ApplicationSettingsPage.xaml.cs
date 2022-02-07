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
using MinecraftServerShell.Core.Managers;
using MinecraftServerShell.Core.Models;
using Ookii.Dialogs.Wpf;

namespace MinecraftServerShell.Dashboard.Pages
{
    /// <summary>
    /// Interaction logic for ApplicationSettingsPage.xaml
    /// </summary>
    public partial class ApplicationSettingsPage : Page
    {
        private readonly AppSettings appSettings;

        public ApplicationSettingsPage()
        {
            InitializeComponent();

            appSettings = AppSettingsManager.ReadOrCreateSettings();
            PluginDirectoryTextBox.Text = appSettings.PluginDirectory;
            ServerDirectoryTextBox.Text = appSettings.ServerDirectory;
            StartupCommandTextBox.Text = appSettings.StartupCommand;
            MaxLoggingEntryTextBox.Text = appSettings.MaxLogLength.ToString();
        }

        private void ServerDirectorySelectionButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            dialog.ShowDialog();

            ServerDirectoryTextBox.Text = dialog.SelectedPath;
            appSettings.ServerDirectory = dialog.SelectedPath;
        }

        private void PluginDirectorySelectionButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            dialog.ShowDialog();

            PluginDirectoryTextBox.Text = dialog.SelectedPath;
            appSettings.PluginDirectory = dialog.SelectedPath;
        }

        private void StartupCommandTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(StartupCommandTextBox.Text))
            {
                appSettings.StartupCommand = StartupCommandTextBox.Text;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (long.TryParse(MaxLoggingEntryTextBox.Text, out _))
            {
                AppSettingsManager.SaveNewSettings(appSettings);

                var dialog = new TaskDialog
                {
                    WindowTitle = Application.Current.MainWindow.Name,
                    MainInstruction = "Success!",
                    Content = "App settings saved successfully!"
                };
                dialog.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
                dialog.ShowDialog();
            }
            else
            {
                var dialog = new TaskDialog
                {
                    WindowTitle = Application.Current.MainWindow.Name,
                    MainInstruction = "Failed!",
                    Content = "Only number is accepted by \"Max logging entries\"",
                    MainIcon = TaskDialogIcon.Error
                };
                dialog.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
                dialog.ShowDialog();
            }
        }
    }
}
