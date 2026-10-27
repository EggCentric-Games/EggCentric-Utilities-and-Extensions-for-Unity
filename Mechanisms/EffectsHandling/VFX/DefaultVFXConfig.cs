using UnityEngine;

namespace EggCentric.EffectsHandling.VFX
{
    [System.Serializable]
    public class DefaultVFXConfig : IEffectConfig
    {
        public ParticleSystem Prefab => _prefab;

        [SerializeField] private ParticleSystem _prefab;
    }
}