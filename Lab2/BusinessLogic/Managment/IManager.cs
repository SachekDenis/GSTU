using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Managment
{
    public interface IManager<T>
        where T : class
    {
        T GetById(int itemId);

        void Add(T item);

        void Delete(int itemId);

        void Update(T item);

        IEnumerable<T> GetAll();
    }
}
