using MinecraftServerShell.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Managers
{
    public class LogManager
    {
        private string Prefix { get; }

        public LogManager(string prefix)
        {
            Prefix = prefix;
        }

        protected static void Log(LogLevel level, string message)
        {
            InternalInstance.AppLog.PluginLog.Append(new Models.Log
            {
                Level = level,
                Message = message,
                CreateTime = DateTime.Now,
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
