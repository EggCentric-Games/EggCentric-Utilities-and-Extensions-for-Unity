namespace EggCentric.ModifiableValues
{
    public interface IModificationApplicationStrategy
    {
        public float GetFor(float baseValue);
        public void MarkDirty();
    }
}
