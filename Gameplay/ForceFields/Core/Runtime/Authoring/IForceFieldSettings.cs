namespace EggCentric.ForceFields
{
    public interface IForceFieldSettings<out TForceField> where TForceField : class, IForceFieldContext
    {
        public TForceField CreateInstance();
    }
}