using EggCentric.ModifiableValues;

namespace EggCentric.Abilities.Modifiers
{
    public interface ICooldownModifier
    {
        public ModifiableValue Cooldown { get; }
        public float RemainingTime { get; set; }
    }
}