using System.Threading.Tasks;

namespace EggCentric.Infrastructure.InitializationPipeline
{
    public class GameBootstrapper : IGameBootstrapper
    {
        private readonly IBootstrapContext _context;
        private readonly BootstrapRunner _bootstrapRunner;

        public GameBootstrapper(IBootstrapContext context)
        {
            _bootstrapRunner = new BootstrapRunner();

            _context = context;
        }

        public async Task RunFor(IBootstrapPipeline pipeline) => await _bootstrapRunner.PerformBootstrap(_context, pipeline);
    }
}