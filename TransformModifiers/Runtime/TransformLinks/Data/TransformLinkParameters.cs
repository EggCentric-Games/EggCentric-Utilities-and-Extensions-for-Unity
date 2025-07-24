using UnityEngine;
using EggCentric.DataContainers;
using EggCentric.Configurators;

namespace EggCentric.TransformModifiers.Linking
{
    [System.Serializable]
    public abstract class TransformLinkParameters<TParameters> : Parameters<TParameters> where TParameters : TransformLinkParameters<TParameters>
    {
        [SerializeReference] public Field<Space> TargetSpace = new();
        [SerializeReference] public Field<Space> ApplicationSpace = new();

        [SerializeField] private Space _serializedTargetSpace;
        [SerializeField] private Space _serializedApplicationSpace;

        public TransformLinkParameters(Space targetSpace = Space.World, Space applicationSpace = Space.World)
        {
            TargetSpace = new Field<Space>(targetSpace);
            ApplicationSpace = new Field<Space>(applicationSpace);
        }

        public override bool Validate(out TParameters validated)
        {
            validated = (TParameters)this;

            TargetSpace ??= new();
            ApplicationSpace ??= new();

            return true;
        }

        public override void ResetToSerialized()
        {
            TargetSpace.Value = _serializedTargetSpace;
            ApplicationSpace.Value = _serializedApplicationSpace;
        }
    }
}