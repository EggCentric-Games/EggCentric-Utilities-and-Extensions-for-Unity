using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.Infrastructure
{
    public class Initializer : MonoBehaviour
    {
        private IEnumerable<IInitializable> _modules;

        public void Initialize()
        {
            CacheModules();
            InitializeModules();
        }

        public void Clear() => _modules = null;

        private void CacheModules() => _modules = GetComponentsInChildren<IInitializable>();

        private void InitializeModules()
        {
            foreach (var module in _modules)
                module.Initialize();
        }
    }
}