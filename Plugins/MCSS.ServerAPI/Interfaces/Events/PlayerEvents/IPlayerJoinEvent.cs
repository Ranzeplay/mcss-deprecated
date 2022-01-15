using MCSS.ServerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSS.ServerAPI.Interfaces.Events.PlayerEvents
{
    public interface IPlayerJoinEvent
    {
        void OnPlayerJoin(string playerName, string networkAddress, string entityId, Location location);
    }
}
