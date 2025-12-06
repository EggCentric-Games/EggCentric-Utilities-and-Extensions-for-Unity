using EggCentric.DataContainers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.ModifiableValues
{
    public class ModifierRegistry
    {
        public IReadOnlyCollection<IValueModifier> AllModifiers => _modifierRegistry;
        public IReadOnlyCollection<IValueModifier> ActiveModifiers => _isDirty ? GetActiveModifiers() : _activeModifiers;
        public IReadOnlyField<bool> IsDirty => _isDirty;

        private readonly List<IValueModifier> _modifierRegistry;
        private IReadOnlyCollection<IValueModifier> _activeModifiers;

        private IModifierFilteringStrategy _filteringStrategy;
        private Field<bool> _isDirty;

        public event Action<IValueModifier> OnModifierAdded;
        public event Action<IValueModifier> OnModifierRemoved;

        public ModifierRegistry() : this(new FreeStrategy()) { }

        public ModifierRegistry(IModifierFilteringStrategy filteringStrategy)
        {
            _modifierRegistry = new List<IValueModifier>();
            _activeModifiers = Array.Empty<IValueModifier>();
            _isDirty = new Field<bool>(true);

            WithStrategy(filteringStrategy);
        }

        public ModifierRegistry WithStrategy(IModifierFilteringStrategy filteringStrategy)
        {
            if (filteringStrategy == null)
            {
                Debug.LogError($"Invalid filtering strategy!");
                return this;
            }

            _filteringStrategy = filteringStrategy;
            _isDirty.Value = true;
            return this;
        }

        public TModifier AddModifier<TModifier>(TModifier modifier) where TModifier : IValueModifier
        {
            _modifierRegistry.Add(modifier);
            _isDirty.Value = true;
            OnModifierAdded?.Invoke(modifier);

            return modifier;
        }

        public void RemoveModifier(IValueModifier modifier)
        {
            if (_modifierRegistry.Remove(modifier))
            {
                _isDirty.Value = true;
                OnModifierRemoved?.Invoke(modifier);
            }
        }

        protected IReadOnlyCollection<IValueModifier> GetActiveModifiers()
        {
            if (_filteringStrategy == null)
            {
                Debug.LogError($"Invalid filtering strategy!");
                return Array.Empty<IValueModifier>();
            }

            _activeModifiers = _filteringStrategy.FilterModifiers(_modifierRegistry);
            _isDirty.Value = false;

            return _activeModifiers;
        }
    }
}
