using UnityEngine;

namespace EggCentric.Effectors
{
    public class TransformPositionProvider : IEffectorPositionProvider
    {
        public Vector2 GetPosition(EffectorEntity effector)
        {
            return effector.transform.position;
        }
    }
}