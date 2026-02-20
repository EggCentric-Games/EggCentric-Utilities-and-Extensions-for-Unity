using EggCentric.ValueProviders.DataContainers;

namespace EggCentric.Samplers
{
    public interface ISampler
    {
        public Field<float> Step { get; }

        public float GetSample();
    }
}