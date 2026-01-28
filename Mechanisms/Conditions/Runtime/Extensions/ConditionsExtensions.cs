using System.Collections.Generic;

namespace EggCentric.Conditions
{
    public static class ConditionsExtensions
    {
        public static ICondition Not(this ICondition condition) => new NotCondition(condition);
        public static ICondition And(this ICondition lhs, ICondition rhs) => new AndCondition(lhs, rhs);
        public static ICondition Or(this ICondition lhs, ICondition rhs) => new OrCondition(lhs, rhs);
        public static ICondition Xor(this ICondition lhs, ICondition rhs) => new XorCondition(lhs, rhs);

        public static bool AreSatisfied(this IEnumerable<ICondition> conditions)
        {
            foreach (var condition in conditions)
                if (!condition.IsSatisfied)
                    return false;

            return true;
        }
    }
}