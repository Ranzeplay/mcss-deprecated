using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Events.ServerEvents.Gameplay.Player
{
    public class PlayerJoinEvent
    {
        public event EventHandler<PlayerJoinEventArgs> PlayerJoin;

        protected void OnPlayerJoin(PlayerJoinEventArgs e)
        {
            PlayerJoin?.Invoke(this, e);
        }
    }

    public class PlayerJoinEventArgs
    {
        public string PlayerName { get; set; }

        public string NetworkAddress { get; set; }

        public string EntityId { get; set; }

        public Location Location { get; set; }
    }
}
