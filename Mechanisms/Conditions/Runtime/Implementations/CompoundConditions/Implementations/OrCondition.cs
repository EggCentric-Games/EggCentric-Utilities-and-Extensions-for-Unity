namespace EggCentric.Conditions
{
    public class OrCondition : CompoundCondition
    {
        public override bool IsSatisfied => lhs.IsSatisfied || rhs.IsSatisfied;

        public OrCondition(ICondition lhs, ICondition rhs) : base(lhs, rhs)
        {
        }
    }
}