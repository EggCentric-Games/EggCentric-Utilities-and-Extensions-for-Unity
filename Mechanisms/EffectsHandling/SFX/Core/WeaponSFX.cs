using EggCentric.EffectsHandling;
using EggCentric.EffectsHandling.SFX.FMOD;
using EggCentric.Sights;
using UnityEngine;

public class WeaponSFX : SFXProvider
{
    [SerializeField] private IEffectPlayer<DefaultFMODRequest, DefaultFMODHandle> _sfxPlayer;
    [SerializeField] private DefaultFMODSampleConfig _sfxSample;

    public override void PlayShot() => _sfxPlayer.PlayOnce(new DefaultFMODRequest(_sfxSample, transform.position));

    private void Awake()
    {
        _sfxPlayer = new NaiveFMODPlayer();
    }
}