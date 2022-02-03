using MinecraftServerShell.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Models
{
    public class Log
    {
        public LogLevel Level { get; set; }

        public DateTime CreateTime { get; set; }

        public string Message { get; set; }
    }

    public class PluginLog : Log
    {
        public string PluginName { get; set; }
    }

    public class ServerLog : Log
    {
        public string Issuer { get; set; }

        public string LogLevel { get; set; }
    }
}
