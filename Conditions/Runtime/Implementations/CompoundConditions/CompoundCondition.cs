namespace EggCentric.Conditions
{
    public abstract class CompoundCondition : ICondition
    {
        protected ICondition lhs;
        protected ICondition rhs;

        public CompoundCondition(ICondition lhs, ICondition rhs)
        {
            this.lhs = lhs;
            this.rhs = rhs;
        }

        public abstract bool Check();
    }
}