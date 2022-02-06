using MinecraftServerShell.Core.Events.CoreEvents;
using MinecraftServerShell.Core.Events.ServerEvents;
using MinecraftServerShell.Core.Events.ServerEvents.Gameplay.Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Managers
{
    public class ServerManager
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

            new ServerStartEvent().OnServerStart(new ServerStartEventArgs());

            InternalInstance.ServerProcess.Start();
            InternalInstance.ServerProcess.Exited += ServerProcess_Exited;

            EventManager.SetupPlayerEvents();
        }

        public static void SendMessageAsync(string message)
        {
            InternalInstance.ServerProcess.StandardInput.WriteLine(message);
        }

        public static void StopServer()
        {
            if (!InternalInstance.ServerProcess.HasExited)
            {
                SendMessageAsync("stop");
                new ServerBeginStopEvent().OnServerBeginStop(new ServerStopEventArgs());

            }
        }

        private static void ServerProcess_Exited(object? sender, EventArgs e)
        {
            new ServerStoppedEvent().OnServerStopped(new ServerStopEventArgs());
        }
    }
}
