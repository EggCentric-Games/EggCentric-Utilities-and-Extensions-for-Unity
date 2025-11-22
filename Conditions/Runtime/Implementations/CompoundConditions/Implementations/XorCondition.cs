namespace EggCentric.Conditions
{
    public class XorCondition : CompoundCondition
    {
        public override bool IsSatisfied => lhs.IsSatisfied != rhs.IsSatisfied;

        public XorCondition(ICondition lhs, ICondition rhs) : base(lhs, rhs)
        {
        }
    }
}