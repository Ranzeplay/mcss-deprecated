﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Models.Enums
{
    public enum EventType
    {
        PlayerJoin,
        PlayerLeave,
        PlayerChat,
        ServerStart,
        ServerStop,
        CommandExecution
    }
}
