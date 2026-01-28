using EggCentric.Abilities.Modifiers;
using EggCentric.Conditions;
using System;

namespace EggCentric.Abilities
{
    public static class AbilityExtensions
    {
        public static IAbility WithCooldown(this IAbility ability, float cooldown)
        {
            var modifier = new CooldownModifier(cooldown);
            ability.AddModifier(modifier);

            return ability;
        }

        public static IAbility WithCondition(this IAbility ability, ICondition condition)
        {
            var modifier = new ConditionalModifier(condition);
            ability.AddModifier(modifier);

            return ability;
        }

        public static IAbility WithCondition(this IAbility ability, Func<bool> checker)
        {
            var modifier = new ConditionalModifier(checker);
            ability.AddModifier(modifier);

            return ability;
        }
    }
}