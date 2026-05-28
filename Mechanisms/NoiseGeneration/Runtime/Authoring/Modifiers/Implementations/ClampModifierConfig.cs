using EggCentric.ProceduralGeneration.Modifiers;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace EggCentric.ProceduralGeneration.Authoring.Modifiers
{
    [System.Serializable]
    [MovedFrom(true, "EggCentric.ProceduralGeneration", null, "ClampLayerModifierConfig")]
    public class ClampModifierConfig : ModifierConfig<ClampModifier>
    {
        public float Min => _min;
        public float Max => _max;

        [SerializeField] private float _min;
        [SerializeField] private float _max = 1f;

        protected override ClampModifier CreateModifier() => new ClampModifier(_min, _max);
    }
}
