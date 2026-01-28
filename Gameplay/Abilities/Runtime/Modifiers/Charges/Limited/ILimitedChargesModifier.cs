using EggCentric.ModifiableValues;

namespace EggCentric.Abilities.Modifiers
{
    public interface ILimitedChargesModifier
    {
        public ModifiableValue MaxCharges { get; }
    }
}