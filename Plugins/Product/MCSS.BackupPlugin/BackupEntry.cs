using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSS.BackupPlugin
{
    internal class BackupEntry
    {
        // public BackupEntry(string name, string issuer, long size)
        // {
        //     Name = name;
        //     Issuer = issuer;
        //     CreateTime = DateTime.Now;
        //     ArchiveSize = size;
        //     Id = Guid.NewGuid().ToString().Split('-')[0];
        // }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Issuer { get; set; }

        public string CreateTime { get; set; }

        public long ArchiveSize { get; set; }
    }
}
