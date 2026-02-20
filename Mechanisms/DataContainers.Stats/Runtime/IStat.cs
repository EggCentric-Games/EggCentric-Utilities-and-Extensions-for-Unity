using EggCentric.DataContainers;

namespace EggCentric.DataContainers.Stats
{
    public interface IStat : ITrackableValue<float>
    {
        public void SetValue(float value);
        public void ChangeValue(float change);
    }
}