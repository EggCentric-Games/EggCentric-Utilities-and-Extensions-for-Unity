using UnityEngine;

namespace EggCentric.Configurables
{
    public abstract class ConfigurableComponent<TConfig> : MonoBehaviour, IConfigurable<TConfig> where TConfig : IConfig
    {
        [SerializeField] protected TConfig _config;

        public void ApplyConfig(TConfig config)
        {
            bool isRebuildNeeded = IsRebuildNeeded(config);
            _config = ValidateConfig(config);

            if (isRebuildNeeded)
                Reconstruct();
            else
                UpdateValues();
        }

        private void Reconstruct()
        {
            Deconstruct();
            Construct(_config);
        }

        private TConfig ValidateConfig(TConfig config)
        {
            if (config == null)
                Debug.LogError($"Invalid config!");

            return config;
        }

        public abstract void Deconstruct();
        protected abstract void Construct(TConfig config);
        protected abstract void UpdateValues();
        protected abstract bool IsRebuildNeeded(TConfig config);
    }
}