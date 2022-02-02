using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Events.ServerEvents.Gameplay
{
    public class CommandExecutionEvent
    {
        public static event EventHandler<CommandExecutionEventArgs> CommandExecution = null!;

        protected virtual void OnCommandExecution(CommandExecutionEventArgs e)
        {
            CommandExecution?.Invoke(this, e);
        }
    }

    public class CommandExecutionEventArgs : EventArgs
    {
        public string Issuer { get; set; } = null!;

        public string CommandName { get; set; } = null!;

        public string[] CommandArgs { get; set; } = null!;

        public string RawData { get; set; } = null!;
    }
}
