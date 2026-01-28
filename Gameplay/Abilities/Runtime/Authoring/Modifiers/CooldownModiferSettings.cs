using UnityEngine;

namespace EggCentric.Abilities.Modifiers
{
    public class CooldownModiferSettings : AbilityModifierSettings<CooldownModifier>
    {
        [SerializeField] private float _cooldown;

        protected override CooldownModifier CreateInstance() => new CooldownModifier(_cooldown);
    }
}