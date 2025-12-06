namespace EggCentric.ModifiableValues
{
    public class FlatModifier : ValueModifier
    {
        private float _change;

        public FlatModifier(float change = 0f, float priority = 0f) : base(priority) => _change = change;

        public override (float refBase, float value) Apply(float refBase, float value)
        {
            float result = value + _change;
            return (result, result);
        }
    }
}
