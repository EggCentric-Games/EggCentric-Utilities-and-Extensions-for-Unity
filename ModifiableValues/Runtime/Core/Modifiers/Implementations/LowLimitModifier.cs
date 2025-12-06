using UnityEngine;

namespace EggCentric.ModifiableValues
{
    public class LowLimitModifier : ValueModifier
    {
        private float _limit;

        public LowLimitModifier(float limit = float.MinValue, float priority = 0f) : base(priority) => _limit = limit;

        public override (float refBase, float value) Apply(float refBase, float value)
        {
            float result = Mathf.Max(value, _limit);
            return (result, result);
        }
    }
}
