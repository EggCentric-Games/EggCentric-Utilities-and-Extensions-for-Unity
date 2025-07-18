using System;
using UnityEngine;

namespace EggCentric.TransformModifiers.Linking
{
    [Serializable]
    public class PositionLinkSettings : TransformLinkSettings
    {
        public Vector3 StaticOffset { get; set; }
        public float FollowDistance { get; set; }

        public PositionLinkSettings(Vector3 staticOffset = default, float followDistance = 0f, Space targetSpace = Space.World, Space applicationSpace = Space.World)
        {
            StaticOffset = staticOffset;
            FollowDistance = followDistance;
            TargetSpace = targetSpace;
            ApplicationSpace = applicationSpace;
        }
    }
}