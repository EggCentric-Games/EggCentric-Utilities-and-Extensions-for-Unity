using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.ModifiableValues
{
    public class LowestStrategy : ModificationApplicationStrategy
    {
        public LowestStrategy(IReadOnlyCollection<IValueModifier> activeModifiers) : base(activeModifiers)
        {
        }

        public override float ApplyFor(float baseValue)
        {
            if (activeModifiers.Count <= 0)
                return baseValue;

            float minResult = float.MaxValue;

            foreach (var modifier in activeModifiers)
            {
                float result = modifier.Apply(baseValue, baseValue).value;
                minResult = Mathf.Min(minResult, result);
            }

            return minResult;
        }
    }
}
