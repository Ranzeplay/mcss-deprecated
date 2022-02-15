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

        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Issuer { get; set; } = null!;

        public string CreateTime { get; set; } = null!;

        public long ArchiveSize { get; set; }
    }
}
