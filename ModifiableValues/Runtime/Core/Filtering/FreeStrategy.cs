using System;
using System.Collections.Generic;
using System.Linq;

namespace EggCentric.ModifiableValues
{
    public class FreeStrategy : ModifierFilteringStrategy
    {
        public FreeStrategy() : base() { }
        public FreeStrategy(Func<IEnumerable<IValueModifier>, IEnumerable<IValueModifier>> sortingRule) : base(sortingRule) { }

        public override IReadOnlyCollection<IValueModifier> FilterModifiers(IReadOnlyCollection<IValueModifier> collection) => sortingRule(collection).ToList();
    }
}
