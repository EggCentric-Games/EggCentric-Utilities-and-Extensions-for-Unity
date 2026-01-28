using System.Threading.Tasks;

namespace EggCentric.Infrastructure.InitializationPipeline
{
    public interface IBootstrapStep
    {
        public Task Execute(IBootstrapContext context);
    }
}