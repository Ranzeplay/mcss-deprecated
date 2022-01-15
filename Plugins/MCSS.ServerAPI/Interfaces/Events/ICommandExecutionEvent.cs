using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSS.ServerAPI.Interfaces.Events
{
    public interface ICommandExecutionEvent
    {
        void OnCommandExecution(string issuer, string commandName, string[] arguments);
    }
}
