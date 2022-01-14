using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Dashboard.Models
{
    class AppSettings
    {
        public string ServerDirectory { get; set; }

        public string StartupCommand { get; set; }

        public string PluginDirectory { get; set; }
    }
}
