using System.Collections.Generic;

namespace EggCentric.Infrastructure.InitializationPipeline
{
    public interface IBootstrapPipeline
    {
        public IEnumerable<IBootstrapStep> BootstrapSteps { get; }
    }
}