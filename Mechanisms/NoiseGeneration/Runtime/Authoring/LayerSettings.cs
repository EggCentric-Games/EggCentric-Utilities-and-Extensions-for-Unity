using System.Runtime.InteropServices;

namespace EggCentric.NoiseGeneration
{
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct LayerSettings
    {
        public GenerationSublayer[] Sublayers;
        public float Amplitude;
        public float Offset;
        public float Normalizer;
    }
}