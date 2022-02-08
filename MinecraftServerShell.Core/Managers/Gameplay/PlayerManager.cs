using MinecraftServerShell.Core.Events.CoreEvents;
using MinecraftServerShell.Core.Models;
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
        private static readonly Regex EntityDataRegex = new("([A-Za-z0-9_]{3,16}) has the following entity data: ([\\s\\S]+)");

        private static readonly string DoubleDataRegexString = new("([+-]?[0-9]*[\\.,]?[0-9]+)?d");
        private static readonly Regex FloatDataRegex = new("([+-]?[0-9]*[\\.,]?[0-9]+)f");

        private static readonly Regex CoordinateDataRegex = new($"^\\[({DoubleDataRegexString}, {DoubleDataRegexString}, {DoubleDataRegexString})\\]");

        public static async Task<MinecraftPlayer> GetPlayerAsync(string playerName) => new MinecraftPlayer
        {
            Name = playerName,
            Location = new Location(await GetPlayerCoordinateAsync(playerName),
                                    await GetPlayerDimensionAsync(playerName)),
            Health = await GetPlayerHealthAsync(playerName),
            FoodLevel = await GetPlayerFoodLevelAsync(playerName)
        };

        public static async Task<Coordinate> GetPlayerCoordinateAsync(string playerName)
        {
            return await Task.Run(() =>
            {
                // Create player data pull task
                Coordinate? coordinate = null;
                var handler = new EventHandler<ServerConsoleOutputEventArgs>((object? s, ServerConsoleOutputEventArgs e) =>
                {
                    if (EntityDataRegex.IsMatch(e.LogEntry.Message))
                    {
                        // If this matches the Pos data we want
                        var entityData = EntityDataRegex.Match(e.LogEntry.Message).Groups[2].Value;

                        // We also need to compare player name with it
                        if (CoordinateDataRegex.IsMatch(entityData) && EntityDataRegex.Match(e.LogEntry.Message).Groups[1].Value == playerName)
                        {
                            var loc = CoordinateDataRegex.Match(entityData);

                            coordinate = new Coordinate
                            {
                                X = double.Parse(loc.Groups[2].Value),
                                Y = double.Parse(loc.Groups[3].Value),
                                Z = double.Parse(loc.Groups[4].Value)
                            };
                        }
                    }
                });

                ServerConsoleOutputEvent.ServerConsoleOutput += handler;

                // Get location data
                ServerManager.SendConsoleMessage($"data get entity {playerName} Pos");

                while (coordinate == null) { }

                // Unsubscribe event (this is a one-time event)
                ServerConsoleOutputEvent.ServerConsoleOutput -= handler;

                return coordinate;
            });
        }

        public static async Task<Dimension> GetPlayerDimensionAsync(string playerName)
        {
            return await Task.Run(() =>
            {
                var dimension = Dimension.Invalid;
                var handler = new EventHandler<ServerConsoleOutputEventArgs>((object? s, ServerConsoleOutputEventArgs e) =>
                {
                    if (EntityDataRegex.IsMatch(e.LogEntry.Message))
                    {
                        // If this matches the Dimension data we want
                        var entityData = EntityDataRegex.Match(e.LogEntry.Message).Groups[2].Value;

                        // Match player name
                        if (EntityDataRegex.Match(e.LogEntry.Message).Groups[1].Value == playerName)
                        {
                            dimension = entityData.Trim('\"').ParseDimension();
                        }
                    }
                });

                ServerConsoleOutputEvent.ServerConsoleOutput += handler;

                // Get location data
                ServerManager.SendConsoleMessage($"data get entity {playerName} Dimension");

                while (dimension == Dimension.Invalid) { }

                // Unsubscribe event (this is a one-time event)
                ServerConsoleOutputEvent.ServerConsoleOutput -= handler;

                return dimension;
            });
        }

        public static async Task<float> GetPlayerHealthAsync(string playerName)
        {
            return await Task.Run(() =>
            {
                // Create player data pull task
                float health = -1;
                var handler = new EventHandler<ServerConsoleOutputEventArgs>((object? s, ServerConsoleOutputEventArgs e) =>
                {
                    if (EntityDataRegex.IsMatch(e.LogEntry.Message))
                    {
                        // If this matches the Health data we want
                        var entityData = EntityDataRegex.Match(e.LogEntry.Message).Groups[2].Value;

                        // We also need to compare player name with it
                        if (FloatDataRegex.IsMatch(entityData) && EntityDataRegex.Match(e.LogEntry.Message).Groups[1].Value == playerName)
                        {
                            var match = FloatDataRegex.Match(entityData);

                            health = float.Parse(match.Groups[1].Value);
                        }
                    }
                });

                ServerConsoleOutputEvent.ServerConsoleOutput += handler;

                // Get location data
                ServerManager.SendConsoleMessage($"data get entity {playerName} Health");

                while (health == -1) { }

                // Unsubscribe event (this is a one-time event)
                ServerConsoleOutputEvent.ServerConsoleOutput -= handler;

                return health;
            });
        }

        public static async Task<int> GetPlayerFoodLevelAsync(string playerName)
        {
            return await Task.Run(() =>
            {
                // Create player data pull task
                int foodLevel = -1;
                var handler = new EventHandler<ServerConsoleOutputEventArgs>((object? s, ServerConsoleOutputEventArgs e) =>
                {
                    if (EntityDataRegex.IsMatch(e.LogEntry.Message))
                    {
                        // If this matches the FoodLevel data we want
                        var entityData = EntityDataRegex.Match(e.LogEntry.Message).Groups[2].Value;

                        // We also need to compare player name with it
                        if (int.TryParse(entityData, out _) && EntityDataRegex.Match(e.LogEntry.Message).Groups[1].Value == playerName)
                        {
                            foodLevel = int.Parse(entityData);
                        }
                    }
                });

                ServerConsoleOutputEvent.ServerConsoleOutput += handler;

                // Get location data
                ServerManager.SendConsoleMessage($"data get entity {playerName} foodLevel");

                while (foodLevel == -1) { }

                // Unsubscribe event (this is a one-time event)
                ServerConsoleOutputEvent.ServerConsoleOutput -= handler;

                return foodLevel;
            });
        }
    }
}
