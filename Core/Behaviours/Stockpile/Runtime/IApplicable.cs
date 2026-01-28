using System.Collections.Generic;

namespace EggCentric.Common.Behaviours
{
    public interface IApplicable<in T>
    {
        public void ApplyTo(T item);
        public void ApplyTo(IEnumerable<T> items);
    }
}