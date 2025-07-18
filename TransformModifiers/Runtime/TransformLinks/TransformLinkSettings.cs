using System;
using UnityEngine;

namespace EggCentric.TransformModifiers.Linking
{
    [Serializable]
    public abstract class TransformLinkSettings
    {
        public Space TargetSpace { get; set; }
        public Space ApplicationSpace { get; set; }
    }
}