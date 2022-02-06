using DemoPlugin.Properties;
using MinecraftServerShell.Core.Events.ServerEvents.Gameplay;
using MinecraftServerShell.Core.Events.ServerEvents.Gameplay.Player;
using MinecraftServerShell.Core.Interfaces;
using MinecraftServerShell.Core.Managers;
using MinecraftServerShell.Core.Managers.Gameplay;
using MinecraftServerShell.Core.Models;
using System.Text.Json;

namespace DemoPlugin
{
    public class Main : IPluginEntry
    {
        internal static Main Instance = null!;

        protected PluginLogManager LogManager = new(Resources.Name);

        public void OnPluginLoad()
        {
            LogManager.LogInfo("Plugin has been loaded!");

            Instance = this;

            PlayerJoinEvent.PlayerJoin += (sender, args) => LogManager.LogInfo("あれ？新人？　" + JsonSerializer.Serialize(args));
            PlayerDisconnectEvent.PlayerLeave += (sender, args) => LogManager.LogInfo("やだ。。　" + JsonSerializer.Serialize(args));
            PlayerJoinEvent.PlayerJoin += (sender, args) => LogManager.LogInfo("I heard someone chatting... " + JsonSerializer.Serialize(args));

            CommandExecuteEvent.CommandExecution += CommandExecuteEvent_CommandExecution;
        }

        private async void CommandExecuteEvent_CommandExecution(object? sender, CommandExecutionEventArgs e)
        {
            if (e.CommandName == "demo")
            {
                LogManager.LogInfo($"{e.Issuer} has issued a command with arguments: {JsonSerializer.Serialize(e.CommandArgs)}");

                var playerData = await PlayerManager.GetPlayerAsync(e.Issuer);
                LogManager.LogInfo($"{playerData.Location.Coordinate.X}");
                LogManager.LogInfo($"{Enum.GetName(typeof(Dimension), playerData.Location.Dimension)}");
            }
        }

        public void OnPluginUnload()
        {
            LogManager.LogInfo("Plugin has been unloaded!");
        }
    }
}