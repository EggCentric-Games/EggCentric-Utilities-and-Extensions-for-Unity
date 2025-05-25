using UnityEngine;

namespace EggCentric.Evaluators
{
    public class DotShaping : IShapeFunction
    {
        public virtual float GetMultiplier(float angle)
        {
            return Mathf.Cos(angle * Mathf.Deg2Rad);
        }
    }
}