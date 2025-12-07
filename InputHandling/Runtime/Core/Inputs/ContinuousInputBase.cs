using EggCentric.Infrastructure;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EggCentric.InputHandling
{
    public abstract class ContinuousInputBase : Input
    {
        private ICoroutineRunner _coroutineRunner;

        private Coroutine _actionRoutine;

        public ContinuousInputBase(ICoroutineRunner coroutineRunner, InputAction observedAction) : base(observedAction)
        {
            _coroutineRunner = coroutineRunner;
        }

        protected override void OnActionStarted(InputAction.CallbackContext context)
        {
            _actionRoutine = _coroutineRunner.StartCoroutine(OnAction());
        }

        protected override void OnActionFinished(InputAction.CallbackContext context)
        {
            if (_actionRoutine == null)
                return;

            _coroutineRunner.StopCoroutine(_actionRoutine);
            _actionRoutine = null;

            SendDefault();
        }

        protected abstract void SendInput();
        protected abstract void SendDefault();

        private IEnumerator OnAction()
        {
            while (true)
            {
                SendInput();

                yield return new WaitForEndOfFrame();
            }
        }
    }
}