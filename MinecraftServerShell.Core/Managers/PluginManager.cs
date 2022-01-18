using MCSS.ServerAPI.Interfaces;
using MCSS.ServerAPI.Interfaces.Events;
using MCSS.ServerAPI.Interfaces.Events.PlayerEvents;
using MCSS.ServerAPI.Interfaces.Events.ServerEvents;
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

        private static PluginRegistry ReadPluginData(string path)
        {
            var pluginAssembly = Assembly.LoadFrom(path);
            var pluginRegistry = new PluginRegistry
            {
                Index = ReadMetadata(pluginAssembly),
                Path = path,
                RegisteredEvents = new Dictionary<string, EventType>()
            };


            foreach (var type in pluginAssembly.GetExportedTypes())
            {
                var interfaces = type.GetInterfaces();

                if (interfaces.Contains(typeof(IPluginEntry)))
                {
                    pluginRegistry.PluginEntry = type.FullName;
                }

                // Check if current type is an event handler
                if (interfaces.Contains(typeof(IPlayerChatEvent)))
                {
                    pluginRegistry.RegisteredEvents[type.FullName] = EventType.PlayerChat;
                }
                if (interfaces.Contains(typeof(IPlayerJoinEvent)))
                {
                    pluginRegistry.RegisteredEvents[type.FullName] = EventType.PlayerJoin;
                }
                if (interfaces.Contains(typeof(IPlayerLeaveEvent)))
                {
                    pluginRegistry.RegisteredEvents[type.FullName] = EventType.PlayerLeave;
                }
                if (interfaces.Contains(typeof(IServerStartEvent)))
                {
                    pluginRegistry.RegisteredEvents[type.FullName] = EventType.ServerStart;
                }
                if (interfaces.Contains(typeof(IServerStopEvent)))
                {
                    pluginRegistry.RegisteredEvents[type.FullName] = EventType.ServerStop;
                }
                if (interfaces.Contains(typeof(ICommandExecutionEvent)))
                {
                    pluginRegistry.RegisteredEvents[type.FullName] = EventType.CommandExecution;
                }
            }

            return pluginRegistry;
        }

        public static void LoadPlugin(string path)
        {
            var plugin = ReadPluginData(path);
            if (plugin != null)
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
