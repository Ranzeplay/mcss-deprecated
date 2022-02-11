using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSS.BackupPlugin
{
    internal class BackupEntry
    {
        public BackupEntry(string name, string issuer)
        {
            Name = name;
            Issuer = issuer;
            CreateTime = DateTime.Now;
        }

        public string Name { get; set; }

        public string Issuer { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
