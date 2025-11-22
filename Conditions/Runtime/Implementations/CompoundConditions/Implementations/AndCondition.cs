namespace EggCentric.Conditions
{
    public class AndCondition : CompoundCondition
    {
        public override bool IsSatisfied => lhs.IsSatisfied && rhs.IsSatisfied;

        public AndCondition(ICondition lhs, ICondition rhs) : base(lhs, rhs)
        {
        }
    }
}