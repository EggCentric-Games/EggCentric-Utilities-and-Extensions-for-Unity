namespace EggCentric.Conditions
{
    public class XorCondition : CompoundCondition
    {
        public XorCondition(ICondition lhs, ICondition rhs) : base(lhs, rhs)
        {
        }

        public override bool Check()
        {
            bool lhsResult = lhs.Check();
            bool rhsResult = rhs.Check();

            return lhsResult != rhsResult;
        }
    }
}