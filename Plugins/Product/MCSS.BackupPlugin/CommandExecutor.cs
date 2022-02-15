using MinecraftServerShell.Core.Events.ServerEvents.Gameplay;
using MinecraftServerShell.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSS.BackupPlugin
{
    internal class CommandExecutor
    {
        internal static async void CommandExecuteEvent_CommandExecution(object? sender, CommandExecutionEventArgs e)
        {
            if (e.CommandName.ToLower() == "backup" && e.CommandArgs.Length >= 1)
            {
                switch (e.CommandArgs[0].ToLower())
                {
                    case "make":
                        var name = "Unnamed backup";
                        if(e.CommandArgs.Length > 1)
                        {
                            name = e.CommandArgs[1];
                        }

                        await BackupManager.CreateBackup(name, e.Issuer);
                        break;
                    case "rollback":
                        if (e.CommandArgs.Length > 1)
                        {
                            await BackupManager.RollbackBackup(e.CommandArgs[1], e.Issuer);
                        }
                        break;
                    case "list":
                        var backupList = BackupManager.GetAllBackup();
                        PrintBackupList(backupList, e.Issuer);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void PrintBackupList(BackupEntry[] backupList, string target)
        {
            backupList = backupList.OrderBy(x => x.CreateTime).ToArray();

            ServerManager.SendConsoleMessage($"tellraw {target} \"{backupList.Length} backup(s) found in total\"");
            ServerManager.SendConsoleMessage($"tellraw {target} \"----------------------------------------------\"");
            foreach (var entry in backupList)
            {
                ServerManager.SendConsoleMessage($"tellraw {target} \"Backup entry: {entry.Name} ({entry.Id})\"");
                ServerManager.SendConsoleMessage($"tellraw {target} \"Create time: {entry.CreateTime}\"");
                ServerManager.SendConsoleMessage($"tellraw {target} \"Issuer: {entry.Issuer}\"");
                ServerManager.SendConsoleMessage($"tellraw {target} \"Size: {Utils.ReadableFileSizeFormatter(entry.ArchiveSize)}\"");
                ServerManager.SendConsoleMessage($"tellraw {target} \"----------------------------------------------\"");
            }
        }
    }
}
