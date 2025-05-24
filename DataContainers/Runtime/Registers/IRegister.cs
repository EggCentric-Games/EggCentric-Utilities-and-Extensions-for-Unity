using System;
using System.Collections.Generic;

namespace EggCentric.Utilities.DataContainers
{
    public interface IRegister<T>
    {
        public event Action<T> OnItemEntry;
        public event Action<T> OnItemExit;

        public void ProcessEntry(IEnumerable<T> registerEntry);
    }
}