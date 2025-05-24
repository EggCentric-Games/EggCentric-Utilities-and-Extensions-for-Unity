using UnityEngine;

namespace EggCentric.Utilities.Evaluators
{
    public class DotShaping : IShapeFunction
    {
        public virtual float GetMultiplier(float angle)
        {
            return Mathf.Cos(angle * Mathf.Deg2Rad);
        }
    }
}