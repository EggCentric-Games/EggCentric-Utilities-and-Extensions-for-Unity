using EggCentric.Abilities.Modifiers;
using EggCentric.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace EggCentric.Abilities
{
    public abstract class AbilityInstance : IAbility
    {
        public bool IsAvailable => !_isExecuting && CanBeExecuted();

        private readonly ICommand _command;

        private List<IAbilityModifier> _modifiers;
        private bool _isExecuting;

        public event Action OnExecutionRequested;
        public event Action OnExecutionDenied;
        public event Action OnExecutionStarted;
        public event Action OnExecutionInterrupted;
        public event Action OnExecutionCompleted;

        private CancellationTokenSource _cancellationTokenSource;

        public AbilityInstance(ICommand command)
        {
            _modifiers = new List<IAbilityModifier>();

            _command = command;
        }

        public IAbility AddModifier(IAbilityModifier modifier)
        {
            if (modifier == null)
            {
                Debug.LogWarning($"Modifier can not be null!");
                return this;
            }

            OnExecutionCompleted += modifier.AfterExecution;
            _modifiers.Add(modifier);

            return this;
        }

        public async Task<bool> TryToPerform()
        {
            OnExecutionRequested?.Invoke();

            if (!IsAvailable)
            {
                OnExecutionDenied?.Invoke();
                return false;
            }

            await Perform();
            return true;
        }

        public void Cancel() => _cancellationTokenSource?.Cancel();

        public bool TryGetModifier<TModifier>(out TModifier modifier) where TModifier : IAbilityModifier
        {
            modifier = _modifiers.OfType<TModifier>().FirstOrDefault();

            return modifier != null;
        }

        public void Tick(float timeStep)
        {
            foreach (var modifier in _modifiers)
                modifier.Tick(timeStep);
        }

        protected virtual void BeforeExecution()
        {
            _isExecuting = true;
            OnExecutionStarted?.Invoke();
        }

        protected virtual void OnInterruption()
        {
            _isExecuting = false;
            OnExecutionInterrupted?.Invoke();
        }

        protected virtual void AfterExecution()
        {
            _isExecuting = false;
            OnExecutionCompleted?.Invoke();
        }

        private async Task Perform()
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();

            BeforeExecution();
            try { await _command.Execute(_cancellationTokenSource.Token); }
            catch (OperationCanceledException) { OnInterruption(); }
            finally { AfterExecution(); }
        }

        private bool CanBeExecuted()
        {
            foreach (var modifier in _modifiers)
                if (!modifier.CanExecute)
                    return false;

            return true;
        }
    }

    public abstract class AbilityInstance<TData> : AbilityInstance where TData : AbilityData
    {
        protected readonly TData abilityData;

        public AbilityInstance(TData abilityData, ICommand command) : base(command) => this.abilityData = abilityData;
    }

    public abstract class AbilityInstance<TData, TContext> : AbilityInstance<TData> where TData : AbilityData
    {
        protected readonly TContext context;

        protected AbilityInstance(TData abilityData, TContext context, ICommand command) : base(abilityData, command)
        {
            this.context = context;
        }
    }
}