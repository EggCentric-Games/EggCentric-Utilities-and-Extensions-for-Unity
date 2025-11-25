using System;

namespace EggCentric.PID
{
    public interface IPidController<TValue>
    {
        public float Output { get; }

        public void SetCoefficients(PidCoefficients coefficients);
        public void SetTarget(TValue target);
    }

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
            float derivative = _coefficients.Kd * ((_error - _previousError) / timeStep);

            Output = proportional + _integral + derivative;
            _previousError = _error;
        }

        protected abstract float GetError(TValue value);

    }
}
