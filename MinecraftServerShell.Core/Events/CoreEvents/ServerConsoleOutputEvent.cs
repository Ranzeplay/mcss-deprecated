using MinecraftServerShell.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Events.CoreEvents
{
    public class ServerConsoleOutputEvent
    {
        public static event EventHandler<ServerConsoleOutputEventArgs> ServerConsoleOutput = null!;

        internal virtual void OnServerConsoleOutput(ServerConsoleOutputEventArgs e)
        {
            ServerConsoleOutput?.Invoke(this, e);
        }
    }

    public class ServerConsoleOutputEventArgs : EventArgs
    {
        public ServerLog LogEntry { get; set; } = null!;
    }
}
