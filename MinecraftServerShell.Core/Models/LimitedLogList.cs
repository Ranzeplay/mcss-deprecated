using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Models
{
    public class LimitedLogList<T> where T : Log
    {
        private long Capacity { get; }

        private LinkedList<T> Elements { get; }

        public event EventHandler<T> NewLog = null!;

        public LimitedLogList(long capacity)
        {
            Capacity = capacity;
            Elements = new LinkedList<T>();
        }

        public void Append(T element)
        {
            if (Elements != null)
            {
                Elements.AddLast(element);

                if (Elements.Count > Capacity)
                {
                    Elements.RemoveLast();
                }

                NewLog?.Invoke(this, element);
            }
        }
    }
}
