//using EggCentric.Effects.Sources;
//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace EggCentric.Explosions
//{
//    public class Explosion : AoE
//    {
//        public Explosion(Vector3 origin, IEnumerable<EffectLayer> effectLayers) : base(origin, effectLayers)
//        {
//        }
//    }

//    public interface IExplosionAffectedObject
//    {

//    }

//    public class ExplosionProcessor
//    {
//        private List<ExplosionRequest> _pendingRequests;

//        public ExplosionProcessor() => _pendingRequests = new List<ExplosionRequest>();

//        public void Tick(float timeStep)
//        {
//            ProcessRequests();
//            ClearQueue();

//            _pendingRequests[0].In(Vector3.zero).Test();
//        }

//        private void ClearQueue() => _pendingRequests.Clear();

//        private void ProcessRequests()
//        {
//            throw new NotImplementedException();
//        }

//        public IExplosionRequest Combine(IExplosionRequest lhs, IExplosionRequest rhs);
//    }

//    public interface IExplosionRequest
//    {
//        public Explosion Create();
//    }

//    public abstract class AoeEffectBuilder<TBuilder> where TBuilder : AoeEffectBuilder<TBuilder>
//    {
//        protected readonly List<EffectLayer> _layers;
//        protected Vector3 _origin;

//        public AoeEffectBuilder() => _layers = new List<EffectLayer>();

//        public TBuilder In(Vector3 origin)
//        {
//            _origin = origin;
//            return (TBuilder)this;
//        }

//        public TBuilder WithLayer(EffectLayer effectLayer)
//        {
//            _layers.Add(effectLayer);
//            return (TBuilder)this;
//        }
//    }

//    public class ExplosionRequest : AoeEffectBuilder<ExplosionRequest>, IExplosionRequest
//    {
//        public Explosion Create() => new Explosion(_origin, _layers);

//        public void Test() { }
//    }
//}
