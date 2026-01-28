using EggCentric.Conditions;
using System;

namespace EggCentric.Abilities.Modifiers
{
    public class ConditionalModifier : AbilityModifier
    {
        public override bool CanExecute => _condition.IsSatisfied;

        private readonly ICondition _condition;

        public ConditionalModifier(ICondition condition) => _condition = condition;
        public ConditionalModifier(Func<bool> checker) => _condition = new SimpleCondition(checker);
    }
}