using EggCentric.Singletons;

namespace EggCentric.Infrastructure
{
    public class Game : Singleton<Game>, IGame
    {
        public IReadOnlyContainer<IService> Context => _context;

        private readonly IReadOnlyContainer<IService> _context;
        private readonly IGameBootstrapper _bootstrapper;
        private readonly IBootstrapPipelineProvider _pipelineProvider;

        public Game(IReadOnlyContainer<IService> context, IGameBootstrapper bootstrapper, IBootstrapPipelineProvider pipelineProvider)
        {
            _context = context;
            _bootstrapper = bootstrapper;
            _pipelineProvider = pipelineProvider;
        }

        public void Run() => _bootstrapper.RunFor(_pipelineProvider.GetPipeline());
    }
}