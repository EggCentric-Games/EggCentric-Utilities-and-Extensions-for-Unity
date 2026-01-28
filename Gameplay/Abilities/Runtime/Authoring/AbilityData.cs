using EggCentric.Abilities.Modifiers;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.Abilities
{
    [System.Serializable]
    public abstract class AbilityData
    {
        public IReadOnlyList<AbilityModifierSettings> Modifiers => _modifiers;

        [SerializeField] private List<AbilityModifierSettings> _modifiers;
    }
}