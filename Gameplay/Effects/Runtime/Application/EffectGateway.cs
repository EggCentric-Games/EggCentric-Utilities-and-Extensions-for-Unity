using EggCentric.Validation;
using EggCentric.Visitables;

namespace EggCentric.Effects
{
    public class EffectGateway : IEffectGateway
    {
        public IEffectReceiver Target { get; }

        private readonly IReception<IEffect, IReception<IEffectComponent, IEffectReceiver>> _effectReception;
        private readonly IValidator<IEffectView> _effectValidator;

        public EffectGateway(IEffectReceiver target, IValidator<IEffectView> effectValidator, IValidator<IEffectComponent> componentValidator)
        {
            Target = target;
            _effectValidator = effectValidator;

            var effectComponentReception = new Reception<IEffectComponent, IEffectReceiver>(Target, componentValidator);
            _effectReception = new Reception<IEffect, IReception<IEffectComponent, IEffectReceiver>>(effectComponentReception, null);
        }

        public bool IsApplicable(IEffectView item) => _effectValidator.Validate(item);
        public bool IsApplicable(IEffect effect) => IsApplicable((IEffectView)effect);
        public bool Accept(IEffect visitor) => _effectReception.Accept(visitor);
    }
}