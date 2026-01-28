namespace EggCentric.ModifiableValues
{
    public class MultiplicativeModifier : ValueModifier
    {
        private float _multiplier;

        public MultiplicativeModifier(float multiplier = 1f, float priority = 0f) : base(priority) => _multiplier = multiplier;

        public override (float refBase, float value) Apply(float refBase, float value)
        {
            float result = value * _multiplier;
            return (result, result);
        }
    }
}
