using System;
using System.Collections.Generic;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.DataAccessLayer.Models;
using ComputerStore.DataAccessLayer.Repo;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public abstract class Validator<T>
        where T : class,IEntity
    {
        private readonly IRepository<T> _items;

        protected Validator(IRepository<T> items)
        {
            _items = items;
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!ValidateProperties(item))
                throw new ValidationException($"{nameof(T)} has invalid properties");

            _items.Add(item);
        }

        public void Delete(int itemId)
        {
            var item = _items.GetById(itemId);

            if (item == null)
            {
                throw new ValidationException($"{nameof(T)} not found");
            }

            if (!ValidateReferences(item))
                throw new ValidationException($"{nameof(T)} has references");

            _items.Delete(itemId);
        }

        public IEnumerable<T> GetAll()
        {
            return _items.GetAll();
        }

        public T GetById(int itemId)
        {
            return _items.GetById(itemId);
        }

        public void Update(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var itemToUpdate = _items.GetById(item.Id);

            if (!ValidateProperties(item))
                throw new ValidationException($"{nameof(T)} has invalid properties");

            if (itemToUpdate == null)
            {
                throw new ValidationException($"{nameof(T)} with such id doesn't exist");
            }

            _items.Update(item);
        }

        protected abstract bool ValidateReferences(T item);

        protected abstract bool ValidateProperties(T item);
    }
}
