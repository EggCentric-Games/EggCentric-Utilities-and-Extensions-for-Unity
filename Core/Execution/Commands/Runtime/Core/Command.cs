using System;
using System.Threading;
using System.Threading.Tasks;

namespace EggCentric.Commands
{
    public abstract class Command : ICommand
    {
        public event Action OnCommandStarted;
        public event Action OnCommandCompleted;

        public async Task Execute(CancellationToken cancellationToken = default)
        {
            OnCommandStarted?.Invoke();
            await ExecuteInternal();
            OnCommandCompleted?.Invoke();
        }

        protected abstract Task ExecuteInternal(CancellationToken cancellationToken = default);
    }

    public abstract class Command<TContext> : Command
    {
        protected readonly TContext context;

        public Command(TContext context) => this.context = context;
    }
}