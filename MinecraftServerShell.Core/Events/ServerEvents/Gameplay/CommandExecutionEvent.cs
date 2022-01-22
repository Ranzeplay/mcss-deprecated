using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Events.ServerEvents.Gameplay
{
    public class CommandExecutionEvent
    {
        public event EventHandler<CommandExecutionEventArgs> CommandExecution;

        protected virtual void OnCommandExecution(CommandExecutionEventArgs e)
        {
            CommandExecution?.Invoke(this, e);
        }
    }

    public class CommandExecutionEventArgs : EventArgs
    {
        public string Issuer { get; set; }

        public string CommandName { get; set; }

        public string[] CommandArgs { get; set; }

        public string RawData { get; set; }
    }
}
