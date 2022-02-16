using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Dashboard.Models
{
    public class ChangeBroadcast<T>
    {
        public event EventHandler<T> ValueChange = null!;

        public T Value { get; private set; }

        public void UpdateValue(T newValue)
        {
            Value = newValue;
            ValueChange?.Invoke(this, newValue);
        }
    }
}
