using UnityEngine;

namespace EggCentric.ForceFields.Types
{
    [System.Serializable]
    public struct StaticFieldSettings : IForceFieldSettings<StaticField>
    {
        public Vector3 Direction;
        public float Strength;

        public StaticField CreateInstance()
        {
            return new StaticField()
            {
                Direction = Direction.normalized,
                Strength = Strength,
            };
        }
    }
}