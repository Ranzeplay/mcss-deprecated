using MinecraftServerShell.Core.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ConsoleOutputPage.xaml
    /// </summary>
    public partial class ConsoleOutputPage : Page
    {
        public ConsoleOutputPage()
        {
            InitializeComponent();

            SetServerControls(false);
        }

        private void StartServerButton_Click(object sender, RoutedEventArgs e)
        {
            ServerManager.StartServer();

            SetServerControls(true);

            Core.InternalInstance.ServerProcess.BeginOutputReadLine();
            Core.InternalInstance.ServerProcess.OutputDataReceived += (s, e) =>
            {
                Dispatcher.Invoke(() =>
                {
                    ServerOutputTextBlock.Text += $"{e.Data}\n";
                    OutputScrollViewer.ScrollToBottom();
                });
            };

            Core.InternalInstance.ServerProcess.Exited += (s, e) =>
            {
                Dispatcher.Invoke(() =>
                {
                    ServerOutputTextBlock.Text += $"[Process has exited with code {Core.InternalInstance.ServerProcess.ExitCode}]\n";
                    SetServerControls(false);
                });
            };
        }

        private void SendCommandButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessageToServer();
        }

        private void StopServerButton_Click(object sender, RoutedEventArgs e)
        {
            ServerManager.StopServer();
        }

        private void SetServerControls(bool value)
        {
            // Start is the opposite of Stop
            StartServerButton.IsEnabled = !value;
            StopServerButton.IsEnabled = value;

            SendCommandButton.IsEnabled = value;
            CommandTextBox.IsEnabled = value;
        }

        private void CommandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessageToServer();
            }
        }

        private void SendMessageToServer()
        {
            ServerManager.SendConsoleMessage(CommandTextBox.Text);
            ServerOutputTextBlock.Text += $"$> {CommandTextBox.Text}\n";
            CommandTextBox.Clear();
        }
    }
}
