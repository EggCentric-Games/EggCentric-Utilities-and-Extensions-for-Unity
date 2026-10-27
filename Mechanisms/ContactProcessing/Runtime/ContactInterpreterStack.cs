using System.Collections.Generic;

namespace EggCentric.Ballistics
{
    public class ContactInterpreterStack<TContact> : IContactInterpreterStack<TContact> where TContact : IContact
    {
        public IReadOnlyList<IContactInterpreter<TContact>> Items => _items;

        private readonly List<IContactInterpreter<TContact>> _items;

        public ContactInterpreterStack() => _items = new List<IContactInterpreter<TContact>>();

        public void Process(TContact contact)
        {
            foreach(var item in _items)
                item.Process(contact);
        }

        public void Add(IContactInterpreter<TContact> item) => _items.Add(item);
        public bool Remove(IContactInterpreter<TContact> item) => _items.Remove(item);
        public void Clear() => _items.Clear();
    }
}