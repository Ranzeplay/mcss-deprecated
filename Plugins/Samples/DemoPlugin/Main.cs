using MCSS.ServerAPI.Interfaces;

namespace DemoPlugin
{
    public class Main : IPluginEntry
    {
        public void OnPluginLoad()
        {
            Console.WriteLine("Plugin has been loaded!");
        }

        public void OnPluginUnload()
        {
            Console.WriteLine("Plugin has been unloaded!");
        }
    }
}