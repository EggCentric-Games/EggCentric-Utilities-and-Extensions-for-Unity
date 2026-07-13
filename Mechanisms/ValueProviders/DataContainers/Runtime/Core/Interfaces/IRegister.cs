using System;
using System.Collections.Generic;

namespace EggCentric.ValueProviders.DataContainers
{
    public interface IRegister<T>
    {
        public IReadOnlyCollection<T> Items { get; }

        public event Action<T> OnItemEntry;
        public event Action<T> OnItemExit;

        public void ProcessEntry(IEnumerable<T> registerEntry);
    }
}