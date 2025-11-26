using EggCentric.Infrastructure;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EggCentric.InputHandling
{
    public class ContiniousInput<TInputType> where TInputType : struct
    {
        private ICoroutineRunner _coroutineRunner;
        private InputAction _observedAction;

        private Coroutine _actionRoutine;

        public event Action<TInputType> OnInput;

        public ContiniousInput(ICoroutineRunner coroutineRunner, InputAction observedAction)
        {
            _coroutineRunner = coroutineRunner;
            _observedAction = observedAction;
        }

        public void Enable()
        {
            _observedAction.started += OnActionStarted;
            _observedAction.canceled += OnActionFinished;
        }

        public void Disable()
        {
            _observedAction.started -= OnActionStarted;
            _observedAction.canceled -= OnActionFinished;
        }

        private void OnActionStarted(InputAction.CallbackContext context)
        {
            _actionRoutine = _coroutineRunner.StartCoroutine(OnAction());
        }

        private void OnActionFinished(InputAction.CallbackContext context)
        {
            if (_actionRoutine == null)
                return;

            _coroutineRunner.StopCoroutine(_actionRoutine);
            _actionRoutine = null;

            OnInput?.Invoke(default);
        }

        private IEnumerator OnAction()
        {
            while (true)
            {
                TInputType input = _observedAction.ReadValue<TInputType>();
                OnInput?.Invoke(input);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}