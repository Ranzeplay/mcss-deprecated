using MinecraftServerShell.Core.Events.CoreEvents;
using MinecraftServerShell.Core.Models.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Managers.Gameplay
{
    public class PlayerManager
    {
        private static readonly Dictionary<Guid, MinecraftPlayer> PendingPlayerData = new();

        private static readonly Regex EntityDataRegex = new("([A-Za-z0-9_]{3,16}) has the following entity data: ([\\s\\S]+)");

        private static readonly string DoubleDataRegexString = new("([+-]?[0-9]*[\\.,]?[0-9]+)?d");

        private static readonly Regex LocationDataRegex = new($"^\\[({DoubleDataRegexString}, {DoubleDataRegexString}, {DoubleDataRegexString})\\]");

        public static async Task<MinecraftPlayer> GetPlayerAsync(string playerName)
        {
            var res = await Task.Run(async () =>
            {
                // Create player data pull task
                var taskId = Guid.NewGuid();
                var handler = new EventHandler<ServerConsoleOutputEventArgs>((object? s, ServerConsoleOutputEventArgs e) =>
                {
                    if (EntityDataRegex.IsMatch(e.LogEntry.Message))
                    {
                        // If this matches the Pos data we want
                        var entityData = EntityDataRegex.Match(e.LogEntry.Message).Groups[2].Value;

                        // We also need to compare player name with it
                        if (LocationDataRegex.IsMatch(entityData) && EntityDataRegex.Match(e.LogEntry.Message).Groups[1].Value == playerName)
                        {
                            var loc = LocationDataRegex.Match(entityData);

                            PendingPlayerData[taskId] = new()
                            {
                                Name = playerName,
                                Location = new()
                                {
                                    X = double.Parse(loc.Groups[2].Value),
                                    Y = double.Parse(loc.Groups[3].Value),
                                    Z = double.Parse(loc.Groups[4].Value)
                                }
                            };
                        }
                    }
                });

                ServerConsoleOutputEvent.ServerConsoleOutput += handler;

                // Get location data
                await ServerManager.SendMessageAsync($"data get entity {playerName} Pos");

                while (!PendingPlayerData.ContainsKey(taskId)) { }

                // Unsubscribe event (this is a one-time event)
                ServerConsoleOutputEvent.ServerConsoleOutput -= handler;

                var result = PendingPlayerData[taskId];

                // Remove it, this data is a snapshot
                PendingPlayerData.Remove(taskId);
                return result;
            });

            return res;
        }
    }
}
