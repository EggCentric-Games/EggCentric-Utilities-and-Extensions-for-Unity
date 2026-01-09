using UnityEngine;

namespace EggCentric.ForceFields.Types
{
    public class StaticField : IForceFieldContext
    {
        public float Strength { get; set; }
        public Vector2 Direction { get; set; }

        public Vector2 EvaluateForPosition(Vector2 position) => Strength * Direction;
    }
}