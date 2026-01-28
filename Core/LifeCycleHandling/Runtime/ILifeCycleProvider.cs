using EggCentric.Infrastructure;
using System;

namespace EggCentric.LifeCycleHandling
{
    public interface ILifeCycleProvider : IService
    {
        public event Action OnAwakePerformed;
        public event Action OnEnablePerformed;
        public event Action OnStartPerformed;
        public event Action OnUpdatePerformed;
        public event Action OnFixedUpdatePerformed;
        public event Action OnLateUpdatePerformed;
        public event Action OnDisablePerformed;
    }
}