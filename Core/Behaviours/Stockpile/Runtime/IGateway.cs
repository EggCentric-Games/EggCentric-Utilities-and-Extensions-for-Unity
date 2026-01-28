namespace EggCentric.Common.Behaviours
{
    public interface IGateway<T>
    {
        public bool IsApplicable(T item);
    }
}