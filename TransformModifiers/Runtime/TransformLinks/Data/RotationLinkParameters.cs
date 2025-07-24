using UnityEngine;
using EggCentric.DataContainers;

namespace EggCentric.TransformModifiers.Linking
{
    [System.Serializable]
    public class RotationLinkParameters : TransformLinkParameters<RotationLinkParameters>
    {
        [SerializeReference] public Field<Vector3> Offset;

        [SerializeField] private Vector3 _serializedOffset;

        public RotationLinkParameters(Vector3 staticOffset = default, Space targetSpace = Space.World, Space applicationSpace = Space.World) : base(targetSpace, applicationSpace)
        {
            Offset = new Field<Vector3>(staticOffset);
        }

        public override bool Validate(out RotationLinkParameters validated)
        {
            Offset ??= new();

            return base.Validate(out validated);
        }

        public override void ResetToSerialized()
        {
            base.ResetToSerialized();

            Offset.Value = _serializedOffset;
        }
    }
}