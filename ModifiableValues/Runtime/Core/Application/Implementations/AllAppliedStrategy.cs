using System.Collections.Generic;

namespace EggCentric.ModifiableValues
{
    public class AllAppliedStrategy : ModificationApplicationStrategy
    {
        public AllAppliedStrategy(IReadOnlyCollection<IValueModifier> activeModifiers) : base(activeModifiers)
        {
        }

        public override float ApplyFor(float baseValue)
        {
            if (activeModifiers.Count <= 0)
                return baseValue;

            (float refBase, float modifiedValue) modificationPair = (baseValue, baseValue);

            foreach (var modifier in activeModifiers)
                modificationPair = modifier.Apply(modificationPair.refBase, modificationPair.modifiedValue);

            return modificationPair.modifiedValue;
        }
    }
}
