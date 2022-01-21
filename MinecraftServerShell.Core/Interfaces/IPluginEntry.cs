using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Interfaces
{
    public interface IPluginEntry
    {
        /// <summary>
        /// This method will be called when MCSS is loading this plugin
        /// </summary>
        void OnPluginLoad();

        /// <summary>
        /// This method will be called when MCSS is unloading this plugin
        /// </summary>
        void OnPluginUnload();
    }
}
