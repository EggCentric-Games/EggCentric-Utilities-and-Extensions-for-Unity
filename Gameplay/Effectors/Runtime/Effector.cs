using UnityEngine;

namespace EggCentric.Effectors
{
    public abstract class Effector : IEffector
    {
        private EffectorSettings _parameters;

        public Effector(EffectorSettings parameters = null)
        {
            SetSettings(parameters);
        }

        public void SetSettings(EffectorSettings parameters)
        {
            if (parameters == null)
                parameters = new EffectorSettings();

            _parameters = parameters.Validate();
        }

        public virtual Vector2 EvaluateAt(Vector2 position)
        {
            Vector2 localOffset = position - _parameters.Origin;
            float angle = Vector2.SignedAngle(_parameters.ReferenceDirection, localOffset);

            float shapeMultiplier = _parameters.Shaping.GetMultiplier(angle);
            float rawForce = (_parameters.BaseValue + _parameters.Growth.Evaluate(localOffset.magnitude)) * shapeMultiplier;

            Vector2 forceDirection = localOffset.normalized;
            float forceMagnitude = Mathf.Clamp(rawForce, _parameters.ValueLimit.x, _parameters.ValueLimit.y);

            return forceDirection * forceMagnitude;
        }
    }
}