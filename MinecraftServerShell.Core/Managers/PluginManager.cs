using MinecraftServerShell.Core.Events.ServerEvents.Gameplay.Player;
using MinecraftServerShell.Core.Interfaces;
using MinecraftServerShell.Core.Models;
using MinecraftServerShell.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Managers
{
    public class PluginManager
    {
        private static PluginIndexModel ReadMetadata(Assembly assembly)
        {
            var pluginIndex = new PluginIndexModel();

            // Find metadata storage place
            try
            {
                var storage = assembly.GetTypes().First(t => t.FullName.Contains("Properties.Resources"));

                var resource = new ResourceManager(storage.FullName, assembly);
                pluginIndex.Name = resource.GetString("Name");
                pluginIndex.Description = resource.GetString("Description");
                pluginIndex.Author = resource.GetString("Author");
                pluginIndex.Version = resource.GetString("Version");

                return pluginIndex;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        private static string? GetPluginEntry(Assembly assembly)
        {
            try
            {
                var entryClass = assembly.GetTypes().FirstOrDefault(t => t.GetInterfaces().Any(i => i.FullName == typeof(IPluginEntry).FullName));

                return entryClass != null ? entryClass.FullName : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public static void LoadPlugin(string path)
        {
            var assembly = Assembly.LoadFile(path);

            var plugin = new PluginRegistry
            {
                Index = ReadMetadata(assembly),
                PluginEntry = GetPluginEntry(assembly),
                Path = path
            };

            if (plugin.PluginEntry != null && plugin.Index != null)
            {
                InternalInstance.PluginsEnabled.Add(plugin);
            }
        }

        public static void LoadAllPlugins(string directory)
        {
            foreach (var file in Directory.GetFiles(directory, "*.dll"))
            {
                LoadPlugin(file);
            }

            InternalInstance.PluginsEnabled.RemoveAll(p => p == null);
        }
    }
}
