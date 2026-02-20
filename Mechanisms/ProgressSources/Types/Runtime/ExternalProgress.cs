using EggCentric.ValueProviders;

namespace EggCentric.ProgressSources.Types
{
    public class ExternalProgress : IProgressSource
    {
        public float Time => _source.Value;

        private readonly IValueProvider<float> _source;

        public ExternalProgress(IValueProvider<float> source) => _source = source;
    }
}