using System.Collections.Generic;

namespace EggCentric.Infrastructure
{
    public interface IBootstrapPipeline
    {
        public IEnumerable<IBootstrapStep> BootstrapSteps { get; }
    }
}