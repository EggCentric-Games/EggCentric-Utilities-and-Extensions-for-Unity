using UnityEngine;

namespace EggCentric.Effectors
{
    public interface IEffectorPositionProvider
    {
        public Vector2 GetPosition(EffectorEntity effector);
    }
}