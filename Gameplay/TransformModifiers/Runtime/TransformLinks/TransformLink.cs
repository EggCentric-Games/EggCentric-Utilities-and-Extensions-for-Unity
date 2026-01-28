using UnityEngine;
using EggCentric.Configurables;

namespace EggCentric.TransformModifiers.Linking
{
    public abstract class TransformLink<TSettings> : MonoBehaviour, IConfigurable<TSettings> where TSettings : TransformLinkParameters<TSettings>
    {
        public TSettings FollowParameters => linkSettings;

        [SerializeField] protected TSettings linkSettings;
        [SerializeField] protected Transform linkedObject;

        public void Follow(Transform target) => linkedObject = target;

        public void ApplyConfig(TSettings config)
        {
            linkSettings = config;
        }
    }
}
