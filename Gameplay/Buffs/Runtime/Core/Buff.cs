using EggCentric.DI;
using System;

namespace EggCentric.Buffs
{
    public abstract class Buff : IBuff
    {
        public IReadOnlyContainer Context { get; }
        public IBuffReceiver Host { get; private set; }

        public Buff() => Context = CreateContext();

        public event Action<IBuff> OnApplication;
        public event Action<IBuff> OnRemoval;

        public bool Visit(IBuffReceiver receiver)
        {
            if (!TryBind(receiver))
                return false;

            ApplyTo(receiver);
            return true;
        }

        public void Remove()
        {
            HandleRemoval();
            OnRemoval?.Invoke(this);
            Host = null;
        }

        private void ApplyTo(IBuffReceiver receiver)
        {
            Host = receiver;
            OnApplication?.Invoke(this);
            HandleApplication();
        }

        protected abstract IContainer CreateContext();
        protected abstract bool TryBind(IBuffReceiver receiver);
        protected abstract void HandleApplication();
        protected abstract void HandleRemoval();
    }
}
