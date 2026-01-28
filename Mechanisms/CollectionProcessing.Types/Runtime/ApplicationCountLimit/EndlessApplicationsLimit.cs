namespace EggCentric.CollectionProcessing.Types
{
    public class EndlessApplicationsLimit<T> : ApplicationCountLimiter<T>
    {
        public override bool CheckItemValidity(T item) => true;
        public override void RegisterApplicationOnItem(T item) {}
    }
}
