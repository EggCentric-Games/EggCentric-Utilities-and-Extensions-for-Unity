public class NotCondition : ICondition
{
    ICondition _source;

    public NotCondition(ICondition source)
    {
        _source = source;
    }

    public bool Check()
    {
        return !_source.Check();
    }
}