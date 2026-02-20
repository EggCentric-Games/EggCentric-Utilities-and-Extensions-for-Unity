namespace EggCentric.ForceFields.Gravity
{
    public interface IGravityFieldSettings<out TGravityField> where TGravityField : class, IGravityFieldContext
    {
        public TGravityField CreateInstance();
    }
}