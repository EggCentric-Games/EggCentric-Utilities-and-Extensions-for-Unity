namespace EggCentric.Ballistics
{
    public interface IContactInterpreter<TContact> where TContact : IContact
    {
        public void Process(TContact contact);
    }
}