using System.Collections.Generic;

namespace EggCentric.Effectors
{
    public class EffectorOriginProvider
    {
        private Dictionary<ReferencePoint, IEffectorPositionProvider> _providers;

        public EffectorOriginProvider()
        {
            _providers = new Dictionary<ReferencePoint, IEffectorPositionProvider>()
            {
                { ReferencePoint.Transform, new TransformPositionProvider() },
                { ReferencePoint.CenterOfMass, new CenterOfMassPositionProvider() },
                { ReferencePoint.Collider, new ColliderPositionProvider() },
                { ReferencePoint.ContactPoint, new ContactPointPositionProvider() },
            };
        }

        public IEffectorPositionProvider Resolve(ReferencePoint referencePoint)
        {
            if(_providers.TryGetValue(referencePoint, out IEffectorPositionProvider provider))
                return provider;

            else return new TransformPositionProvider();
        }
    }
}