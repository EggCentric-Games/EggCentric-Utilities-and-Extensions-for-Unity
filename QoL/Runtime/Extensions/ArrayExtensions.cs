using System.Collections.Generic;
using System.Linq;

namespace EggCentric.QoL
{
    public static class ArrayExtensions
    {
        public static T? ElementAtLooped<T>(this ICollection<T>? collection, int index)
        {
            if (!collection?.Any() ?? true)
                return default;

            int indexWrapped = index.Wrapped(collection.Count);

            return collection.ElementAt(indexWrapped);
        }
    }
}
