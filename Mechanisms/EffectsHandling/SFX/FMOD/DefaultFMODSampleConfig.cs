using FMODUnity;
using UnityEngine;

namespace EggCentric.EffectsHandling.SFX.FMOD
{
    //[CreateAssetMenu(fileName = "FMOD_Sample_", menuName = "EggCentric/Configs/SFX/FMOD" )]
    [System.Serializable]
    public class DefaultFMODSampleConfig : IEffectConfig
    {
        public EventReference Event => _event;

        [SerializeField] public EventReference _event;
    }
}