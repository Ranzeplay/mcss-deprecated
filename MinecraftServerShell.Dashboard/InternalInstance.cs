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
        internal static ConsoleOutputPage ConsoleOutputPage { get; set; } = new();

        internal static LogPage LogPage { get; set; } = new();

        internal static ChangeBroadcast<string> StatusBarText { get; set; } = new();

        internal static ChangeBroadcast<string> AppStatus { get; set; } = new();
    }
}
