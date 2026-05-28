using EggCentric.NoiseGeneration.Modifiers;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace EggCentric.NoiseGeneration.Authoring.Modifiers
{
    [System.Serializable]
    [MovedFrom(true, "EggCentric.ProceduralGeneration", null, "PowerLayerModifierConfig")]
    public class PowerModifierConfig : ModifierConfig<PowerModifier>
    {
        public float Power => _power;

        [SerializeField] private float _power = 1f;

        protected override PowerModifier CreateModifier() => new PowerModifier(_power);
    }
}
