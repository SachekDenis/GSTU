using BusinessLogic.Exception;
using DataAccesLayer.Models;
using DataAccesLayer.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Managment
{
    public class Validator<T>
        where T : Entity
    {
        protected readonly IRepository<T> _items;
        public Validator(IRepository<T> items)
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
            var item = _items.GetById(itemId).Result;

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
            return _items.GetAll().Result;
        }

        public T GetById(int itemId)
        {
            return _items.GetById(itemId).Result;
        }

        public void Update(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var itemToUpdate = _items.GetById(item.Id).Result;

            if (!ValidateProperties(item))
                throw new ValidationException($"{nameof(T)} has invalid properties");

            if (itemToUpdate == null)
            {
                throw new ValidationException($"{nameof(T)} with such id doesn't exist");
            }

            _items.Update(item);
        }

        protected virtual bool ValidateReferences(T item)
        {
            return true;
        }

        protected virtual bool ValidateProperties(T item)
        {
            return true;
        }
    }
}
