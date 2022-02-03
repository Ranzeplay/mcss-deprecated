using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Events.ServerEvents.Gameplay.Player
{
    public class PlayerJoinEvent
    {
        public static event EventHandler<PlayerJoinEventArgs> PlayerJoin = null!;

        internal void OnPlayerJoin(PlayerJoinEventArgs e)
        {
            PlayerJoin?.Invoke(this, e);
        }
    }

    public class PlayerJoinEventArgs
    {
        public string PlayerName { get; set; } = null!;

        public string NetworkAddress { get; set; } = null!;

        public string EntityId { get; set; } = null!;

        public Location Location { get; set; } = null!;
    }
}
