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
    public static class InternalInstance
    {
        public static ConsoleOutputPage ConsoleOutputPage { get; set; } = new();
    }
}
