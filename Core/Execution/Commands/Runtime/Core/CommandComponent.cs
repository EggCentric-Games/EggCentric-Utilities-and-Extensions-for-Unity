using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace EggCentric.Commands
{
    public abstract class CommandComponent : MonoBehaviour, ICommand
    {
        [SerializeField] private bool _isExecutableWhenDisabled;
        private ICommand _command;

        public event Action OnCommandStarted;
        public event Action OnCommandCompleted;

        public Task Execute(CancellationToken cancellationToken = default)
        {
            if (_isExecutableWhenDisabled || gameObject.activeSelf)
                return _command.Execute(cancellationToken);

            return Task.CompletedTask;
        }

        protected virtual void Awake()
        {
            _command = InitializeCommand();

            if (_command == null)
            {
                Debug.LogError($"Command can not be null after initialization!");
                return;
            }

            _command.OnCommandStarted += () => OnCommandStarted?.Invoke();
            _command.OnCommandCompleted += () => OnCommandCompleted?.Invoke();
        }

        protected abstract ICommand InitializeCommand();
    }
}