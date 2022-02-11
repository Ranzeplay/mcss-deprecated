using MinecraftServerShell.Core.Events.ServerEvents.Gameplay;
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
            if (e.CommandName == "backup" && e.CommandArgs.Length > 1)
            {
                switch (e.CommandArgs[0].ToLower())
                {
                    case "make":
                        await BackupManager.CreateBackup(e.CommandArgs[1], e.Issuer);
                        break;
                    case "rollback":
                        break;
                    case "list":
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
