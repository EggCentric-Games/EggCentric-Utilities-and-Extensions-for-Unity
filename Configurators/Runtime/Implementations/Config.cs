using UnityEngine;

namespace EggCentric.Configurators
{
    public abstract class Config<TConfig> : ScriptableObject, IConfigurator<TConfig> where TConfig : Config<TConfig>
    {
        public abstract bool Validate(out TConfig validated);

        bool IConfigurator.Validate(out IConfigurator validated)
        {
            bool success = Validate(out TConfig typedValidated);
            validated = typedValidated;

            return success;
        }
    }
}