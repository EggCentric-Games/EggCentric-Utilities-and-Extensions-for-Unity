using System;
using UnityEngine;

namespace EggCentric.Singletons
{
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton<T> where T : MonoSingleton<T>
    {
        public static T Instance { get; private set; }
        public static bool IsValid => Instance != null;

        public static event Action OnInitialized;

        protected virtual void Awake() => HandleInstancing();

        private void HandleInstancing()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning($"Singleton<{typeof(T).Name}> already has an instance.");
                DestroySelf();
                return;
            }

            AssignInstance();
        }

        private void AssignInstance()
        {
            Instance = (T)this;
            DontDestroyOnLoad(gameObject);

            OnInitialized?.Invoke();
        }

        private void DestroySelf()
        {
            Debug.LogWarning($"Duplicate singleton {typeof(T).Name} detected. Destroying {gameObject.name}");
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}