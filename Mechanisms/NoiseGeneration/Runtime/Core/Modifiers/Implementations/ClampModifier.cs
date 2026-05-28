using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace EggCentric.ProceduralGeneration.Modifiers
{
    [MovedFrom(true, "EggCentric.ProceduralGeneration", null, "ClampLayerModifier")]
    public class ClampModifier : IGenerationModifier
    {
        private readonly float _min;
        private readonly float _max;

        public ClampModifier(float min = 0f, float max = 1f)
        {
            _min = min;
            _max = max;
        }

        public float Modify(float value) => Mathf.Clamp(value, _min, _max);
    }
}
