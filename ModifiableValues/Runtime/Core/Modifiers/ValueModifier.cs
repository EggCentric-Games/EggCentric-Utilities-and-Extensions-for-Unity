namespace EggCentric.ModifiableValues
{
    public abstract class ValueModifier : IValueModifier
    {
        public float Priority { get; private set; }

        public ValueModifier(float priority = 0f) => Priority = priority;

        public abstract (float refBase, float value) Apply(float refBase, float value);
    }
}
