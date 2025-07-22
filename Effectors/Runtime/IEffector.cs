using UnityEngine;

namespace EggCentric.Effectors
{
    public interface IEffector
    {
        public void SetSettings(EffectorSettings parameters);
        public Vector2 EvaluateAt(Vector2 position);
    }
}