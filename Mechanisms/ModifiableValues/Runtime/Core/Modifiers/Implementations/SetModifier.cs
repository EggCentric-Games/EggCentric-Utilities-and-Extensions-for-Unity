namespace EggCentric.ModifiableValues
{
    public class SetModifier : ValueModifier
    {
        private float _targetValue;

        public SetModifier(float targetValue, float priority = 0f) : base(priority) => _targetValue = targetValue;

        public override (float refBase, float value) Apply(float refBase, float value)
        {
            float result = _targetValue;
            return (result, result);
        }
    }
}
