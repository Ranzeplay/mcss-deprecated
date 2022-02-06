using MinecraftServerShell.Core.Events.CoreEvents;
using MinecraftServerShell.Core.Events.ServerEvents.Gameplay;
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

        private static readonly Regex PlayerLoginRegex = new("([A-Za-z0-9_]{3,16})\\[\\/([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}:[0-9]{1,5})\\] logged in with entity id ([.0-9])+ at \\(([+-]?[0-9]+(?:\\.[0-9]*)?), ([+-]?[0-9]+(?:\\.[0-9]*)?), ([+-]?[0-9]+(?:\\.[0-9]*)?)\\)");
        private static readonly Regex PlayerLostConnectionRegex = new("([A-Za-z0-9_]{3,16}) lost connection: ([A-Za-z0-9_]*)");
        private static readonly Regex PlayerChatRegex = new("\\<([A-Za-z0-9_]{3,16})\\> ([\\s\\S]*)");

        private static readonly string MCSSCommandPrefix = "!!";

        internal static void SetupEvents()
        {
            InternalInstance.ServerProcess.OutputDataReceived += ServerProcess_OutputDataReceived;

            SetupPlayerEvents();
        }

        private static void SetupPlayerEvents()
        {
            ServerConsoleOutputEvent.ServerConsoleOutput += ServerConsoleOutputEvent_PlayerJoin;
            ServerConsoleOutputEvent.ServerConsoleOutput += ServerConsoleOutputEvent_PlayerDisconnect;
            ServerConsoleOutputEvent.ServerConsoleOutput += ServerConsoleOutputEvent_PlayerChat;
            PlayerChatEvent.PlayerChat += PlayerChatEvent_CommandExecution;
        }

        internal static void ClearEvents()
        {
            InternalInstance.ServerProcess.OutputDataReceived -= ServerProcess_OutputDataReceived;
        }

        private static void ClearPlayerEvents()
        {
            ServerConsoleOutputEvent.ServerConsoleOutput -= ServerConsoleOutputEvent_PlayerJoin;
            ServerConsoleOutputEvent.ServerConsoleOutput -= ServerConsoleOutputEvent_PlayerDisconnect;
            ServerConsoleOutputEvent.ServerConsoleOutput -= ServerConsoleOutputEvent_PlayerChat;
            PlayerChatEvent.PlayerChat -= PlayerChatEvent_CommandExecution;
        }

        private static void ServerConsoleOutputEvent_PlayerChat(object? sender, ServerConsoleOutputEventArgs e)
        {
            if (PlayerChatRegex.IsMatch(e.LogEntry.Message))
            {
                var match = PlayerChatRegex.Match(e.LogEntry.Message);

                new PlayerChatEvent().OnPlayerChat(new PlayerChatEventArgs
                {
                    PlayerName = match.Groups[1].Value,
                    ChatMessage = match.Groups[2].Value,
                });
            }
        }

        private static void ServerConsoleOutputEvent_PlayerDisconnect(object? sender, ServerConsoleOutputEventArgs e)
        {
            if (PlayerLostConnectionRegex.IsMatch(e.LogEntry.Message))
            {
                var match = PlayerLostConnectionRegex.Match(e.LogEntry.Message);

                new PlayerDisconnectEvent().OnPlayerLeave(new PlayerLeaveEventArgs
                {
                    PlayerName = match.Groups[1].Value,
                    Reason = match.Groups[2].Value,
                });
            }
        }

        private static void ServerConsoleOutputEvent_PlayerJoin(object? sender, ServerConsoleOutputEventArgs e)
        {
            if (PlayerLoginRegex.IsMatch(e.LogEntry.Message))
            {
                var match = PlayerLoginRegex.Match(e.LogEntry.Message);

                new PlayerJoinEvent().OnPlayerJoin(new PlayerJoinEventArgs
                {
                    PlayerName = match.Groups[1].Value,
                    NetworkAddress = match.Groups[2].Value,
                    EntityId = match.Groups[3].Value,
                    Location = new(double.Parse(match.Groups[3].Value), double.Parse(match.Groups[4].Value), double.Parse(match.Groups[5].Value), string.Empty)
                });
            }
        }

        private static void PlayerChatEvent_CommandExecution(object? sender, PlayerChatEventArgs e)
        {
            if (e.ChatMessage.StartsWith(MCSSCommandPrefix))
            {
                // Remove command prefix
                var commandParts = e.ChatMessage[MCSSCommandPrefix.Length..].Split(' ');

                new CommandExecuteEvent().OnCommandExecution(new()
                {
                    CommandName = commandParts[0],
                    CommandArgs = commandParts[1..],
                    Issuer = e.PlayerName,
                    RawData = e.ChatMessage
                });
            }
        }

        private static void ServerProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                var serverLogRegex = new Regex("^\\[\\d{2}:\\d{2}:\\d{2}\\] \\[([^\\]]*)\\/([^/]*)\\]: ([\\s\\S]+)");
                var serverLogMatch = serverLogRegex.Match(e.Data);

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
                        Message = e.Data,
                    };
                }

                if (logEntry != null)
                {
                    // Add it into log list
                    InternalInstance.AppLog.ServerLog.Append(logEntry);

                    new ServerConsoleOutputEvent().OnServerConsoleOutput(new ServerConsoleOutputEventArgs { LogEntry = logEntry });
                }
            }
        }
    }
}
