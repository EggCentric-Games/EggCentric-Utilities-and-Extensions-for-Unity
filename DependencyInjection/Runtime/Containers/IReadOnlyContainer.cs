namespace EggCentric.DI
{
    public interface IReadOnlyContainer
    {
        public bool Resolve<TRequested>(out TRequested service);
        public bool ResolveAll<TRequested>(out TRequested[] services);
    }

    public interface IReadOnlyContainer<TItem>
    {
        public bool Resolve<TRequested>(out TRequested service) where TRequested : TItem;
        public bool ResolveAll<TRequested>(out TRequested[] services) where TRequested : TItem;
    }
}