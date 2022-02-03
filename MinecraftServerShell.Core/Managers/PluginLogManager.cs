using MinecraftServerShell.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Managers
{
    public class PluginLogManager
    {
        private string PluginName { get; }

        public PluginLogManager(string pluginName)
        {
            PluginName = pluginName;
        }

        protected void Log(LogLevel level, string message)
        {
            InternalInstance.AppLog.PluginLog.Append(new Models.PluginLog
            {
                Level = level,
                Message = $"",
                CreateTime = DateTime.Now,
                PluginName = PluginName
            });
        }

        public void LogInfo(string message)
        {
            Log(LogLevel.Info, message);
        }

        public void LogWarning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        public void LogError(string message)
        {
            Log(LogLevel.Warning, message);
        }
    }
}
