using UnityEngine;

public class PositionLink : TransformLink<PositionLinkSettings>
{
    private Vector3 totalOffset => linkSettings.StaticOffset - transform.forward * linkSettings.FollowDistance;

    private void Update() => UpdatePosition();

    private void UpdatePosition()
    {
        if (!linkedObject)
            return;

        ApplyOffset(totalOffset);
    }

    private void ApplyOffset(Vector3 offset)
    {
        if (linkSettings.ApplicationSpace == Space.Self)
            transform.localPosition = GetTargetPosition(offset);
        else
            transform.position = GetTargetPosition(offset);
    }

    private Vector3 GetTargetPosition(Vector3 offset) =>
        linkSettings.TargetSpace == Space.Self
        ? linkedObject.TransformVector(offset)
        : linkedObject.position + offset;
}