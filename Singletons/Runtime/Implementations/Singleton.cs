using System;
using UnityEngine;

namespace EggCentric.Singletons
{
    public abstract class Singleton<T> : ISingleton<T> where T : Singleton<T>
    {
        public static T Instance { get; private set; }
        public static bool IsValid => Instance != null;

        public static event Action OnInitialized;

        public Singleton() => HandleInstancing();

        private void HandleInstancing()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogError($"Singleton<{typeof(T).Name}> already has an instance.");
                return;
            }
            
            AssignInstance();
        }

        private void AssignInstance()
        {
            Instance = (T)this;

            OnInitialized?.Invoke();
        } 
    }
}