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

        public event EventHandler<T> NewLog;

        public LimitedLogList(long capacity)
        {
            Capacity = capacity;
        }

        public void Append(T element)
        {
            Elements.AddLast(element);

            if(Elements.Count > Capacity)
            {
                Elements.RemoveLast();
            }

            NewLog?.Invoke(this, element);
        }
    }
}
