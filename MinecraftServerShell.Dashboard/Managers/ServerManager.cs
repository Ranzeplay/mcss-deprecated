using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Dashboard.Managers
{
    internal class ServerManager
    {
        public static void StartServer()
        {
            var settings = AppSettingsManager.ReadOrCreateSettings();

            InternalInstance.ServerProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = settings.ServerDirectory,
                    Arguments = $"/c {settings.StartupCommand}",
                    // Redirect IO
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true
                },
                EnableRaisingEvents = true
            };

            InternalInstance.ServerProcess.Start();
        }

        public static void SendMessage(string message)
        {
            InternalInstance.ServerProcess.StandardInput.WriteLine(message);
        }
    }
}
