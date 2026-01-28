using EggCentric.DataContainers;
using EggCentric.Configurators;
using EggCentric.Evaluators;
using UnityEngine;

namespace EggCentric.Effectors
{
    [System.Serializable]
    public class EffectorParameters : Parameters<EffectorParameters>
    {
        [SerializeReference] public Field<ReferencePoint> ForceOrigin;
        [SerializeReference] public Field<float> ReferenceAngle;

        [SerializeReference] public Field<GrowthType> GrowthType;
        [SerializeReference] public Field<float> GrowthBase;
        [SerializeReference] public Field<ShapingType> ShapingType;

        [SerializeReference] public Field<float> BaseValue;
        [SerializeReference] public Field<Vector2> ValueLimit;

        [Header("Spatial")]
        [SerializeField] private ReferencePoint _serializableForceOrigin;
        [SerializeField] private float _serializableReferenceAngle;

        [Header("Evaluation")]
        [SerializeField] private GrowthType _serializableGrowthType;
        [SerializeField] private float _serializableGrowthBase;
        [SerializeField] private ShapingType _serializableShapingType;

        [Header("Output")]
        [SerializeField] private float _serializableBaseValue;
        [Range(Mathf.NegativeInfinity, Mathf.Infinity)]
        [SerializeField] private Vector2 _serializableValueLimit;

        public EffectorParameters()
        {
            Validate(out _);
        }

        public override void ResetToSerialized()
        {
            ForceOrigin.Value = _serializableForceOrigin;
            ReferenceAngle.Value = _serializableReferenceAngle;

            GrowthType.Value = _serializableGrowthType;
            GrowthBase.Value = _serializableGrowthBase;
            ShapingType.Value = _serializableShapingType;

            BaseValue.Value = _serializableBaseValue;
            ValueLimit.Value = _serializableValueLimit;
        }

        public override bool Validate(out EffectorParameters validated)
        {
            validated = this;

            ForceOrigin ??= new();
            ReferenceAngle ??= new();

            GrowthType ??= new();
            GrowthBase ??= new();
            ShapingType ??= new();

            BaseValue ??= new();
            ValueLimit ??= new();

            return true;
        }
    }
}