using EggCentric.ModifiableValues;
using System;

namespace EggCentric.Abilities.Modifiers
{
    public class LimitedChargesModifier : ChargesModifier, ILimitedChargesModifier
    {
        public ModifiableValue MaxCharges { get; }

        public LimitedChargesModifier(int maxCharges = 1, int chargesCount = 0) : base(chargesCount)
        {
            RemainingCharges.OnValueChangedNoArgs += Validate;
            MaxCharges.ModifiedValue.OnValueChangedNoArgs += Validate;
        }

        private void Validate() => RemainingCharges.Value = Math.Clamp(RemainingCharges, 0, (int)MaxCharges);
    }
}