using System;
using System.Collections.Generic;
using System.Linq;

namespace EggCentric.ModifiableValues
{
    public class SingleModifierStrategy : ModifierFilteringStrategy
    {
        public SingleModifierStrategy() : base() { }
        public SingleModifierStrategy(Func<IEnumerable<IValueModifier>, IEnumerable<IValueModifier>> sortingRule) : base(sortingRule) { }

        public override IReadOnlyCollection<IValueModifier> FilterModifiers(IReadOnlyCollection<IValueModifier> collection)
        {
            if (collection.Count <= 0)
                return Array.Empty<IValueModifier>();

            return new IValueModifier[] { sortingRule(collection).First() };
        }
    }
}
