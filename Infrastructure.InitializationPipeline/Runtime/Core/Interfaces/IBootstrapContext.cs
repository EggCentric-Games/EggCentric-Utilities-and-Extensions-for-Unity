using EggCentric.DI;

namespace EggCentric.Infrastructure.InitializationPipeline
{
    public interface IBootstrapContext

    {
        public IReadOnlyContainer<IService> Services { get; }
        public IContainer Runtime { get; }
    }
}