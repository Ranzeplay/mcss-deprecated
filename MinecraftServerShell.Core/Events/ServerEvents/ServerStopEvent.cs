using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Events.ServerEvents
{
    public class ServerStopEvent
    {
        public event EventHandler<ServerStopEventArgs> ServerStop = null!;

        protected virtual void OnServerStop(ServerStopEventArgs e)
        {
            ServerStop?.Invoke(this, e);
        }
    }

    public class ServerStopEventArgs : EventArgs
    {
        public bool IsForceStop { get; set; }
    }
}
