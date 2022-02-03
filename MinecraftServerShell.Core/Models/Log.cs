using MinecraftServerShell.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Models
{
    public class Log
    {
        public LogLevel Level { get; set; }

        public DateTime CreateTime { get; set; }

        public string Message { get; set; }
    }
}
