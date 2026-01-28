namespace EggCentric.Visitables
{
    public interface IVisitor<in TVisitable>
    {
        public bool Visit(TVisitable visitable);
    }
}