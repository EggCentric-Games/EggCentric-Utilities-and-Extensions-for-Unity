using System;

namespace EggCentric.Infrastructure.InitializationPipeline
{
    public interface IBootstrapEventsProvider
    {
        public event Action OnInvalidPipeline;
        public event Action OnInvalidPipelineStep;
    }
}