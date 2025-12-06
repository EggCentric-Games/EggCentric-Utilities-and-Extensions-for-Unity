namespace EggCentric.ModifiableValues
{
    public class InverseModifier : ValueModifier
    {
        private float _base;

        public InverseModifier(float @base, float priority = 0f) : base(priority) => _base = @base;

        public override (float refBase, float value) Apply(float refBase, float value)
        {
            float result = _base / value;
            return (result, result);
        }
    }
}
