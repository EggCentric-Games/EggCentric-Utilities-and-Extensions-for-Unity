using UnityEngine;

namespace EggCentric.ProceduralGeneration
{
    [System.Serializable]
    public struct OctaveSettings
    {
        public int OctavesCount => _octavesCount;
        public float OctaveScale => _octaveScale;
        public float OctavePower => _octavePower;
        public float OctaveAngle => _octaveAngle;
        public float OctaveSpeed => _octaveSpeed;

        [SerializeField] private int _octavesCount;
        [SerializeField] private float _octaveScale;
        [SerializeField] private float _octavePower;
        [SerializeField] private float _octaveAngle;
        [SerializeField] private float _octaveSpeed;

        public Octave[] CreateOctaves()
        {
            var octaves = new Octave[OctavesCount];
            for (int i = 0; i < octaves.Length; i++)
            {
                var octave = new Octave();
                octave.Scale = Mathf.Pow(OctaveScale, i);
                octave.Power = Mathf.Pow(OctavePower, i);
                octave.Speed = Mathf.Pow(OctaveSpeed, i);
                octave.Angle = OctaveAngle * i;

                octaves[i] = octave;
            }

            return octaves;
        }

        public float GetNormalizer()
        {
            if (OctavePower == 1f)
                return OctavesCount;

            return (Mathf.Pow(OctavePower, OctavesCount) - 1) / (OctavePower - 1);
        }
    }
}