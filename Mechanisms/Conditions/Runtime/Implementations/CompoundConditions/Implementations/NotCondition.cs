namespace EggCentric.Conditions
{
    public class NotCondition : ICondition
    {
        public bool IsSatisfied => !_source.IsSatisfied;

        private ICondition _source;

        public NotCondition(ICondition source)
        {
            _source = source;
        }
    }
}