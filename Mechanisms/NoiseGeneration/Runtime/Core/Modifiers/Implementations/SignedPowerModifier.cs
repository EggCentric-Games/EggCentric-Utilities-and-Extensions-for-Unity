using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace EggCentric.ProceduralGeneration.Modifiers
{

    [MovedFrom(true, "EggCentric.ProceduralGeneration", null, "AbsolutePowerLayerModifier")]
    public class SignedPowerModifier : IGenerationModifier
    {
        private readonly float _power;

        public SignedPowerModifier(float power)
        {
            _power = power;
        }

        public float Modify(float value)
        {
            var sign = Mathf.Sign(value);
            return sign * Mathf.Pow(Mathf.Abs(value), _power);
        }
    }
}
