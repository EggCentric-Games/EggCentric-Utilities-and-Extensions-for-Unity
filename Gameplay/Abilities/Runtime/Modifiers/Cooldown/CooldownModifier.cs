using EggCentric.ModifiableValues;

namespace EggCentric.Abilities.Modifiers
{
    public class CooldownModifier : AbilityModifier, ICooldownModifier
    {
        public ModifiableValue Cooldown { get; private set; }
        public float RemainingTime { get; set; }

        public override bool CanExecute => RemainingTime <= 0;

        public CooldownModifier(float cooldown) => Cooldown.SetBaseValue(cooldown);

        public override void AfterExecution() => RemainingTime = Cooldown;
        public override void Tick(float timeStep) => RemainingTime -= timeStep;
    }
}