using MinecraftServerShell.Core.Events.ServerEvents;
using MinecraftServerShell.Core.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

            ServerStartEvent.ServerStart += ServerStartEvent_ServerStart;
        }

        private async void ServerStartEvent_ServerStart(object? sender, ServerStartEventArgs e)
        {
            InternalInstance.AppStatus.UpdateValue("Server Running");

            await Task.Run(async () =>
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    SetServerControls(true);
                });

                Thread.Sleep(1000);

                Core.InternalInstance.ServerProcess.BeginOutputReadLine();
                Core.InternalInstance.ServerProcess.OutputDataReceived += async (s, e) =>
                {
                    await Dispatcher.InvokeAsync(() =>
                    {
                        ServerOutputTextBlock.Text += $"{e.Data}\n";
                        OutputScrollViewer.ScrollToBottom();
                    });
                };

                Core.InternalInstance.ServerProcess.Exited += async (s, e) =>
                {
                    await Dispatcher.InvokeAsync(() =>
                    {
                        ServerOutputTextBlock.Text += $"[Process has exited with code {Core.InternalInstance.ServerProcess.ExitCode}]\n";
                        SetServerControls(false);
                        InternalInstance.AppStatus.UpdateValue("Idle");
                    });
                };
            });
        }

        private void StartServerButton_Click(object sender, RoutedEventArgs e)
        {
            ServerManager.StartServer();
            InternalInstance.StatusBarText.UpdateValue("Server starting");
        }

        private void SendCommandButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessageToServer();
        }

        private void StopServerButton_Click(object sender, RoutedEventArgs e)
        {
            ServerManager.StopServer();
            InternalInstance.StatusBarText.UpdateValue("Stopping server");
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

        private void ClearOutputButton_Click(object sender, RoutedEventArgs e)
        {
            ServerOutputTextBlock.Text = string.Empty;
            Dispatcher.Invoke(() =>
            {
                InternalInstance.StatusBarText.UpdateValue("Output cleared");
            });
        }

        private void KillServerButton_Click(object sender, RoutedEventArgs e)
        {
            Core.InternalInstance.ServerProcess.Kill();

            Dispatcher.Invoke(() =>
            {
                ServerOutputTextBlock.Text += $"[Killing server process]\n";
                InternalInstance.StatusBarText.UpdateValue("Killing server");
            });
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InternalInstance.StatusBarText.UpdateValue("Ready");
        }
    }
}
