using System;
using System.Threading.Tasks;

namespace EggCentric.Infrastructure
{
    public sealed class BootstrapRunner : IBootstrapEventsProvider
    {
        public event Action OnInvalidPipeline;
        public event Action OnInvalidPipelineStep;

        public async Task PerformBootstrap(IBootstrapPipeline _pipeline)
        {
            if (_pipeline == null)
            {
                OnInvalidPipeline?.Invoke();
                return;
            }

            foreach (var step in _pipeline.BootstrapSteps)
            {
                if (step == null)
                {
                    OnInvalidPipelineStep?.Invoke();
                    continue;
                }
                await step.Execute();
            }
        }
    }
}