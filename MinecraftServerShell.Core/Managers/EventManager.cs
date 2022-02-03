using MinecraftServerShell.Core.Events.ServerEvents.Gameplay.Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Managers
{
    public class EventManager
    {
        private static readonly Regex PlayerLoginRegex = new("^\\[\\d{2}:\\d{2}:\\d{2}\\] \\[[^\\]]*\\/[^/]*\\]: ([A-Za-z0-9_]{3,16})\\[\\/([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}:[0-9]{1,5})\\] logged in with entity id ([.0-9])+ at \\(([+-]?[0-9]+(?:\\.[0-9]*)?), ([+-]?[0-9]+(?:\\.[0-9]*)?), ([+-]?[0-9]+(?:\\.[0-9]*)?)\\)");
        private static readonly Regex PlayerLostConnectionRegex = new("^\\[\\d{2}:\\d{2}:\\d{2}\\] \\[[^\\]]*\\/[^/]*\\]: ([A-Za-z0-9_]{3,16}) lost connection: ([A-Za-z0-9_])+");
        private static readonly Regex PlayerChatRegex = new("^\\[\\d{2}:\\d{2}:\\d{2}\\] \\[[^\\]]*\\/[^/]*\\]: \\[([A-Za-z0-9_]{3,16})\\] ([\\s\\S]*)");

        public static void SetupPlayerEvents()
        {
            InternalInstance.ServerProcess.OutputDataReceived += ServerProcess_OutputDataReceived;
        }

        private static void ServerProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            var data = e.Data;

            if (data != null)
            {
                if (PlayerLoginRegex.IsMatch(data))
                {
                    var match = PlayerLoginRegex.Match(data);

                    new PlayerJoinEvent().OnPlayerJoin(new PlayerJoinEventArgs
                    {
                        PlayerName = match.Groups[0].Value,
                        NetworkAddress = match.Groups[1].Value,
                        EntityId = match.Groups[2].Value,
                        Location = new()
                        {
                            X = double.Parse(match.Groups[3].Value),
                            Y = double.Parse(match.Groups[4].Value),
                            Z = double.Parse(match.Groups[5].Value)
                        }
                    });
                }
                else if (PlayerLostConnectionRegex.IsMatch(data))
                {
                    var match = PlayerLostConnectionRegex.Match(data);

                    new PlayerLeaveEvent().OnPlayerLeave(new PlayerLeaveEventArgs
                    {
                        PlayerName = match.Groups[0].Value,
                        Reason = match.Groups[1].Value,
                    });
                }
                else if (PlayerChatRegex.IsMatch(data))
                {
                    var match = PlayerChatRegex.Match(data);

                    new PlayerChatEvent().OnPlayerChat(new PlayerChatEventArgs
                    {
                        PlayerName = match.Groups[0].Value,
                        ChatMessage = match.Groups[1].Value,
                    });
                }
            }
        }
    }
}
