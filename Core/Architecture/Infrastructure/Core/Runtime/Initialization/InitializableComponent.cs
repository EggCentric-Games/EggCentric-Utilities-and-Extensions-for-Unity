using UnityEngine;

namespace EggCentric.Infrastructure
{
    public abstract class InitializableComponent : MonoBehaviour, IInitializable
    {
        protected bool isInitialized;

        public void Initialize()
        {
            if (isInitialized)
                return;

            isInitialized = true;
            OnInitialized();
        }

        protected abstract void OnInitialized();
    }
}