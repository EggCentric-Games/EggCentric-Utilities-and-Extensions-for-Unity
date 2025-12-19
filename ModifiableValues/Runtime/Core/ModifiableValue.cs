using EggCentric.DataContainers;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.ModifiableValues
{
    public class ModifiableValue : IModifiableValue
    {
        public ITrackableValue<float> BaseValue => _baseValue;
        public ITrackableValue<float> ModifiedValue => _modifiedValue;
        public IReadOnlyCollection<IValueModifier> ActiveModifiers => _modifierRegistry.ActiveModifiers;

        private IModificationApplicationStrategy _applicationStrategy;

        private readonly Field<float> _baseValue;
        private readonly IDataCache<float> _modifiedValue;
        private readonly ModifierRegistry _modifierRegistry;

        public ModifiableValue(float baseValue = 0f)
        {
            _modifierRegistry = new ModifierRegistry();
            _baseValue = new Field<float>(baseValue);

            _modifiedValue = new AutomatedDataCache<float>(new PersistentDataCache<float>(), () => _applicationStrategy.ApplyFor(_baseValue));

            WithApplicationStrategy(new AllAppliedStrategy(_modifierRegistry.ActiveModifiers));
        }

        public ModifiableValue WithFilteringStrategy(IModifierFilteringStrategy filteringStrategy)
        {
            if(filteringStrategy == null)
            {
                Debug.LogError($"Invalid filtering strategy!");
                return this;
            }

            _modifierRegistry.WithStrategy(filteringStrategy);
            return this;
        }

        public ModifiableValue WithApplicationStrategy(IModificationApplicationStrategy applicationStrategy)
        {
            if (applicationStrategy == null)
            {
                Debug.LogError($"Invalid application strategy!");
                return this;
            }

            SetApplicationStrategy(applicationStrategy);
            return this;
        }

        public void SetBaseValue(float value) => _baseValue.Value = value;

        public IValueModifier AddModifier(IValueModifier modifier) => _modifierRegistry.AddModifier(modifier);
        public void RemoveModifier(IValueModifier modifier) => _modifierRegistry.RemoveModifier(modifier);

        public static implicit operator float(ModifiableValue obj) => obj.ModifiedValue.Value;

        private void SetApplicationStrategy(IModificationApplicationStrategy applicationStrategy)
        {
            if (_applicationStrategy != null)
            {
                _modifierRegistry.IsDirty.OnValueChangedNoArgs -= _modifiedValue.Invalidate;
                _baseValue.OnValueChangedNoArgs -= _modifiedValue.Invalidate;
            }

            _applicationStrategy = applicationStrategy;
            _modifiedValue.Invalidate();

            _modifierRegistry.IsDirty.OnValueChangedNoArgs += _modifiedValue.Invalidate;
            _baseValue.OnValueChangedNoArgs += _modifiedValue.Invalidate;
        }
    }
}
