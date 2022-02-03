using MinecraftServerShell.Core.Events.CoreEvents;
using MinecraftServerShell.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;

namespace MinecraftServerShell.Dashboard.Pages
{
    /// <summary>
    /// Interaction logic for LogPage.xaml
    /// </summary>
    public partial class LogPage : Page
    {
        private readonly DataTable ServerLogDataTable = new();

        public LogPage()
        {
            InitializeComponent();

            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            timer.Tick += (s, e) => { ServerLogTable.Items.Refresh(); };
            timer.Start();

            // Initialize server log
            ServerLogDataTable.Columns.Add("Time");
            ServerLogDataTable.Columns.Add("Level");
            ServerLogDataTable.Columns.Add("Issuer");
            ServerLogDataTable.Columns.Add("Message");

            ServerConsoleOutputEvent.ServerConsoleOutput += (s, e) =>
            {
                var log = e.LogEntry;
                Dispatcher.Invoke(() =>
                {
                    ServerLogDataTable.Rows.Add(log.CreateTime.ToString("G"), log.LogLevel, log.Issuer, log.Message);
                    ServerLogTable.UpdateLayout();
                    ServerLogTable.ScrollIntoView(ServerLogTable.Items[ServerLogTable.Items.Count - 1]);
                });
            };

            ServerLogTable.ItemsSource = ServerLogDataTable.DefaultView;
        }
    }
}
