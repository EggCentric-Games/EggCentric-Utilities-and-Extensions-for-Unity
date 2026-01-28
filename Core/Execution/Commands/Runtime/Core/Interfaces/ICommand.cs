using System;
using System.Threading;
using System.Threading.Tasks;

namespace EggCentric.Commands
{
    public interface ICommand
    {
        public event Action OnCommandStarted;
        public event Action OnCommandCompleted;

        public Task Execute(CancellationToken cancellationToken = default);
    }
}