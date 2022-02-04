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
        private readonly DataTable PluginLogDataTable = new();

        public LogPage()
        {
            InitializeComponent();

            #region InitServerLog
            ServerLogDataTable.Columns.Add("Time");
            ServerLogDataTable.Columns.Add("Issuer");
            ServerLogDataTable.Columns.Add("Level");
            ServerLogDataTable.Columns.Add("Message");

            Core.InternalInstance.AppLog.ServerLog.NewLog += (s, e) =>
            {
                var log = e;
                // Display latest log to table
                Dispatcher.Invoke(() =>
                {
                    ServerLogDataTable.Rows.Add(log.CreateTime.ToString("G"), log.Issuer, log.LogLevel, log.Message);
                    ServerLogTable.UpdateLayout();
                    ServerLogTable.ScrollIntoView(ServerLogTable.Items[^1]);
                });
            };

            ServerLogTable.ItemsSource = ServerLogDataTable.DefaultView;
            #endregion

            #region InitPluginLog
            PluginLogDataTable.Columns.Add("Time");
            PluginLogDataTable.Columns.Add("Plugin");
            PluginLogDataTable.Columns.Add("Level");
            PluginLogDataTable.Columns.Add("Message");

            Core.InternalInstance.AppLog.PluginLog.NewLog += (s, e) =>
            {
                var log = e;
                Dispatcher.Invoke(() =>
                {
                    PluginLogDataTable.Rows.Add(log.CreateTime.ToString("G"), log.Level.ToString("d"), log.PluginName, log.Message);
                    PluginLogTable.UpdateLayout();
                    PluginLogTable.ScrollIntoView(PluginLogTable.Items[^1]);
                });
            };

            PluginLogTable.ItemsSource = PluginLogDataTable.DefaultView;
            #endregion
        }
    }
}
