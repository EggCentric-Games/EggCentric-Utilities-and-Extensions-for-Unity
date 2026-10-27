namespace EggCentric.Ballistics
{
    public class ContactHandler<THit, TContact> where THit : struct where TContact : IContact
    {
        private ICollisionHandler<THit, TContact> _collisionHandler;
        private IContactInterpreter<TContact> _interpreter;

        public ContactHandler(ICollisionHandler<THit, TContact> collisionHandler, IContactInterpreter<TContact> interpreter)
        {
            _collisionHandler = collisionHandler;
            _interpreter = interpreter;
        }

        public void HandleHit(THit hit)
        {
            var contact = _collisionHandler.HandleHit(hit);
            _interpreter.Process(contact);
        }
    }
}