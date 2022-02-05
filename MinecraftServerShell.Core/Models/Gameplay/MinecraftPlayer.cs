using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Models.Gameplay
{
    public class MinecraftPlayer
    {
        public string Name { get; set; } = null!;

        public Location Location { get; set; } = null!;
    }
}
