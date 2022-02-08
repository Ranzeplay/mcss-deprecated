using MCSS.BackupPlugin.Properties;
using MinecraftServerShell.Core.Interfaces;
using MinecraftServerShell.Core.Managers;

namespace MCSS.BackupPlugin
{
    public class Main : IPluginEntry
    {
        internal static Main Instance = null!;

        protected PluginLogManager LogManager = new(Resources.Name);

        public void OnPluginLoad()
        {
            Instance = this;

            LogManager.LogInfo("Plugin has been loaded!");
        }

        public void OnPluginUnload()
        {
            LogManager.LogInfo("Plugin has been unloaded!");
        }
    }
}