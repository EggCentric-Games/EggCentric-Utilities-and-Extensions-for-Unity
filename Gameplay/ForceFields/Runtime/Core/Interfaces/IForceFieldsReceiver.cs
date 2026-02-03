using EggCentric.DataContainers;

namespace EggCentric.ForceFields
{
    public interface IForceFieldsReceiver<in TSource> where TSource : IForceFieldContext
    {
        public ITrackableValue<bool> HasActiveFields { get; }

        public void AddSource(TSource source);
        public void RemoveSource(TSource source);
    }
}