using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.ModifiableValues
{
    public class HighestStrategy : ModificationApplicationStrategy
    {
        public HighestStrategy(IReadOnlyCollection<IValueModifier> activeModifiers) : base(activeModifiers)
        {
        }

        public override float ApplyFor(float baseValue)
        {
            if (activeModifiers.Count <= 0)
                return baseValue;

            float maxResult = float.MinValue;

            foreach (var modifier in activeModifiers)
            {
                float result = modifier.Apply(baseValue, baseValue).value;
                maxResult = Mathf.Max(maxResult, result);
            }

            return maxResult;
        }
    }
}
