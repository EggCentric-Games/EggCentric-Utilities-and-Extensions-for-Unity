namespace EggCentric.Visitables
{
    public interface IVisitable<in TVisitor>
    {
        public bool Accept(TVisitor visitor);
    }
}