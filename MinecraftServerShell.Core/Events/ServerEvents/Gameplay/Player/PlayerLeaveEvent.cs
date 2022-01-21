using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Events.ServerEvents.Gameplay.Player
{
    public class PlayerLeaveEvent
    {
        public event EventHandler<PlayerLeaveEventArgs> PlayerLeave;

        protected void OnPlayerJoin(PlayerLeaveEventArgs e)
        {
            PlayerLeave?.Invoke(this, e);
        }
    }

    public class PlayerLeaveEventArgs
    {
        public string PlayerName { get; set; }

        public string Reason { get; set; }
    }
}
