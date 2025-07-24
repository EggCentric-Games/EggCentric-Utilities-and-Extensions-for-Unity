using UnityEngine;
using EggCentric.Configurators;

namespace EggCentric.Configurables
{
    public abstract class ConfigurableComponent<TConfigurator> : MonoBehaviour, IConfigurable<TConfigurator> where TConfigurator : IConfigurator
    {
        [SerializeField] protected TConfigurator _configurator;

        public void ApplyConfig(TConfigurator configurator)
        {
            bool isRebuildNeeded = IsRebuildNeeded(configurator);
            _configurator = ValidateConfigurator(configurator);

            if (isRebuildNeeded)
                Reconstruct();
            else
                UpdateValues();
        }

        private void Reconstruct()
        {
            Deconstruct();
            Construct(_configurator);
        }

        private TConfigurator ValidateConfigurator(TConfigurator configurator)
        {
            if (configurator == null)
                Debug.LogError($"Invalid config!");

            return configurator;
        }

        public abstract void Deconstruct();
        protected abstract void Construct(TConfigurator configurator);
        protected abstract void UpdateValues();
        protected abstract bool IsRebuildNeeded(TConfigurator configurator);
    }
}