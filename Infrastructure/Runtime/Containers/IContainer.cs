namespace EggCentric.Infrastructure
{
    public interface IContainer : IReadOnlyContainer
    {
        public void Bind(object item);
    }

    public interface IContainer<TItem> : IReadOnlyContainer<TItem>
    {
        public void Bind(TItem payload);
    }
}