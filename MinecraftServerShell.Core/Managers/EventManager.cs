using MinecraftServerShell.Core.Events.CoreEvents;
using MinecraftServerShell.Core.Events.ServerEvents.Gameplay.Player;
using MinecraftServerShell.Core.Models;
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
        private static readonly string ServerOutputPrefixRegex = "^\\[\\d{2}:\\d{2}:\\d{2}\\] \\[[^\\]]*\\/[^/]*\\]: ";

        private static readonly Regex PlayerLoginRegex = new(ServerOutputPrefixRegex + "([A-Za-z0-9_]{3,16})\\[\\/([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}:[0-9]{1,5})\\] logged in with entity id ([.0-9])+ at \\(([+-]?[0-9]+(?:\\.[0-9]*)?), ([+-]?[0-9]+(?:\\.[0-9]*)?), ([+-]?[0-9]+(?:\\.[0-9]*)?)\\)");
        private static readonly Regex PlayerLostConnectionRegex = new(ServerOutputPrefixRegex + "([A-Za-z0-9_]{3,16}) lost connection: ([A-Za-z0-9_])+");
        private static readonly Regex PlayerChatRegex = new(ServerOutputPrefixRegex + "\\[([A-Za-z0-9_]{3,16})\\] ([\\s\\S]*)");

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
                        PlayerName = match.Groups[1].Value,
                        NetworkAddress = match.Groups[2].Value,
                        EntityId = match.Groups[3].Value,
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

                    new PlayerDisconnectEvent().OnPlayerLeave(new PlayerLeaveEventArgs
                    {
                        PlayerName = match.Groups[1].Value,
                        Reason = match.Groups[2].Value,
                    });
                }
                else if (PlayerChatRegex.IsMatch(data))
                {
                    var match = PlayerChatRegex.Match(data);

                    new PlayerChatEvent().OnPlayerChat(new PlayerChatEventArgs
                    {
                        PlayerName = match.Groups[1].Value,
                        ChatMessage = match.Groups[2].Value,
                    });
                }

                // Anyway, we need to broadcast ServerConsoleOutput
                IssueServerOutputEvent(data);
            }
        }

        private static void IssueServerOutputEvent(string raw)
        {
            var serverLogRegex = new Regex("^\\[\\d{2}:\\d{2}:\\d{2}\\] \\[([^\\]]*)\\/([^/]*)\\]: ([\\s\\S]+)");
            var serverLogMatch = serverLogRegex.Match(raw);

            ServerLog logEntry;
            if (serverLogMatch.Success)
            {
                logEntry = new ServerLog
                {
                    CreateTime = DateTime.Now,
                    Issuer = serverLogMatch.Groups[1].Value,
                    LogLevel = serverLogMatch.Groups[2].Value,
                    Message = serverLogMatch.Groups[3].Value,
                };
            }
            else
            {
                logEntry = new ServerLog
                {
                    CreateTime = DateTime.Now,
                    Issuer = "-",
                    LogLevel = "-",
                    Message = raw,
                };
            }


            // Add it into log list
            InternalInstance.AppLog.ServerLog.Append(logEntry);

            new ServerConsoleOutputEvent().OnServerConsoleOutput(new ServerConsoleOutputEventArgs { LogEntry = logEntry });
        }
    }
}
