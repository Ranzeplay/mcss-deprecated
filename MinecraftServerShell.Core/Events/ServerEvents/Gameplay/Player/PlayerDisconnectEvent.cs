using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Events.ServerEvents.Gameplay.Player
{
    public class PlayerDisconnectEvent
    {
        public static event EventHandler<PlayerLeaveEventArgs> PlayerLeave = null!;

        internal void OnPlayerLeave(PlayerLeaveEventArgs e)
        {
            PlayerLeave?.Invoke(this, e);
        }
    }

    public class PlayerLeaveEventArgs
    {
        public string PlayerName { get; set; } = null!;

        public string Reason { get; set; } = null!;
    }
}
