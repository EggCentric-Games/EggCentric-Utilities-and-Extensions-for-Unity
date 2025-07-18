using UnityEngine;

namespace EggCentric.TransformModifiers.Linking
{
    public abstract class TransformLink<TSettings> : MonoBehaviour where TSettings : TransformLinkSettings
    {
        public TSettings FollowParameters => linkSettings;

        [SerializeField] protected TSettings linkSettings;
        [SerializeField] protected Transform linkedObject;

        public void Follow(Transform target) => linkedObject = target;
    }
}
