using BusinessLogic.Exception;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DataAccessLayer.Models;
using DataAccessLayer.Repo;

namespace BusinessLogic.Validation
{
    public abstract class Validator<T>
        where T : class,IEntity
    {
        private readonly IRepository<T> _items;

        protected Validator(IRepository<T> items)
        {
            _items = items;
        }

        public async Task Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!ValidateProperties(item))
                throw new ValidationException($"{nameof(T)} has invalid properties");

            await _items.Add(item);
        }

        public async Task Delete(int itemId)
        {
            var item = await _items.GetById(itemId);

            if (item == null)
            {
                throw new ValidationException($"{nameof(T)} not found");
            }

            if (!ValidateReferences(item))
                throw new ValidationException($"{nameof(T)} has references");

            await _items.Delete(itemId);
        }

        public IEnumerable<T> GetAll()
        {
            return _items.GetAll();
        }

        public async Task<T> GetById(int itemId)
        {
            return await _items.GetById(itemId);
        }

        public async Task Update(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var itemToUpdate = await _items.GetById(item.Id);

            if (!ValidateProperties(item))
                throw new ValidationException($"{nameof(T)} has invalid properties");

            if (itemToUpdate == null)
            {
                throw new ValidationException($"{nameof(T)} with such id doesn't exist");
            }

            await _items.Update(item);
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
