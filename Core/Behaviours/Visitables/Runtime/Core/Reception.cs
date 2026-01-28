using EggCentric.Validation;
using System;

namespace EggCentric.Visitables
{
    public class Reception<TVisitor, TDestination> : IReception<TVisitor, TDestination> where TVisitor : IVisitor<TDestination>
    {
        public TDestination Destination { get; }

        public readonly IValidator<TVisitor> Validator;

        public event Action<TVisitor> OnBeforeVisit;
        public event Action<TVisitor> OnAfterVisit;
        public event Action<TVisitor> OnVisitRejection;

        public Reception(TDestination destination, IValidator<TVisitor> validator = null)
        {
            Destination = destination;
            Validator = validator;
        }

        public bool Accept(TVisitor visitor)
        {
            if (!IsApplicable(visitor))
            {
                OnVisitRejection?.Invoke(visitor);
                return false;
            }

            return Apply(visitor);
        }

        private bool Apply(TVisitor visitor)
        {
            OnBeforeVisit?.Invoke(visitor);
            var result = visitor.Visit(Destination);

            if (result)
                OnAfterVisit?.Invoke(visitor);

            return result;
        }

        public bool IsApplicable(TVisitor visitor) => Validator.Validate(visitor);
    }
}