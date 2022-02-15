using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSS.BackupPlugin
{
    internal class Utils
    {
        // From https://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net
        internal static string ReadableFileSizeFormatter(long length)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (length >= 1024 && order < sizes.Length - 1)
            {
                order++;
                length /= 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            return string.Format("{0:0.##} {1}", length, sizes[order]);
        }

        internal static void CopyDirectory(string source, string target)
        {
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }

            var directory = new DirectoryInfo(source);
            foreach (var entry in directory.GetDirectories())
            {
                CopyDirectory(entry.FullName, Path.Combine(target, entry.Name));
            }

            foreach (var entry in directory.GetFiles())
            {
                if (entry.Name != "session.lock")
                {
                    File.Copy(entry.FullName, Path.Combine(target, entry.Name));
                }
            }
        }
    }
}
