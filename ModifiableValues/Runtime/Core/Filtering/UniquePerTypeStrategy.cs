using System;
using System.Collections.Generic;
using System.Linq;

namespace EggCentric.ModifiableValues
{
    public class UniquePerTypeStrategy : ModifierFilteringStrategy
    {
        public UniquePerTypeStrategy() : base() { }
        public UniquePerTypeStrategy(Func<IEnumerable<IValueModifier>, IEnumerable<IValueModifier>> sortingRule) : base(sortingRule) { }

        public override IReadOnlyCollection<IValueModifier> FilterModifiers(IReadOnlyCollection<IValueModifier> collection)
        {
            if (collection.Count == 0)
                return Array.Empty<IValueModifier>();

            return collection.GroupBy(x => x.GetType()).Select(g => sortingRule(g).First()).ToList();
        }
    }
}
