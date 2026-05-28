using EggCentric.NoiseGeneration.Modifiers;
using UnityEngine;

namespace EggCentric.NoiseGeneration.Authoring.Modifiers
{
    [System.Serializable]
    public class SignedPowerModifierConfig : ModifierConfig<SignedPowerModifier>
    {
        public float Power => _power;

        [SerializeField] private float _power = 1f;

        protected override SignedPowerModifier CreateModifier() => new SignedPowerModifier(_power);
    }
}
