using EggCentric.DI;

namespace EggCentric.Infrastructure.InitializationPipeline
{
    public class ProjectContext : IGameContext, IBootstrapContext
    {
        public IReadOnlyContainer<IService> Services => _services;
        public IContainer Runtime => _runtime;

        IReadOnlyContainer IGameContext.Runtime => Runtime;

        private readonly IContainer<IService> _services = new NaiveContainer<IService>();
        private readonly IContainer _runtime = new NaiveContainer();
    }
}