using System;

namespace EggCentric.Visitables
{
    public interface IVisitingEventsProvider<out TVisitor>
    {
        public event Action<TVisitor> OnBeforeVisit;
        public event Action<TVisitor> OnAfterVisit;
        public event Action<TVisitor> OnVisitRejection;
    }
}