using System.Runtime.InteropServices;
using UnityEngine;

namespace EggCentric.NoiseGeneration
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GenerationSublayer
    {
        public Vector3 PositionMultiplier { get; set; }
        public Vector3 Velocity { get; set; }
        public float Power { get; set; }
    }
}