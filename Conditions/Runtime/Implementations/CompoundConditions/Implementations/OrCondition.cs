public class OrCondition : CompoundCondition
{
    public OrCondition(ICondition lhs, ICondition rhs) : base(lhs, rhs)
    {
    }

    public override bool Check()
    {
        return lhs.Check() || rhs.Check();
    }
}
