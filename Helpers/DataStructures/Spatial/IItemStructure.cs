namespace EggCentric.DataStructures
{
    public interface IItemStructure<TItem>
    {
        public bool Insert(TItem item);
        public bool Update(TItem item);
        public bool Remove(TItem item);
    }
}