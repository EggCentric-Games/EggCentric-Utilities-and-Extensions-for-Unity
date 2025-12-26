using System.Threading.Tasks;

namespace EggCentric.Infrastructure
{
    public interface IBootstrapStep
    {
        public Task Execute();
    }
}