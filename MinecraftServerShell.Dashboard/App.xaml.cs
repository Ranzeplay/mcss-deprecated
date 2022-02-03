using MinecraftServerShell.Core;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            Core.InternalInstance.AppLog = new ApplicationLog(AppSettingsManager.ReadOrCreateSettings().MaxLogLength);

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                if (!Core.InternalInstance.ServerProcess.HasExited)
                {
                    ServerManager.StopServer();
                }
            }
            catch { }


            PluginManager.UnloadAllPlugins(AppSettingsManager.ReadOrCreateSettings().PluginDirectory);

            base.OnExit(e);
        }
    }
}
