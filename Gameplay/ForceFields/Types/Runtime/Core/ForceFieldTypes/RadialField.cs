using EggCentric.Evaluators;
using UnityEngine;

namespace EggCentric.ForceFields.Types
{
    public class RadialField : IForceFieldContext
    {
        public Vector2 Center { get; set; }
        
        private readonly IGrowth _growth;

        public RadialField(IGrowth growth, Vector2 center)
        {
            Center = center;
            _growth = growth;
        }

        public Vector2 EvaluateForPosition(Vector2 position)
        {
            Vector2 delta = Center - position;
            return delta.normalized * _growth.Evaluate(delta.magnitude);
        }
    }
}