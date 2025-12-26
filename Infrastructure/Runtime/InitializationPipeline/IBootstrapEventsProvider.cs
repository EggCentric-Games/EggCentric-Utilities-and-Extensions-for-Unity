using System;

namespace EggCentric.Infrastructure
{
    public interface IBootstrapEventsProvider
    {
        public event Action OnInvalidPipeline;
        public event Action OnInvalidPipelineStep;
    }
}