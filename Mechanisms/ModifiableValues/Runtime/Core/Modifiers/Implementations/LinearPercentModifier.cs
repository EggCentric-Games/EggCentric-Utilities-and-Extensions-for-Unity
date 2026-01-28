namespace EggCentric.ModifiableValues
{
    public class LinearPercentModifier : ValueModifier
    {
        private float _multiplier;

        public LinearPercentModifier(float multiplier = 1f, float priority = 0f) : base(priority) => _multiplier = multiplier;

        public override (float refBase, float value) Apply(float refBase, float value)
        {
            float result = value + (refBase * (_multiplier - 1));
            return (refBase, result);
        }
    }
}
