using ComputerStore.DataAccessLayer.Context;
using ComputerStore.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputerStore.DataAccessLayer.Repo
{
    public class StoreRepository<T> : IRepository<T> where T : class, IEntity
    {
        private bool _disposed;
        private readonly StoreContext _context;

        public StoreRepository(StoreContext context)
        {
            _context = context;
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.Find<T>(id);
            if (item == null)
                return;
            _context.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>()
                         .AsNoTracking()
                         .FirstOrDefault(e => e.Id == id);
        }

        public void Update(T item)
        {
            var entry = _context.Set<T>().First(e => e.Id == item.Id);
            _context.Entry(entry).CurrentValues.SetValues(item);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
