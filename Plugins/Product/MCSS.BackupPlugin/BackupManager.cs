using MCSS.BackupPlugin.Properties;
using MinecraftServerShell.Core.Managers;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSS.BackupPlugin
{
    internal class BackupManager
    {
        private static void BroadcastMessage(string content)
        {
            ServerManager.SendConsoleMessage($"say [Backup] {content}");
        }

        internal static async Task CreateBackup(string name)
        {
            Main.Instance.LogManager.LogInfo("Creating backup");
            BroadcastMessage("Backing up, you may feel laggy for a while...");

            // Record time
            var startTime = DateTime.Now.Ticks;

            var worldSavePath = Path.Combine(AppSettingsManager.ReadOrCreateSettings().ServerDirectory, "world");
            var temporaryDirectory = Path.Combine(AppSettingsManager.ReadOrCreateSettings().PluginDirectory, Resources.Name, "backup-temp");
            var targetSavePath = Path.Combine(AppSettingsManager.ReadOrCreateSettings().PluginDirectory, Resources.Name, "target.zip");

            // Disable auto-save
            ServerManager.SendConsoleMessage("save-off");
            // Make immediate save
            ServerManager.SendConsoleMessage("save-all flush");

            await Task.Run(() =>
            {
                static void CopyWorldDirectory(string worldDirectory, string containingDirectory)
                {
                    if (!Directory.Exists(containingDirectory))
                    {
                        Directory.CreateDirectory(containingDirectory);
                    }

                    var directory = new DirectoryInfo(worldDirectory);
                    foreach (var entry in directory.GetDirectories())
                    {
                        CopyWorldDirectory(entry.FullName, Path.Combine(containingDirectory, entry.Name));
                    }

                    foreach(var entry in directory.GetFiles())
                    {
                        if(entry.Name != "session.lock")
                        {
                            File.Copy(entry.FullName, Path.Combine(containingDirectory, entry.Name));
                        }
                    }
                }

                CopyWorldDirectory(worldSavePath, temporaryDirectory);

                // Restart auto-save, because we have copied the server directory
                ServerManager.SendConsoleMessage("save-on");

                BroadcastMessage("World copied, compressing world...");
                ZipFile.CreateFromDirectory(temporaryDirectory, targetSavePath);

                Directory.Delete(temporaryDirectory, true);
            });

            
            // From https://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net
            static string ReadableFileSizeFormatter(string filename)
            {
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                double len = new FileInfo(filename).Length;
                int order = 0;
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len /= 1024;
                }

                // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
                // show a single decimal place, and no space.
               return string.Format("{0:0.##} {1}", len, sizes[order]);
            }

            Main.Instance.LogManager.LogInfo($"Backup created, size: {ReadableFileSizeFormatter(targetSavePath)}");
            BroadcastMessage($"Backup completed in {new TimeSpan(DateTime.Now.Ticks - startTime).TotalSeconds:0.00} seconds, total size is about {ReadableFileSizeFormatter(targetSavePath)}");
        }
    }
}
