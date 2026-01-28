using EggCentric.Common.Behaviours;

namespace EggCentric.Visitables
{
    public interface IReception<TVisitor, TDestination> : IGateway<TVisitor>, IVisitingEventsProvider<TVisitor>, IVisitable<TVisitor> where TVisitor : IVisitor<TDestination>
    {
        public TDestination Destination { get; }
    }
}