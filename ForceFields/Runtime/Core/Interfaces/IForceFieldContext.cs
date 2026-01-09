using UnityEngine;

namespace EggCentric.ForceFields
{
    public interface IForceFieldContext
    {
        public Vector2 EvaluateForPosition(Vector2 position);
    }
}