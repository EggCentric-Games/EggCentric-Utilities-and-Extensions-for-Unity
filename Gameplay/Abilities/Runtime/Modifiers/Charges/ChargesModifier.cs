using EggCentric.DataContainers;

namespace EggCentric.Abilities.Modifiers
{
    public class ChargesModifier : AbilityModifier, IChargesModifier
    {
        public override bool CanExecute => RemainingCharges > 0;

        public Field<int> RemainingCharges { get; }

        public ChargesModifier(int chargeCount = 0) => RemainingCharges = new Field<int>(chargeCount);
    }
}