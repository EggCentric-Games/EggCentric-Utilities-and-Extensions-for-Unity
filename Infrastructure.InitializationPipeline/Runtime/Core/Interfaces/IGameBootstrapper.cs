using System.Threading.Tasks;

namespace EggCentric.Infrastructure.InitializationPipeline
{
    public interface IGameBootstrapper
    {
        public Task RunFor(IBootstrapPipeline pipeline);
    }
}