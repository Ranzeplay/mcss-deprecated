using MinecraftServerShell.Dashboard.Models;
using MinecraftServerShell.Dashboard.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Dashboard
{
    internal static class InternalInstance
    {
        public static Process ServerProcess { get; set; } = new();

        public static ConsoleOutputPage ConsoleOutputPage { get; set; } = new();

        public static List<PluginRegistry> PluginsEnabled { get; set; } = new();
    }
}
