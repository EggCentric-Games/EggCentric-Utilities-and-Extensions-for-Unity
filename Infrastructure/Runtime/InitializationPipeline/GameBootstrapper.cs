using System.Threading.Tasks;

namespace EggCentric.Infrastructure
{
    public class GameBootstrapper : IGameBootstrapper
    {
        private readonly BootstrapRunner _bootstrapRunner = new();

        public GameBootstrapper()
        {
        }

        public async Task RunFor(IBootstrapPipeline pipeline) => await _bootstrapRunner.PerformBootstrap(pipeline);
    }
}