using EggCentric.Evaluators;
using EggCentric.QoL;
using UnityEngine;

namespace EggCentric.ForceFields.Types
{
    public class LineField : IForceFieldContext
    {
        public float Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                _lineDirection = VectorExtensions.FromAngle(_angle);
                _lineNormal = _lineDirection.Normal();
            }
        }

        public float Offset { get; set; }

        private readonly IGrowth _growth;

        private float _angle;
        private Vector2 _lineDirection;
        private Vector2 _lineNormal;

        public Vector2 EvaluateForPosition(Vector2 position)
        {
            float delta = position.DistanceToLine(_lineDirection) + Offset;
            float forceMagnitude = _growth.Evaluate(delta);
            Vector2 forceDirection = -_lineNormal;

            return forceDirection * forceMagnitude;
        }
    }
}