using MinecraftServerShell.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Models
{
    public class PluginRegistry
    {
        public PluginIndexModel Index { get; set; }

        public string Path { get; set; }

        public string PluginEntry { get; set; }

        public Dictionary<string, EventType> RegisteredEvents { get; set; }
    }
}
