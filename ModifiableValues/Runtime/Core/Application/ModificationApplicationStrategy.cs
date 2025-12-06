using System.Collections.Generic;

namespace EggCentric.ModifiableValues
{
    public abstract class ModificationApplicationStrategy : IModificationApplicationStrategy
    {
        protected readonly IReadOnlyCollection<IValueModifier> activeModifiers;

        private float _cachedValue;
        private bool _isDirty;

        public ModificationApplicationStrategy(IReadOnlyCollection<IValueModifier> activeModifiers) => this.activeModifiers = activeModifiers;

        public float GetFor(float baseValue)
        {
            if (!_isDirty)
                return _cachedValue;

            return RecalculateValue(baseValue);
        }

        public void MarkDirty() => _isDirty = true;

        protected abstract float ApplyFor(float baseValue);

        private float RecalculateValue(float baseValue)
        {
            _cachedValue = ApplyFor(baseValue);
            _isDirty = false;

            return _cachedValue;
        }
    }
}
