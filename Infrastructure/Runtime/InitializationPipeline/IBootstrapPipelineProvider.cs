namespace EggCentric.Infrastructure
{
    public interface IBootstrapPipelineProvider
    {
        public IBootstrapPipeline GetPipeline();
    }
}