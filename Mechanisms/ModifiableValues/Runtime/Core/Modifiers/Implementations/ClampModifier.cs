using UnityEngine;

namespace EggCentric.ModifiableValues
{
    public class ClampModifier : ValueModifier
    {
        private float _minValue;
        private float _maxValue;

        public ClampModifier(float minValue = float.MinValue, float maxValue = float.MaxValue, float priority = 0f) : base(priority)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public override (float refBase, float value) Apply(float refBase, float value)
        {
            float result = Mathf.Clamp(value, _minValue, _maxValue);
            return (result, result);
        }
    }
}
