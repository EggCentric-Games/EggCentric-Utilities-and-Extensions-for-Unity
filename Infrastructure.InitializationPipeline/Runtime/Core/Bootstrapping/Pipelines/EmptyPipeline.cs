using System;
using System.Collections.Generic;

namespace EggCentric.Infrastructure.InitializationPipeline
{
    public sealed class EmptyPipeline : IBootstrapPipeline
    {
        public IEnumerable<IBootstrapStep> BootstrapSteps => Array.Empty<IBootstrapStep>();
    }
}