using EggCentric.Abilities.Modifiers;
using System.Threading.Tasks;

namespace EggCentric.Abilities
{
    public interface IAbility : IAbilityEventsProvider
    {
        public bool IsAvailable { get; }

        public Task<bool> TryToPerform();
        public void Tick(float timeStep);
        public void Cancel();

        public IAbility AddModifier(IAbilityModifier modifier);
    }
}