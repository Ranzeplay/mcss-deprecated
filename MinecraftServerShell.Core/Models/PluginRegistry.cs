using MinecraftServerShell.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Models
{
    public class PluginRegistry
    {
        public PluginIndexModel Index { get; set; } = null!;

        public string Path { get; set; } = null!;

        public string PluginEntry { get; set; } = null!;

        public Assembly PluginAssembly { get; set; } = null!;
    }
}
