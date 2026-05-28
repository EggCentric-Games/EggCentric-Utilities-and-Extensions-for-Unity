using System.Collections.Generic;

namespace EggCentric.ProceduralGeneration.Modifiers
{
    public static class ModifierUtility
    {
        public static float Apply(IReadOnlyList<IGenerationModifier> modifiers, float value)
        {
            if (modifiers == null)
                return value;

            for (int i = 0; i < modifiers.Count; i++)
            {
                var modifier = modifiers[i];
                if (modifier != null)
                    value = modifier.Modify(value);
            }

            return value;
        }
    }
}
