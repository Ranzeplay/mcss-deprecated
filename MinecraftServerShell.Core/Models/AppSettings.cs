using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Models
{
    public class AppSettings
    {
        public AppSettings()
        {
            ServerDirectory = Path.Combine(Environment.CurrentDirectory, "Server");
            PluginDirectory = Path.Combine(Environment.CurrentDirectory, "Plugins");

            StartupCommand = string.Empty;
            MaxLogLength = 1000;
        }

        public string ServerDirectory { get; set; }

        public string StartupCommand { get; set; }

        public string PluginDirectory { get; set; }

        public long MaxLogLength { get; set; }
    }
}
