namespace EggCentric.Ballistics
{
    public interface ICollisionHandler<THit, TContact> where TContact : IContact
    {
        public TContact HandleHit(THit hit);
    }
}