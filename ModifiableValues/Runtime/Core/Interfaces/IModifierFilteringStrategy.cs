using System;
using System.Collections.Generic;
using System.Linq;

namespace EggCentric.ModifiableValues
{
    public interface IModifierFilteringStrategy
    {
        public IReadOnlyCollection<IValueModifier> FilterModifiers(IReadOnlyCollection<IValueModifier> collection);
    }

    public abstract class ModifierFilteringStrategy : IModifierFilteringStrategy
    {
        protected readonly Func<IEnumerable<IValueModifier>, IEnumerable<IValueModifier>> sortingRule;

        public ModifierFilteringStrategy() : this(x => x.OrderByDescending(x => x.Priority))
        {
        }

        public ModifierFilteringStrategy(Func<IEnumerable<IValueModifier>, IEnumerable<IValueModifier>> sortingRule) => this.sortingRule = sortingRule;

        public abstract IReadOnlyCollection<IValueModifier> FilterModifiers(IReadOnlyCollection<IValueModifier> collection);
    }
}
