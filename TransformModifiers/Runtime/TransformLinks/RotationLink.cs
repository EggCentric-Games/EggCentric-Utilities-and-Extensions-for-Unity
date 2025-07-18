using UnityEngine;

namespace EggCentric.TransformModifiers.Linking
{
    public class RotationLink : TransformLink<RotationLinkSettings>
    {
        private Quaternion offset => Quaternion.Euler(linkSettings.Offset);

        private void Update() => UpdateRotation();

        private void UpdateRotation()
        {
            if (!linkedObject)
                return;

            ApplyOffset(offset);
        }

        private void ApplyOffset(Quaternion offset)
        {
            if (linkSettings.ApplicationSpace == Space.Self)
                transform.localRotation = GetTargetRotation(offset);
            else
                transform.rotation = GetTargetRotation(offset);
        }

        private Quaternion GetTargetRotation(Quaternion offset) =>
            linkSettings.TargetSpace == Space.Self
            ? linkedObject.localRotation * offset
            : linkedObject.rotation * offset;
    }
}