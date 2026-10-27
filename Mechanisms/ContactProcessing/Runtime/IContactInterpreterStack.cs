namespace EggCentric.Ballistics
{
    public interface IContactInterpreterStack<TContact> : IContactInterpreter<TContact>, IItemStack<IContactInterpreter<TContact>> where TContact : IContact
    {

    }
}