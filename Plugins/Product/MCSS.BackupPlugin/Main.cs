using MCSS.BackupPlugin.Properties;
using MinecraftServerShell.Core.Events.ServerEvents.Gameplay;
using MinecraftServerShell.Core.Interfaces;
using MinecraftServerShell.Core.Managers;

namespace MCSS.BackupPlugin
{
    public class Main : IPluginEntry
    {
        internal static Main Instance = null!;

        internal PluginLogManager LogManager = new(Resources.Name);

        public void OnPluginLoad()
        {
            Instance = this;

            CommandExecuteEvent.CommandExecution += CommandExecutor.CommandExecuteEvent_CommandExecution;

            LogManager.LogInfo("Plugin has been loaded!");
        }

        

        public void OnPluginUnload()
        {
            LogManager.LogInfo("Plugin has been unloaded!");
        }
    }
}