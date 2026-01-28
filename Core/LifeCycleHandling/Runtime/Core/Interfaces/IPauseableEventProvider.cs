using System;

namespace EggCentric.LifeCycleHandling
{

    public interface IPauseableEventProvider
    {
        public event Action OnPause;
        public event Action OnResume;
    }
}