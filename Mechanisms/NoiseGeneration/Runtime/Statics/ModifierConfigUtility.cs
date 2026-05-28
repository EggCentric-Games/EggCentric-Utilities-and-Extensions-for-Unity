using System.Collections.Generic;
using EggCentric.NoiseGeneration.Modifiers;

namespace EggCentric.NoiseGeneration.Authoring.Modifiers
{
    internal static class ModifierConfigUtility
    {
        public static IGenerationModifier[] CreateModifiers(IModifierConfig[] modifierConfigs)
        {
            if (modifierConfigs == null || modifierConfigs.Length == 0)
                return System.Array.Empty<IGenerationModifier>();

            var modifiers = new List<IGenerationModifier>(modifierConfigs.Length);
            for (int i = 0; i < modifierConfigs.Length; i++)
            {
                var config = modifierConfigs[i];
                if (config != null)
                    modifiers.Add(config.CreateInstance());
            }

            return modifiers.ToArray();
        }
    }
}
