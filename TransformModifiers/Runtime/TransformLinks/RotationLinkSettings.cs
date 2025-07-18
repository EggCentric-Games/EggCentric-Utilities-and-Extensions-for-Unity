using UnityEngine;

public class RotationLinkSettings : TransformLinkSettings
{
    public Vector3 Offset { get; set; }

    public RotationLinkSettings(Vector3 staticOffset = default, bool isLocalSpace = false, Space targetSpace = Space.World, Space applicationSpace = Space.World)
    {
        Offset = staticOffset;
        TargetSpace = targetSpace;
        ApplicationSpace = applicationSpace;
    }
}
