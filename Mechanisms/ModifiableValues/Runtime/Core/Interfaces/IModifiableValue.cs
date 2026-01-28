namespace EggCentric.ModifiableValues
{
    public interface IModifiableValue : IReadOnlyModifiableValue
    {
        public IValueModifier AddModifier(IValueModifier modifier);
        public void RemoveModifier(IValueModifier modifier);
    }
}
