using MinecraftServerShell.Core;
using MinecraftServerShell.Core.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
			var appSettings = AppSettingsManager.ReadOrCreateSettings();

            Core.InternalInstance.AppLog = new ApplicationLog(appSettings.MaxLogLength);

            // Create directories if not exist
            Directory.CreateDirectory(appSettings.PluginDirectory);
			Directory.CreateDirectory(appSettings.ServerDirectory);

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
