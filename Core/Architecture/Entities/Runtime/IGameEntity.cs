namespace EggCentric.Entities
{
    public interface IGameEntity
    {
        public bool Get<TComponent>(out TComponent component) where TComponent : IEntityComponent;
    }
}