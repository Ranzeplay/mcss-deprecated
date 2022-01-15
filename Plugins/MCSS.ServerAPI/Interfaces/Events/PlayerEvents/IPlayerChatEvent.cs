using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSS.ServerAPI.Interfaces.Events.PlayerEvents
{
    public interface IPlayerChatEvent
    {
        void OnPlayerChat(string playerName, string message);
    }
}
