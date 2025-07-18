using System;
using UnityEngine;

[Serializable]
public abstract class TransformLinkSettings
{
    public Space TargetSpace { get; set; }
    public Space ApplicationSpace { get; set; }
}
