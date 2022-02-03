using MinecraftServerShell.Core.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MinecraftServerShell.Dashboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            if (!Core.InternalInstance.ServerProcess.HasExited)
            {
                ServerManager.StopServer();
            }

            PluginManager.UnloadAllPlugins(AppSettingsManager.ReadOrCreateSettings().PluginDirectory);

            base.OnExit(e);
        }
    }
}
