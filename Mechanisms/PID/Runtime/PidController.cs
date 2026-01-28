using System;
using UnityEngine;

namespace EggCentric.PID
{
    public abstract class PidController<TValue> : IPidController<TValue>
    {
        public float Output { get; private set; }

        protected TValue target;
        private Func<TValue> _getter;

        private PidCoefficients _coefficients;

        private float _error;
        private float _previousError;

        private float _integral;

        public PidController(Func<TValue> getter)
        {
            _getter = getter;
        }
        public void SetCoefficients(PidCoefficients coefficients) => _coefficients = coefficients;

        public void SetTarget(TValue target)
        {
            this.target = target;
            _previousError = 0f;
            _integral = 0f;
        }

        public void Tick(float timeStep)
        {
            _error = GetError(_getter());
            float proportional = _coefficients.Kp * _error;
            _integral += _coefficients.Ki * (_error * timeStep);
            _integral = _coefficients.Ki != 0 ? Mathf.Clamp(_integral, -1f * _coefficients.Ki, 1f * _coefficients.Ki) : 0f;
            float derivative = _coefficients.Kd * ((_error - _previousError) / timeStep);

            Output = proportional + _integral + derivative;
            _previousError = _error;
        }

        protected abstract float GetError(TValue value);

    }
}
