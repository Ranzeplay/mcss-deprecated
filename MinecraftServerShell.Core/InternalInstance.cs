using MinecraftServerShell.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core
{
    public static class InternalInstance
    {
        public static Process ServerProcess { get; set; } = new();

        public static List<PluginRegistry> PluginsEnabled { get; set; } = new();

        public static ApplicationLog AppLog { get; set; }
    }

    public class ApplicationLog
    {
        public ApplicationLog(long capacity)
        {
            ServerLog = new(capacity);
            PluginLog = new(capacity);
            OtherLog = new(capacity);
        }

        public LimitedLogList<Log> ServerLog { get; set; }

        public LimitedLogList<Log> PluginLog { get; set; }

        public LimitedLogList<Log> OtherLog { get; set; }
    }
}
