using MinecraftServerShell.Core.Events.ServerEvents.Gameplay.Player;
using MinecraftServerShell.Core.Interfaces;
using System.Text.Json;

namespace DemoPlugin
{
    public class Main : IPluginEntry
    {
        public void OnPluginLoad()
        {
            Console.WriteLine("Plugin has been loaded!");

            PlayerJoinEvent.PlayerJoin += (sender, args) => Console.WriteLine("あれ？新人？　" + JsonSerializer.Serialize(args));
            PlayerLeaveEvent.PlayerLeave += (sender, args) => Console.WriteLine("やだ。。　" + JsonSerializer.Serialize(args));
            PlayerJoinEvent.PlayerJoin += (sender, args) => Console.WriteLine("I heard someone chatting... " + JsonSerializer.Serialize(args));
        }

        public void OnPluginUnload()
        {
            Console.WriteLine("Plugin has been unloaded!");
        }
    }
}