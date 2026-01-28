using UnityEngine;
using EggCentric.DataContainers;

namespace EggCentric.TransformModifiers.Linking
{
    [System.Serializable]
    public class PositionLinkParameters : TransformLinkParameters<PositionLinkParameters>
    {
        [SerializeReference] public Field<Vector3> StaticOffset;
        [SerializeReference] public Field<float> FollowDistance;

        [SerializeField] private Vector3 _serializedStaticOffset;
        [SerializeField] private float _serializedFollowDistance;

        public PositionLinkParameters(Vector3 staticOffset = default, float followDistance = 0f, Space targetSpace = Space.World, Space applicationSpace = Space.World) : base(targetSpace, applicationSpace)
        {
            StaticOffset = new Field<Vector3>(staticOffset);
            FollowDistance = new Field<float>(followDistance);
        }

        public override bool Validate(out PositionLinkParameters validated)
        {
            StaticOffset ??= new();
            FollowDistance ??= new();

            return base.Validate(out validated);
        }

        public override void ResetToSerialized()
        {
            base.ResetToSerialized();
        }
    }
}