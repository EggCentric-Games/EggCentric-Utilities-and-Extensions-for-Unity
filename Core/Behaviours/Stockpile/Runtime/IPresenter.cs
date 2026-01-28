namespace EggCentric.Common.Behaviours
{
    public interface IPresenter<T>
    {
        public bool CanPresent<TRequested>(out TRequested component) where TRequested : T;
    }
}