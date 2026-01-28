using EggCentric.DataContainers;
using System.Collections.Generic;

namespace EggCentric.ModifiableValues
{
    public interface IReadOnlyModifiableValue : IValueProvider<float>
    {
        public ITrackableValue<float> BaseValue { get; }
        public ITrackableValue<float> ModifiedValue { get; }
        public IReadOnlyCollection<IValueModifier> ActiveModifiers { get; }

        float IValueProvider<float>.Value => ModifiedValue.Value;
    }
}
