using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Events.ServerEvents
{
    public class ServerStartEvent
    {
        public static event EventHandler<ServerStartEventArgs> ServerStart = null!;

        internal virtual void OnServerStart(ServerStartEventArgs e)
        {
            ServerStart?.Invoke(this, e);
        }
    }

    public class ServerStartEventArgs : EventArgs
    {
        public bool IsRestart { get; set; }
    }
}
