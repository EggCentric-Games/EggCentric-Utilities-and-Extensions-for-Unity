using System;
using System.Collections.Generic;

namespace EggCentric.DataContainers
{
    public class Register<T> : IRegister<T>
    {
        private HashSet<T> registeredItems;

        public event Action<T> OnItemEntry;
        public event Action<T> OnItemExit;

        public Register(HashSet<T> registeredItems = null)
        {
            this.registeredItems = registeredItems ?? new HashSet<T>();
        }

        public void ProcessEntry(IEnumerable<T> registerEntry)
        {
            HashSet<T> itemsToProceed = new HashSet<T>(registerEntry);
            HashSet<T> newItems = SelectNewItems(itemsToProceed);
            HashSet<T> missingItems = SelectMissingItems(itemsToProceed);

            HandleNewItems(newItems);
            HandleMissingItems(missingItems);

            registeredItems = itemsToProceed;
        }

        private HashSet<T> SelectNewItems(HashSet<T> itemsInList)
        {
            HashSet<T> selectedItems = new HashSet<T>(itemsInList);
            selectedItems.ExceptWith(registeredItems);

            return selectedItems;
        }

        private HashSet<T> SelectMissingItems(HashSet<T> itemsInList)
        {
            HashSet<T> selectedItems = new HashSet<T>(registeredItems);
            selectedItems.ExceptWith(itemsInList);

            return selectedItems;
        }

        private void HandleNewItems(HashSet<T> items)
        {
            foreach (T item in items)
            {
                HandleItemEntry(item);
            }
        }

        private void HandleMissingItems(HashSet<T> items)
        {
            foreach (T item in items)
            {
                HandleItemExit(item);
            }
        }

        private void HandleItemEntry(T item)
        {
            OnItemEntry?.Invoke(item);
        }

        private void HandleItemExit(T item)
        {
            OnItemExit?.Invoke(item);
        }
    }
}