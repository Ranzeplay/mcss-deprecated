using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Events.ServerEvents.Gameplay.Player
{
    public class PlayerChatEvent
    {
        public static event EventHandler<PlayerChatEventArgs> PlayerChat = null!;

        internal virtual void OnPlayerChat(PlayerChatEventArgs e)
        {
            PlayerChat?.Invoke(this, e);
        }
    }

    public class PlayerChatEventArgs : EventArgs
    {

        public string PlayerName { get; set; } = null!;

        public string ChatMessage { get; set; } = null!;
    }
}
