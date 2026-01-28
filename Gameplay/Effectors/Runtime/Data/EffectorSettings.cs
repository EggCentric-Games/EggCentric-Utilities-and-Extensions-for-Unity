using EggCentric.Evaluators;
using UnityEngine;

namespace EggCentric.Effectors
{
    public class EffectorSettings
    {
        // Spatial
        public Vector2 Origin = Vector2.zero;
        public Vector2 ReferenceDirection = Vector2.right;

        // Force Shaping
        public IGrowth Growth = new FlatGrowth(0f);
        public IShapeFunction Shaping = new DefaultShaping();

        // Output
        public float BaseValue = 0f;
        public Vector2 ValueLimit = new Vector2(Mathf.NegativeInfinity, Mathf.Infinity);

        public EffectorSettings Validate()
        {
            if (Mathf.Approximately(ReferenceDirection.sqrMagnitude, 0f))
                ReferenceDirection = Vector2.right;

            if (Growth == null)
                Growth = new FlatGrowth(0f);

            if (Shaping == null)
                Shaping = new DefaultShaping();

            if (ValueLimit == Vector2.zero)
                ValueLimit = new Vector2(Mathf.NegativeInfinity, Mathf.Infinity);

            return this;
        }
    }
}