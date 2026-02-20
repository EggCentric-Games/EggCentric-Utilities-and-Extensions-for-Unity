using EggCentric.ValueProviders;

namespace EggCentric.ValueProviders.Stats
{
    public interface IStat : ITrackableValue<float>
    {
        public void SetValue(float value);
        public void ChangeValue(float change);
    }
}