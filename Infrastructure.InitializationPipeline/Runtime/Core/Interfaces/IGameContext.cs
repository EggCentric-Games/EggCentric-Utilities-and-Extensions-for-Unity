using EggCentric.DI;

namespace EggCentric.Infrastructure.InitializationPipeline
{
    public interface IGameContext
    {
        public IReadOnlyContainer<IService> Services { get; }
        public IReadOnlyContainer Runtime { get; }
    }
}