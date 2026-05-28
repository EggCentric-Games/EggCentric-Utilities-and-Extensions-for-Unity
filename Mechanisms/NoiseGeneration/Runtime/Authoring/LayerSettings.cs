using System.Runtime.InteropServices;

namespace EggCentric.ProceduralGeneration
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