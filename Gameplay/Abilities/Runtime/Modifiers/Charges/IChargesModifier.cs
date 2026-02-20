using EggCentric.ValueProviders.DataContainers;

namespace EggCentric.Abilities.Modifiers
{
    public interface IChargesModifier
    {
        public Field<int> RemainingCharges { get; }
    }
}