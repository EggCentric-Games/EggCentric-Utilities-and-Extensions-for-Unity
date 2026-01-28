using UnityEngine;

namespace EggCentric.ModifiableValues
{
    public class HighLimitModifier : ValueModifier
    {
        private float _limit;

        public HighLimitModifier(float limit = float.MaxValue, float priority = 0f) : base(priority) => _limit = limit;

        public override (float refBase, float value) Apply(float refBase, float value)
        {
            float result = Mathf.Min(value, _limit);
            return (result, result);
        }
    }
}
