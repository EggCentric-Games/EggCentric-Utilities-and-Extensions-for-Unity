using EggCentric.ValueProviders;

namespace EggCentric.QoL.Selectors
{
    public interface INumberSelector : IValueProvider<float>
    {
        public void Validate();
    }
}