using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace EggCentric.ProceduralGeneration.Modifiers
{
    [MovedFrom(true, "EggCentric.ProceduralGeneration", null, "PowerLayerModifier")]
    public class PowerModifier : IGenerationModifier
    {
        private readonly float _power;

        public PowerModifier(float power)
        {
            _power = power;
        }

        public float Modify(float value) => Mathf.Pow(value, _power);
    }
}
