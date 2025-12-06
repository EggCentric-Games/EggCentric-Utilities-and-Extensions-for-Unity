namespace EggCentric.ModifiableValues
{
    public interface IValueModifier
    {
        public float Priority { get; }

        public (float refBase, float value) Apply(float refBase, float value);
    }
}
