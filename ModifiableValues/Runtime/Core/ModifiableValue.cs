using EggCentric.DataContainers;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.ModifiableValues
{
    public class ModifiableValue
    {
        public IReadOnlyCollection<IValueModifier> ActiveModifiers => _modifierRegistry.ActiveModifiers;
        public float ModifiedValue => _applicationStrategy.GetFor(BaseValue);

        public readonly Field<float> BaseValue;

        private readonly ModifierRegistry _modifierRegistry;
        private IModificationApplicationStrategy _applicationStrategy;

        public ModifiableValue(float baseValue = 0f)
        {
            _modifierRegistry = new ModifierRegistry();
            BaseValue = new Field<float>(baseValue);

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
    
        public IValueModifier AddModifier(IValueModifier modifier) => _modifierRegistry.AddModifier(modifier);
        public void RemoveModifier(IValueModifier modifier) => _modifierRegistry.RemoveModifier(modifier);

        public static implicit operator float(ModifiableValue obj) => obj.ModifiedValue;

        private void SetApplicationStrategy(IModificationApplicationStrategy applicationStrategy)
        {
            if (_applicationStrategy != null)
            {
                _modifierRegistry.IsDirty.OnValueChangedNoArgs -= _applicationStrategy.MarkDirty;
                BaseValue.OnValueChangedNoArgs -= _applicationStrategy.MarkDirty;
            }

            _applicationStrategy = applicationStrategy;
            _modifierRegistry.IsDirty.OnValueChangedNoArgs += _applicationStrategy.MarkDirty;
            BaseValue.OnValueChangedNoArgs += _applicationStrategy.MarkDirty;
        }
    }
}
