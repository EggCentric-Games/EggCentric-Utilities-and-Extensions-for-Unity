namespace EggCentric.Conditions
{
    public class AndCondition : CompoundCondition
    {
        public AndCondition(ICondition lhs, ICondition rhs) : base(lhs, rhs)
        {
        }

        public override bool Check()
        {
            return lhs.Check() && rhs.Check();
        }
    }
}