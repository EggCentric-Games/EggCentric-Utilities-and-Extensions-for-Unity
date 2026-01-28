namespace EggCentric.Infrastructure.InitializationPipeline
{
    public class Game : IGame
    {
        public IGameContext Context => _context;

        private readonly IGameContext _context;
        private readonly IGameBootstrapper _bootstrapper;
        private readonly IBootstrapPipelineProvider _pipelineProvider;

        public Game(IGameContext context, IGameBootstrapper bootstrapper, IBootstrapPipelineProvider pipelineProvider)
        {
            _context = context;
            _bootstrapper = bootstrapper;
            _pipelineProvider = pipelineProvider;
        }

        public void Run() => _bootstrapper.RunFor(_pipelineProvider.GetPipeline());
    }
}