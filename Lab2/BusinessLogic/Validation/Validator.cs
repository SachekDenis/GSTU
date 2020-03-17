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

        public virtual void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _items.Add(item);
        }

        public virtual void Delete(int itemId)
        {
            var item = _items.GetById(itemId).Result;

            if (item == null)
            {
                throw new InvalidOperationException($"{nameof(T)} not found");
            }

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

        public virtual void Update(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var itemToUpdate = _items.GetById(item.Id).Result;

            if (itemToUpdate == null)
            {
                throw new InvalidOperationException($"{nameof(T)} with such id doesn't exist");
            }

            _items.Update(item);
        }
    }
}
