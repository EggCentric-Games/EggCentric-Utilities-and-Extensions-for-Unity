using System.Collections.Generic;

namespace EggCentric.ModifiableValues
{
    public abstract class ModificationApplicationStrategy : IModificationApplicationStrategy
    {
        protected readonly IReadOnlyCollection<IValueModifier> activeModifiers;

        public ModificationApplicationStrategy(IReadOnlyCollection<IValueModifier> activeModifiers) => this.activeModifiers = activeModifiers;

        public abstract float ApplyFor(float baseValue);
    }
}
