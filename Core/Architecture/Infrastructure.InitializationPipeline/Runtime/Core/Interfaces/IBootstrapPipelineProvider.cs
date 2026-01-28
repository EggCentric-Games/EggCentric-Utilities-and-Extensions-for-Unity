namespace EggCentric.Infrastructure.InitializationPipeline
{
    public interface IBootstrapPipelineProvider
    {
        public IBootstrapPipeline GetPipeline();
    }
}