using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Events.ServerEvents
{
    public class ServerBeginStopEvent
    {
        public static event EventHandler<ServerStopEventArgs> ServerBeginStop = null!;

        internal virtual void OnServerBeginStop(ServerStopEventArgs e)
        {
            ServerBeginStop?.Invoke(this, e);
        }
    }

    public class ServerStopEventArgs : EventArgs
    {
        public bool IsForceStop { get; set; }
    }

    public class ServerStoppedEvent
    {
        public static event EventHandler<ServerStopEventArgs> ServerStopped = null!;

        internal virtual void OnServerStopped(ServerStopEventArgs e)
        {
            ServerStopped?.Invoke(this, e);
        }
    }
}
