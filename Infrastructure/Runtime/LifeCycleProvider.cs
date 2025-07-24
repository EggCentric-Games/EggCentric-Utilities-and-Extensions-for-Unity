using EggCentric.Singletons;
using System;

namespace EggCentric.Infrastructure
{
    public class LifeCycleProvider : MonoSingleton<LifeCycleProvider>, ILifeCycleProvider
    {
        public event Action OnAwakePerformed;
        public event Action OnEnablePerformed;
        public event Action OnStartPerformed;
        public event Action OnUpdatePerformed;
        public event Action OnFixedUpdatePerformed;
        public event Action OnLateUpdatePerformed;
        public event Action OnDisablePerformed;

        protected override void Awake()
        {
            base.Awake();

            OnAwakePerformed?.Invoke();
        }

        private void OnEnable()
        {
            OnEnablePerformed?.Invoke();
        }

        private void Start()
        {
            OnStartPerformed?.Invoke();
        }

        private void Update()
        {
            OnUpdatePerformed?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdatePerformed?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdatePerformed?.Invoke();
        }

        private void OnDisable()
        {
            OnDisablePerformed?.Invoke();
        }
    }
}