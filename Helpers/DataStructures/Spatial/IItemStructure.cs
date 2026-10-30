namespace EggCentric.DataStructures
{
    public interface IItemStructure<TItem>
    {
        public bool Insert(TItem item);
        public void Update(TItem item);
        public void Remove(TItem item);
    }
}