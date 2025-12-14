using EggCentric.DataContainers;
using System.Collections.Generic;

namespace EggCentric.ModifiableValues
{
    public interface IReadOnlyModifiableValue
    {
        public IReadOnlyField<float> BaseValue { get; }
        public ITrackableValue<float> ModifiedValue { get; }
        public IReadOnlyCollection<IValueModifier> ActiveModifiers { get; }
    }
}
