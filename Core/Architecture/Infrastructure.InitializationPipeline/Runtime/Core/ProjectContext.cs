using EggCentric.DI;

namespace EggCentric.Infrastructure.InitializationPipeline
{
    public class ProjectContext : IGameContext, IBootstrapContext
    {
        public IContainer<IService> Services => _services;
        public IContainer Runtime => _runtime;

        IReadOnlyContainer<IService> IBootstrapContext.Services => Services;
        IReadOnlyContainer<IService> IGameContext.Services => Services;
        IReadOnlyContainer IGameContext.Runtime => Runtime;

        private readonly IContainer<IService> _services = new NaiveContainer<IService>();
        private readonly IContainer _runtime = new NaiveContainer();
    }
}