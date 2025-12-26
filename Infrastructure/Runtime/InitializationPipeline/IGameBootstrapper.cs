using System.Threading.Tasks;

namespace EggCentric.Infrastructure
{
    public interface IGameBootstrapper
    {
        public Task RunFor(IBootstrapPipeline pipeline);
    }
}