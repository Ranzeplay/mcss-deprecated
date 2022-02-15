using MCSS.BackupPlugin.Properties;
using MinecraftServerShell.Core.Managers;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MCSS.BackupPlugin
{
    internal class BackupManager
    {
        private static void BroadcastMessage(string content)
        {
            ServerManager.SendConsoleMessage($"tellraw @a \"[Backup] {content}\"");
        }

        internal static async Task CreateBackup(string name, string issuer)
        {
            Main.Instance.LogManager.LogInfo("Creating backup");

            // Cannot start more than one backup instance at the same time
            if (Main.Instance.IsIdle)
            {
                // Set status
                Main.Instance.IsIdle = false;
            }
            else
            {
                ServerManager.SendConsoleMessage($"tellraw {issuer} \"Oops, there is already a backup task running...\"");
                return;
            }

            BroadcastMessage($"A backup operation has triggered by {issuer}, you may feel laggy for a while...");

            // Record time
            var startTime = DateTime.Now.Ticks;

            var backupId = Guid.NewGuid().ToString().Split('-').First();
            var worldSavePath = Path.Combine(AppSettingsManager.ReadOrCreateSettings().ServerDirectory, "world");
            var temporaryDirectory = Path.Combine(Main.Instance.PluginDataPath, "backup-temp");
            var targetDirectory = Path.Combine(Main.Instance.PluginDataPath, Resources.Name, backupId);
            var targetZipPath = Path.Combine(targetDirectory, "backup.zip");
            

            // Disable auto-save
            ServerManager.SendConsoleMessage("save-off");
            // Make immediate save
            ServerManager.SendConsoleMessage("save-all flush");

            await Task.Run(() =>
            {
                BroadcastMessage("Stage 1/3 : Copy world");
                Utils.CopyDirectory(worldSavePath, temporaryDirectory);

                // Restart auto-save, because we have copied the server directory
                ServerManager.SendConsoleMessage("save-on");

                BroadcastMessage("Stage 2/3 : Compress world...");
                Directory.CreateDirectory(targetDirectory);
                ZipFile.CreateFromDirectory(temporaryDirectory, targetZipPath);

                Directory.Delete(temporaryDirectory, true);
            });

            BroadcastMessage("Stage 3/3 : Finalizing...");
            
            // Save backup profile
            var info = new BackupEntry
            {
                Id = backupId,
                Name = name,
                Issuer = issuer,
                ArchiveSize = new FileInfo(targetZipPath).Length,
                CreateTime = DateTime.Now.ToString("g"),
            };
            File.WriteAllText(Path.Combine(targetDirectory, "info.json"), JsonSerializer.Serialize(info));

            Main.Instance.LogManager.LogInfo($"Backup created, size: {Utils.ReadableFileSizeFormatter(info.ArchiveSize)}");
            BroadcastMessage($"Backup completed in {new TimeSpan(DateTime.Now.Ticks - startTime).TotalSeconds:0.00} seconds, total size is about {Utils.ReadableFileSizeFormatter(info.ArchiveSize)}");
            Main.Instance.IsIdle = true;
        }

        private static void CopyWorldDirectory(string worldDirectory, string containingDirectory)
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

            foreach (var entry in directory.GetFiles())
            {
                if (entry.Name != "session.lock")
                {
                    File.Copy(entry.FullName, Path.Combine(containingDirectory, entry.Name));
                }
            }
        }

        public static BackupEntry[] GetAllBackup()
        {
            var backupEntries = new List<BackupEntry>();
            var backupRootDirectory = Path.Combine(AppSettingsManager.ReadOrCreateSettings().PluginDirectory, Resources.Name);

            foreach (var info in new DirectoryInfo(backupRootDirectory).GetDirectories())
            {
                var profileText = File.ReadAllText(Path.Combine(info.FullName, "info.json"));
                var entry = JsonSerializer.Deserialize<BackupEntry>(profileText);

                backupEntries.Add(entry);
            }

            return backupEntries.ToArray();
        }
    }
}
