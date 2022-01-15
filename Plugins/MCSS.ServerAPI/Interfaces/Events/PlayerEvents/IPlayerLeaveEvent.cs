using MCSS.ServerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSS.ServerAPI.Interfaces.Events.PlayerEvents
{
    public interface IPlayerLeaveEvent
    {
        void OnPlayerLeft(string playerName, string reason);
    }
}
